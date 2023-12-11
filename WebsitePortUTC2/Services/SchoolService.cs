using Newtonsoft.Json;

namespace WebsitePortUTC2.Services
{
    public interface ISchoolService
    {
        Task<dynamic> GetSchoolAsync();
        Task<dynamic> PutSchool(int schoolId, string name, string shortName, string code, string logoUrl, string faviconUrl, string hotline, string phone, string email);
    }
    public class SchoolService : ISchoolService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SchoolService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<dynamic> GetSchoolAsync()
        {
            try
            {
                // Sử dụng HttpClient tái sử dụng
                var httpClient = _httpClientFactory.CreateClient();

                var response = await httpClient.GetAsync("https://api-intern-test.h2aits.com/School/GetById?id=2");
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

        public async Task<dynamic> PutSchool(int schoolId, string name, string shortName, string code, string logoUrl, string faviconUrl, string hotline, string phone, string email)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var apiUrl = $"https://api-intern-test.h2aits.com/School/Update";

                // Tạo dữ liệu theo định dạng x-www-form-urlencoded
                var formData = new Dictionary<string, string>
                {
                    { "Id", schoolId.ToString() },
                    { "Name", name },
                    { "ShortName", shortName },
                    { "Code", code },
                    { "LogoUrl", logoUrl },
                    { "FaviconUrl", faviconUrl },
                    { "Hotline", hotline },
                    { "Phone", phone },
                    { "Email", email },
                    { "addressId", "2" }
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

    }
}
