namespace MYAcademy_PortfolioProject.Entities
{
    public class Message
    {
        public int MessageID { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderSubject { get; set; }
        public string MessageDetail { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
