namespace WebsitePortUTC2.Models
{
    public class News
    {
        public int Id { get; set; }
        public int? SchoolId { get; set; }
        public int? NewsCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public string Detail { get; set; }
        public bool? IsHot { get; set; }
        public int? ViewNumber { get; set; }
        public DateTime PublishedAt { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string MetaUrl { get; set; }
        public string MetaImagePreview { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? Timer { get; set; }
    }
}
