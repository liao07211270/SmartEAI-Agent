using System.Text;
using System.Text.Json;

namespace SmartEAI.Api.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        // 透過依賴注入 (DI) 取得 HttpClient 與隱藏在保險箱的 API Key
        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"] ?? throw new ArgumentNullException("找不到 Gemini API Key");
        }

        public async Task<string> GenerateMedicalSummaryAsync(string prompt)
        {
            // 1. 清理 API Key：拔除可能不小心存入的雙引號與前後空白 (剛剛就是漏了這行！)
            var cleanApiKey = _apiKey.Replace("\"", "").Trim();

            // 2. 完整模型名稱 (使用 2026 年最新版的 gemini-2.5-flash)
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={cleanApiKey}";

            // 依照 Google API 規定的 JSON 格式組裝請求內容
            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            
            // 發送 POST 請求給 Google
            var response = await _httpClient.PostAsync(url, content);
            
            // 3. 【除錯神器】如果失敗，把 Google 伺服器給的「真實抱怨內容」抓出來印在畫面上
            if (!response.IsSuccessStatusCode)
            {
                var errorDetail = await response.Content.ReadAsStringAsync();
                throw new Exception($"Google API 拒絕了請求。狀態碼: {response.StatusCode}, 詳細原因: {errorDetail}");
            }

            // 讀取並解析回傳的 JSON 結果
            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonResponse);
            
            var generatedText = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text").GetString();

            return generatedText ?? "無法生成摘要";
        }
    }
}