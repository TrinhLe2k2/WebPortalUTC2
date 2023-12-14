namespace WebsitePortUTC2.Models
{
    public class NewsCategoryObj
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }
        public int? SchoolId { get; set; }
        public int? ParentId { get; set; }
        public string? Remark { get; set; }
        public int? Sort { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? Timer { get; set; }
    }

    public class ImageObj
    {
        public int Id { get; set; }
        public string? RelativeUrl { get; set; }
        public string? SmallUrl { get; set; }
        public string? MediumUrl { get; set; }
    }

    public class NewsModel
    {
        public int Id { get; set; }
        public int? SchoolId { get; set; }
        public int? NewsCategoryId { get; set; }
        public string? Name { get; set; }
        public string? NameSlug { get; set; }
        public string? Description { get; set; }
        public int? ImageId { get; set; }
        public string? Detail { get; set; }
        public bool? IsHot { get; set; }
        public int? ViewNumber { get; set; }
        public DateTime PublishedAt { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaUrl { get; set; }
        public string? MetaImagePreview { get; set; }
        public ImageObj ImageObj { get; set; }
        public NewsCategoryObj NewsCategoryObj { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? Timer { get; set; }
    }

    public class ApiResponse
    {
        public int? Result { get; set; }
        public long? Time { get; set; }
        public string? DataDescription { get; set; }
        public List<NewsModel> Data { get; set; }
        public int? Data2nd { get; set; }
        public ErrorObj Error { get; set; }
    }

    public class ErrorObj
    {
        public int Code { get; set; }
        public string? Message { get; set; }
    }
    public class ResultNews
    {
    }
}
