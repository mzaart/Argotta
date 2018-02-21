using System;
using System.Linq;
using System.Threading.Tasks;
using Multilang.Db.Contexts;
using Multilang.Models.Db;

namespace Multilang.Repositories.UserRepository
{
    public class UserRepository : IRepository<User>
    {
        private ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Delete(User entity)
        {
            dbContext.Users.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<User> Find(Predicate<User> predicate)
        {
            return await Task.Run(
                () => dbContext.Users.Where(u => predicate(u)).FirstOrDefault());
                
        }

        public IQueryable<User> GetAll()
        {
            return dbContext.Users.AsQueryable();
        }

        public async Task<User> GetById(string id)
        {
            return await Task.Run(() =>
                dbContext.Users.SingleOrDefault(u => u.Id.ToString() == id));
        }

        public async Task Insert(User entity)
        {
            dbContext.Users.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}