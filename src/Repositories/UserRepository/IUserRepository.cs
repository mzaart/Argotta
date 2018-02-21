using System;
using System.Threading.Tasks;
using Multilang.Models.Db;
using Multilang.Repositories;

namespace Multilang.Repositories.UserRepository {

    public interface IUserRepository : IRepository<User>
    {
       
    }
}