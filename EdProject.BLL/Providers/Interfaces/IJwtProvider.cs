using EdProject.DAL.Entities;

namespace EdProject.BLL.Providers.Interfaces
{
    public interface IJwtProvider
    {
        public string GenerateAccessToken(User appUser);
        public string GenerateRefreshToken();
    }
}
