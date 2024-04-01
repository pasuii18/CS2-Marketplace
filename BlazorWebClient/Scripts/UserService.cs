using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Xml.Linq;

namespace BlazorWebClient.Scripts
{
    public class UserService
    {
        public readonly string BaseUrl = "https://localhost:7274/api/v1";
        private readonly HttpClient _httpClient;
        public event Action OnNavbarRefresh;
        private string _token { get; set; }
        private string _uuid { get; set; }
        private string _role { get; set; }

        public HttpClient HttpClient => GetHttpClient();
        public string UserUUID 
        {
            get => _uuid;
            private set => _uuid = value;
        }
        public string UserRole 
        {
            get => _role;
            private set
            {
                _role = value;
                OnNavbarRefresh.Invoke();
            }
        }
        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                SetClaimsFromToken();
                OnNavbarRefresh.Invoke();
            }
        }

        public bool IsUserLoggedIn => !string.IsNullOrEmpty(_token);
        public bool IsCoolGuy => UserRole is "Developer" or "Administrator";
        public bool IsSuperCoolGuy => UserRole is "Developer";

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        private void SetClaimsFromToken()
        {
            if (IsUserLoggedIn)
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(Token);
                UserUUID = jwtToken.Claims.FirstOrDefault(c => c.Type == "uuid")?.Value;
                UserRole = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            }
        }
        public void Clear()
        {
            Token = null;
            UserUUID = null;
            UserRole = null;
        }
        private HttpClient GetHttpClient()
        {
            if (IsUserLoggedIn) _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return _httpClient;
        }
    }
}
