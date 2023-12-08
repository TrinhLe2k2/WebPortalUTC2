using Newtonsoft.Json;

namespace WebsitePortUTC2.Services
{
    public interface ISchoolService
    {
        Task<dynamic> GetSchoolAsync();
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
    }
}
