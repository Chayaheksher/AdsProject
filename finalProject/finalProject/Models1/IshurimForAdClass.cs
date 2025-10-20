using Humanizer;

namespace finalProject.Models
{
    public class IshurimForAdClass
    {
        public int adNum {  get; set; }
        public int? characterIdNeedApproval {  get; set; }
        public int ishurForAdId { get; set; }
        public string? adType { get; set; }
        public string? size { get; set; }
        public string? location { get; set; }
        public string? status {  get; set; }
        public int nowStatusId {  get; set; }
        public int? statusIshur {  get; set; }
    }
}
