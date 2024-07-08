using PicturePilot.Data.Enums;

namespace PicturePilot.Data.Entities;

public class Report : BaseEntity
{
    public string Message { get; set; }
    public User Sender { get; set; }
    public ReportType ReportType { get; set; }
    public int TargetId { get; set; }
}
