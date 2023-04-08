using Identiy_API.Model;

namespace Identiy_API.Infrastructure.AuthFactory.AuthAgregatorFactory
{
    public interface IAgregatorAuthFactory
    {
        IAuthFactoy GetAuthService(string role);
    }
}
