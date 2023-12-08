using Newtonsoft.Json;
using System.Net.Http;

namespace WebsitePortUTC2.Services
{
    public interface INewsService
    {
        Task<dynamic> GetAllNews();
        Task <dynamic> GetListNewsByPaging(int? NewsCategoryId, string? SearchText, int? Page, int? Record);
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

        public async Task<dynamic> GetListNewsByPaging(int? NewsCategoryId, string? SearchText, int? Page, int? Record)
        {
            var url = "https://api-intern-test.h2aits.com/News/GetListByPaging?SequenceStatus=1&SchoolId=2";

            if(NewsCategoryId != null) url += "&NewsCategoryId=" + NewsCategoryId;
            if(SearchText != "") url += "&SearchText=" + SearchText;
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
    }
}
