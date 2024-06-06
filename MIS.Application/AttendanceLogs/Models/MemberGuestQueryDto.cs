namespace MIS.Application.AttendanceLogs.Models
{
    public class MemberGuestQueryDto
    {
        public List<MemberGuestQueryItem> Results { get; set; }
        public int Total { get; set; }
    }

    public class MemberGuestQueryItem
    {
        public long? MemberId { get; set; }
        public long? GuestId { get; set; }
        public string FullName { get; set; }
        public string Network { get; set; }
    }
}
