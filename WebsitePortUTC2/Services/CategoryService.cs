using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebsitePortUTC2.Services
{
    public interface ICategoryService
    {
        Task<dynamic> GetCategoryById(int categoryId);
        Task<dynamic> GetListByStatus(int statusId);
        Task<dynamic> GetListCategoryByPaging(string searchText, int page, int record);

        Task<dynamic> PostCategory(string name);
        Task<dynamic> PutCategory(int categoryId, string name, int status);
        Task<bool> DeleteCategory(int categoryId);
    }
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<dynamic> GetCategoryById(int categoryId)
        {
            try
            {
                // Sử dụng HttpClient tái sử dụng
                var httpClient = _httpClientFactory.CreateClient();

                var response = await httpClient.GetAsync("https://api-intern-test.h2aits.com/NewsCategory/GetById?id=" + categoryId);
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
            catch (Exception ex)
            {
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }

        public async Task<dynamic> GetListByStatus(int statusId)
        {
            try
            {
                // Sử dụng HttpClient tái sử dụng
                var httpClient = _httpClientFactory.CreateClient();

                var response = await httpClient.GetAsync("https://api-intern-test.h2aits.com/NewsCategory/GetListByStatus?status="+ statusId);
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
            catch (Exception ex)
            {
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }

        public async Task<dynamic> GetListCategoryByPaging(string searchText, int page, int record)
        {
            try
            {
                var url = "https://api-intern-test.h2aits.com/NewsCategory/GetListByPaging?SequenceStatus=1";

                if (searchText != "" && searchText != null) url += "&SearchText=" + searchText;
                if (page != null) url += "&Page=" + page;
                if (record != null) url += "&Record=" + record;

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
            catch (Exception ex)
            {
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }

        public async Task<dynamic> PostCategory(string name)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var apiUrl = "https://api-intern-test.h2aits.com/NewsCategory/Create";

                // Tạo dữ liệu theo định dạng x-www-form-urlencoded
                var formData = new Dictionary<string, string>
                    {
                        { "Name", name },
                        { "Status", "1" }
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

        public async Task<dynamic> PutCategory(int categoryId, string name, int status)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var apiUrl = $"https://api-intern-test.h2aits.com/NewsCategory/Update";

                // Tạo dữ liệu theo định dạng x-www-form-urlencoded
                var formData = new Dictionary<string, string>
                {
                    { "Id", categoryId.ToString() },
                    { "Name", name },
                    { "Status", status.ToString() }
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

        public async Task<bool> DeleteCategory(int categoryId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var apiUrl = $"https://api-intern-test.h2aits.com/NewsCategory/DeleteHard?id={categoryId}";

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
