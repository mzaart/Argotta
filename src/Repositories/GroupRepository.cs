using System.Linq;
using System.Threading.Tasks;
using Multilang.Db.Contexts;
using Multilang.Models.Db;

namespace Multilang.Repositories
{
    public class GroupRepository : IRepository<Group>
    {
        private ApplicationDbContext dbContext;

        public GroupRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Delete(Group entity)
        {
            dbContext.Groups.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Group> Find(System.Predicate<Group> predicate)
        {
            return await Task.Run(
                () => dbContext.Groups.Where(g => predicate(g)).FirstOrDefault());
        }

        public IQueryable<Group> GetAll()
        {
            return dbContext.Groups.AsQueryable();
        }

        public async Task<Group> GetById(string title)
        {
            return await Task.Run(() =>
                dbContext.Groups.SingleOrDefault(g => g.Title.ToString() == title));
        }

        public async Task Insert(Group entity)
        {
            dbContext.Groups.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}