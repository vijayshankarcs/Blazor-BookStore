using BookStore_API.Contracts;
using BookStore_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _db;
        public AuthorRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Author entity)
        {
            //throw new NotImplementedException();
            await _db.Authors.AddAsync(entity);
            return await Save();

        }

        public async Task<bool> Delete(Author entity)
        {
            //throw new NotImplementedException();
            _db.Authors.Remove(entity);
            return await Save();
        }

        public async Task<IList<Author>> FindAll()
        {
            // throw new NotImplementedException();
            var authers = await _db.Authors.ToListAsync();
            return authers;
        }

        public async Task<Author> FindById(int id)
        {
            //  throw new NotImplementedException();
            var author = await _db.Authors.FindAsync(id);
            return author;
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Authors.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            //throw new NotImplementedException();
            var chanages = await _db.SaveChangesAsync();
            return chanages > 0;
        }

        public async Task<bool> Update(Author entity)
        {
            //throw new NotImplementedException();

            _db.Authors.Update(entity);
            return await Save();
        }
    }
}
