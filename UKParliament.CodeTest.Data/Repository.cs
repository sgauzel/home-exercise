using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UKParliament.CodeTest.Data
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        private PersonManagerContext _personManagerContext;
        public Repository(PersonManagerContext personManagerContext)
        {
            _personManagerContext = personManagerContext;
        }
        public async Task<int> Add(T entity)
        {
           _personManagerContext.Set<T>().Add(entity);
           return await _personManagerContext.SaveChangesAsync();
        }

        public async Task<int> Delete(T entity)
        {
            _personManagerContext.Set<T>().Remove(entity);
            return await _personManagerContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _personManagerContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<int> Update(T entity)
        {
            _personManagerContext.Set<T>().Update(entity);
            return await _personManagerContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _personManagerContext?.Dispose();
        }
    }
}
