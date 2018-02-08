
namespace Multilang.Services.AuthTokenServices 
{
    public interface IAuthTokenService<T>
    {
        string Issue(T data);
        bool IsValid(string token);
        T GetData(string token);
    }
}