using Associations.DataAccess.Data;
using Associations.DataAccess.Interfaces;
using Associations.DataAccess.Interfaces.Repositories;
using Associations.DataAccess.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AssociationsDbContext _context;
        private readonly IMapper _mapper;

        private IWordsRepository _wordsRepository;
        private IWordRelsRepository _wordRelsRepository;

        public IWordsRepository WordsRepository
        {
            get
            {
                if (_wordsRepository == null)
                {
                    _wordsRepository = new WordsRepository(_context, _mapper);
                }

                return _wordsRepository;
            }
        }

        public IWordRelsRepository WordRelsRepository
        {
            get
            {
                if (_wordRelsRepository == null)
                {
                    _wordRelsRepository = new WordRelsRepository(_context, _mapper);
                }

                return _wordRelsRepository;
            }
        }

        public UnitOfWork(AssociationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                var changes = _context.ChangeTracker.Entries().Count(
                    p => p.State == EntityState.Modified || p.State == EntityState.Deleted
                                                         || p.State == EntityState.Added);
                if (changes == 0) return true;

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                var changes = _context.ChangeTracker.Entries().Count(
                    p => p.State == EntityState.Modified || p.State == EntityState.Deleted
                                                         || p.State == EntityState.Added);
                if (changes == 0) return true;

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // DbSet?.Local?.Clear();
                    _context?.Dispose();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AbstractRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
