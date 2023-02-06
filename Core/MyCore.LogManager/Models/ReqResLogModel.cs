namespace MyCore.LogManager.Models
{
    public class ReqResLogModel
    {
        public string RequestIP { get; set; }
        public string RequestPhone { get; set; }
        public int RequestUserId { get; set; }
        public string ServicesName { get; set; }
        public string RequestData { get; set; }
        public DateTime RequestDate { get; set; }
        public string ResponseData { get; set; }
        public DateTime ResponseDate { get; set; }
        public TimeSpan ResponseTotalTime { get; set; }
        public string ResponseMessage { get; set; }
    }
}