using EdProject.DAL.Entities;
using System.Collections.Generic;

namespace EdProject.BLL.Providers.Interfaces
{
    public interface IJwtProvider
    {
        public string GenerateAccessToken(User appUser, IList<string> roles);
        public string GenerateRefreshToken();
    }
}
