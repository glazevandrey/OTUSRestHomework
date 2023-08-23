using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T?> GetAsync(long id);
        public Task<T> AddAsync(T customer);

    }
}
