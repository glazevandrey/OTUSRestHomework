using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        DataContext _context;
        public EFRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<T> GetAsync(long id)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(m=>m.Id == id);
            return result;
        }
    }
}
