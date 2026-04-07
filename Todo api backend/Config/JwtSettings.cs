using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Todo_api_backend.Config
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
