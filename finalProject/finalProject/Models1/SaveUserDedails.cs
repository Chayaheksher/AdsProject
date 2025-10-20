using static finalProject.COMMON.Enums;

namespace finalProject.Models
{
    public class SaveUserDedails
    {
       public int? userId { get; set; }
        //public DateTime lastEnterDate { get; set; }
        public string charactersName { get; set; }
        public string fullName {  get; set; }
        public string userName {  get; set; }
        public int passwords {  get; set; }
        //public string charactersName {  get; set; }
        public List<SaveComm> getCommunication { get; set; }
    }
}
