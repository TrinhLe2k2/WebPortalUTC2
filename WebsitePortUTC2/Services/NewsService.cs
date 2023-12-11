using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebsitePortUTC2.Models;

namespace WebsitePortUTC2.Services
{
    public interface INewsService
    {
        Task<dynamic> GetAllNews();
        Task<dynamic> GetNewsByID(int id);
        Task<dynamic> PostNews(string name, string description, int newsCategoryId, string metaUrl);
        Task<dynamic> GetListNewsByPaging(int? NewsCategoryId, string? SearchText, int? Page, int? Record);
        Task<dynamic> PutNews(int newsId, string name, string description, int newsCategoryId, string metaUrl, string publishedAt);
        Task<bool> DeleteNews(int newsId);
    }
    public class NewsService : INewsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NewsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<dynamic> GetAllNews()
        {
            // Sử dụng HttpClient tái sử dụng
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync("https://api-intern-test.h2aits.com/News/GetListByStatus?status=1");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            // Chuyển đổi JSON thành đối tượng dynamic
            var newsApiResponse = JsonConvert.DeserializeObject<dynamic>(content);

            // Kiểm tra cấu trúc của đối tượng JSON trả về
            if (newsApiResponse.data != null && newsApiResponse.data.Count > 0)
            {
                return newsApiResponse.data;
            }

            // Trả về null hoặc giá trị mặc định tùy thuộc vào logic ứng dụng của bạn
            return null;
        }
        public async Task<dynamic> GetNewsByID(int id)
        {
            // Sử dụng HttpClient tái sử dụng
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync("https://api-intern-test.h2aits.com/News/GetById?id="+id);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            // Chuyển đổi JSON thành đối tượng dynamic
            var newsApiResponse = JsonConvert.DeserializeObject<dynamic>(content);

            // Kiểm tra cấu trúc của đối tượng JSON trả về
            if (newsApiResponse.data != null)
            {
                return newsApiResponse.data;
            }

            // Trả về null hoặc giá trị mặc định tùy thuộc vào logic ứng dụng của bạn
            return null;
        }
        public async Task<dynamic> GetListNewsByPaging(int? NewsCategoryId, string? SearchText, int? Page, int? Record)
        {
            var url = "https://api-intern-test.h2aits.com/News/GetListByPaging?SequenceStatus=1";

            if(NewsCategoryId != null) url += "&NewsCategoryId=" + NewsCategoryId;
            if(SearchText != "" && SearchText != null) url += "&SearchText=" + SearchText;
            if(Page != null) url += "&Page=" + Page;
            if(Record != null) url += "&Record=" + Record;

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var newsApiResponse = JsonConvert.DeserializeObject<dynamic>(content);
            if (newsApiResponse.data != null && newsApiResponse.data.Count > 0)
            {
                return newsApiResponse.data;
            }
            return null;
        }
        public async Task<dynamic> PostNews(string name, string description, int newsCategoryId, string metaUrl)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var apiUrl = "https://api-intern-test.h2aits.com/News/Create";

                // Tạo dữ liệu theo định dạng x-www-form-urlencoded
                var formData = new Dictionary<string, string>
                    {
                        { "Name", name },
                        { "Description", description },
                        { "NewsCategoryId", newsCategoryId.ToString() },
                        { "MetaUrl", metaUrl },
                        { "Status", "1" }, // Nếu Status là kiểu int, cần chuyển về kiểu string
                        { "PublishedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") } // Định dạng ngày giờ theo đúng yêu cầu của API
                    };

                var content = new FormUrlEncodedContent(formData);

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
        public async Task<dynamic> PutNews(int newsId, string name, string description, int newsCategoryId, string metaUrl, string publishedAt)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var apiUrl = $"https://api-intern-test.h2aits.com/News/Update";

                // Tạo dữ liệu theo định dạng x-www-form-urlencoded
                var formData = new Dictionary<string, string>
                {
                    { "Id", newsId.ToString() },
                    { "Name", name },
                    { "Description", description },
                    { "NewsCategoryId", newsCategoryId.ToString() },
                    { "MetaUrl", metaUrl },
                    { "Status", "1" },
                    { "PublishedAt", publishedAt }
                };

                var content = new FormUrlEncodedContent(formData);

                // Thực hiện cuộc gọi PUT và nhận response
                var response = await client.PutAsync(apiUrl, content);

                // Đảm bảo cuộc gọi thành công (StatusCode 2xx)
                response.EnsureSuccessStatusCode();

                // Đọc response và chuyển đổi thành kiểu dữ liệu cụ thể (hoặc kiểu NewsModel nếu bạn muốn)
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
        public async Task<bool> DeleteNews(int newsId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var apiUrl = $"https://api-intern-test.h2aits.com/News/Delete?id={newsId}";

                // Thực hiện cuộc gọi DELETE và nhận response
                var response = await client.DeleteAsync(apiUrl);

                // Đảm bảo cuộc gọi thành công (StatusCode 2xx)
                response.EnsureSuccessStatusCode();

                // Xóa thành công
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây (hiển thị thông báo lỗi hoặc chuyển hướng đến trang lỗi)
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }


    }
}
