using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;

namespace Multilang.Repositories.ProfilePicRepository
{
    public class ProfilePicRepository : IProfilePicRepository
    {
        private IHostingEnvironment environment;

        public ProfilePicRepository(IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        
        async Task<bool> IProfilePicRepository.setProfilePic(string userId, string base64EncodedPic)
        {
            string filename = userId + ".jpg";
            string path = Path.Combine(environment.ContentRootPath, "profile_pics", filename);
            byte[] img = WebEncoders.Base64UrlDecode(base64EncodedPic);

            try
            {
                using (FileStream fs = System.IO.File.Create(path))
                {
                    await fs.WriteAsync(img, 0, img.Length);
                    return true;
                }
            }
            catch (IOException e)
            {
                return false;
            }
        }

        string IProfilePicRepository.getProfilePicPath(string userId)
        {
            string filename = userId + ".jpg";
            string path = Path.Combine(environment.ContentRootPath, "profile_pics", filename);
            if (!File.Exists(path))
            {
                path = Path.Combine(environment.ContentRootPath, "profile_pics", "default.jpg");
            }
            
            return path;
        }
    }
}