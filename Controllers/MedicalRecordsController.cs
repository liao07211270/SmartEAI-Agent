using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEAI.Api.Data;
using SmartEAI.Api.Models;
using SmartEAI.Api.Services;

namespace SmartEAI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IGeminiService _geminiService;

        // 注入 (DI) 同時取得資料庫與 AI 服務
        public MedicalRecordsController(AppDbContext context, IGeminiService geminiService)
        {
            _context = context;
            _geminiService = geminiService;
        }

        // GET: api/MedicalRecords/patient/1 (取得特定病患的所有就診紀錄)
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetRecordsByPatient(int patientId)
        {
            var records = await _context.MedicalRecords
                .Where(m => m.PatientId == patientId)
                .ToListAsync();

            return Ok(records);
        }

        // POST: api/MedicalRecords (新增就診紀錄)
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> PostMedicalRecord(MedicalRecord record)
        {
            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();
            return Ok(record);
        }

        // GET: api/MedicalRecords/patient/1/summary (AI 生成病歷摘要)
        [HttpGet("patient/{patientId}/summary")]
        public async Task<ActionResult<object>> GetPatientSummary(int patientId)
        {
            // 從資料庫撈出該病患的所有就診紀錄
            var records = await _context.MedicalRecords
                .Where(m => m.PatientId == patientId)
                .OrderBy(m => m.VisitDate)
                .ToListAsync();

            if (!records.Any())
            {
                return NotFound(new { Message = "找不到該病患的就診紀錄，無法生成摘要。" });
            }

            // 將撈出來的資料組裝成純文字，準備餵給 AI
            var recordDetails = string.Join("\n", records.Select(r => 
                $"日期: {r.VisitDate:yyyy-MM-dd}, 症狀: {r.Symptom}, 診斷: {r.Diagnosis}"));

            // 設計系統提示詞 (Prompt Engineering)
            var prompt = $@"
            你現在是一位專業的醫療資訊系統助理。請根據以下病患的歷史就診紀錄，
            生成一份簡明扼要的「病歷摘要報告」，請務必使用繁體中文，並以專業且客觀的語氣撰寫。
            請僅依據提供的紀錄進行總結。若紀錄中未提及某項資訊（如過敏史或特定檢查結果），請標註「無相關紀錄」，嚴禁自行虛構診斷結果 。
            
            報告需包含以下三個段落：
            1. 主要症狀總結
            2. 歷次診斷變化趨勢
            3. 系統自動生成的後續照護建議
            
            以下是病患的就診紀錄：
            {recordDetails}";

            // 呼叫 Gemini 服務進行推理
            try
            {
                var summary = await _geminiService.GenerateMedicalSummaryAsync(prompt);
                
                // 將結果包裝成 JSON 回傳給前端
                return Ok(new { 
                    PatientId = patientId, 
                    RecordCount = records.Count,
                    AiSummary = summary 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "AI 服務發生錯誤", Details = ex.Message });
            }
        }
    }
}