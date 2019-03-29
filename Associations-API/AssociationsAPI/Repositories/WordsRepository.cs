using AssociationsAPI.Data;
using AssociationsAPI.Models;
using AssociationsAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssociationsAPI.Repositories
{
    public class WordsRepository: IWordsRepository
    {
        private readonly DataContext _context;

        public WordsRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Words>> GetWords()
        {
            var words = await _context.Words
                .Include(w => w.Rels)
                .ToListAsync();

            return words;
        }
    }
}
