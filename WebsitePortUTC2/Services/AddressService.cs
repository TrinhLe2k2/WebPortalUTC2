using Newtonsoft.Json;
using System.Xml.Linq;

namespace WebsitePortUTC2.Services
{
    public interface IAddressService
    {
        Task<dynamic> PutAddress(string addressText);
    }
    public class AddressService : IAddressService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AddressService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<dynamic> PutAddress(string addressText)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var apiUrl = $"https://api-intern-test.h2aits.com/Address/Update";

                // Tạo dữ liệu theo định dạng x-www-form-urlencoded
                var formData = new Dictionary<string, string>
                {
                    { "Id", "2" },
                    { "AddressText", addressText }
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
