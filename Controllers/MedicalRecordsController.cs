using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEAI.Api.Data;
using SmartEAI.Api.Models;

namespace SmartEAI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicalRecordsController(AppDbContext context)
        {
            _context = context;
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
    }
}