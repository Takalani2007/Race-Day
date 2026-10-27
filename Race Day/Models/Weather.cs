namespace Race_Day.Models
{
  public class Weather
{
    [Key]
    public int WeatherId { get; set; }

    [Required]
    public int EventId { get; set; }   // Foreign Key

    [MaxLength(50)]
    public string Condition { get; set; } = string.Empty;

    public double Temperature { get; set; }

    public DateTime RecordedAt { get; set; } = DateTime.Now;

    // 🔗 Navigation property
    [ForeignKey(nameof(EventId))]
    public Event? Event { get; set; }
}
}
