using Microsoft.EntityFrameworkCore;
using SmartEAI.Api.Models;

namespace SmartEAI.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // 在資料庫裡建立 Patients 和 MedicalRecords 兩張表
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
    }
}