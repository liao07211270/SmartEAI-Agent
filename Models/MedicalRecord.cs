namespace SmartEAI.Api.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; } // 外部鍵 (Foreign Key)，對應到 Patient 的 Id
        public DateTime VisitDate { get; set; }
        public string Symptom { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        
        // 關聯設定：這筆紀錄屬於哪一位病患
        public Patient? Patient { get; set; }
    }
}