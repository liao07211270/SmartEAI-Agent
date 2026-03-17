namespace SmartEAI.Api.Services
{
    // 這是一份合約，規定任何實作這個介面的類別，都必須具備 GenerateMedicalSummaryAsync 這個功能
    public interface IGeminiService
    {
        Task<string> GenerateMedicalSummaryAsync(string prompt);
    }
}