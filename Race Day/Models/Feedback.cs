namespace Race_Day.Models
{
        public class Feedback
    {
        public int FeedbackId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public Event? Event { get; set; }
    }

}
