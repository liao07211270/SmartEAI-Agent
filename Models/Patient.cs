namespace SmartEAI.Api.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        
        // 關聯設定：一個病患可以擁有多筆就診紀錄
        public List<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}