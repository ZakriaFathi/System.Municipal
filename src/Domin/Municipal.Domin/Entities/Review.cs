using Municipal.Utils.Enums;


namespace Municipal.Domin.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public ReviewState ReviewState { get; set; } = ReviewState.Pending;
        public Guid ByUser { get; set; }
        public Guid ReviewedBy { get; set; }
        public PermissionNames PermissionNames { get; set; }
        public string AfterReview { get; set; }
        public string? BeforeReview { get; set; }
    }
}
