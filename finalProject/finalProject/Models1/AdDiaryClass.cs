namespace finalProject.Models
{
    public class AdDiaryClass
    {
        public DateTime? dateAndTime {  get; set; }
        public string? userName {  get; set; }
        public DateTime? publicationDates { get; set; }
        public string? ishurType {  get; set; }
        public string? approvalOrRejection {  get; set; }
        public int approvalOrRejectionCode {  get; set; }
        public string? note {  get; set; }
    }
}
