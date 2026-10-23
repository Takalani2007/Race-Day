namespace Race_Day.Models
{
   public class Enrolment
    {
        public int EnrolmentId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime EnrolledAt { get; set; }
        public User? User { get; set; }
        public Event? Event { get; set; }
    }
}
