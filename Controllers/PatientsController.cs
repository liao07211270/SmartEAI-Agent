using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEAI.Api.Data;
using SmartEAI.Api.Models;

namespace SmartEAI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // 透過依賴注入取得資料庫的連線
        public PatientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients (取得所有病患清單)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        // POST: api/Patients (新增病患)
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return Ok(patient);
        }
    }
}