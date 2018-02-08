using System.Threading.Tasks;

namespace Multilang.Repositories.ProfilePicRepository
{
    public interface IProfilePicRepository
    {
        Task<bool> setProfilePic(string userId, string base64EncodedPic);
        string getProfilePicPath(string userId);
    }
}