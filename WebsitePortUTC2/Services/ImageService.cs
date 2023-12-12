using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace WebsitePortUTC2.Services
{
    public interface IImageService
    {
        Task<dynamic> PostImage(string refId, IFormFile ImageFile);
    }
    public class ImageService : IImageService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ImageService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<dynamic> PostImage(string refId, IFormFile imageFile)
        {
            try
            {
                string accessToken = "wgq0qSWCgxw+0t98Vl5YLHtf24Mnmq4ufcsNZ9H55oWBjWlTyf9L2d/SGPIvEWF3";
                var client = _httpClientFactory.CreateClient();
                var apiUrl = "https://image-intern-test.h2aits.com/Image/UploadImage";

                // Tạo dữ liệu theo định dạng MultipartFormData
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(refId), "RefId");
                var imageContent = new StreamContent(imageFile.OpenReadStream());
                content.Add(imageContent, "ImageFile", imageFile.FileName);

                // Thêm token vào tiêu đề Authorization
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Thực hiện cuộc gọi POST và nhận response
                var response = await client.PostAsync(apiUrl, content);

                // Đảm bảo cuộc gọi thành công (StatusCode 2xx)
                response.EnsureSuccessStatusCode();

                // Đọc response và chuyển đổi thành kiểu dynamic (hoặc kiểu NewsModel nếu bạn muốn)
                var responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(responseData);

                // Xử lý dữ liệu nếu cần
                return data;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây (hiển thị thông báo lỗi hoặc chuyển hướng đến trang lỗi)
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }


    }
}
