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
            // 使用 Gemini 1.5 Flash 模型 (速度快、適合長文本處理)
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}";

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
            response.EnsureSuccessStatusCode();

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