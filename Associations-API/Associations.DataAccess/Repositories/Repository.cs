﻿using Associations.Common.Interfaces;
using Associations.Common.UrlQueries;
using Associations.DataAccess.Data;
using Associations.DataAccess.Interfaces.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Associations.DataAccess.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TKey : IComparable
    {
        protected readonly AssociationsDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected readonly IMapper _mapper;

        public int TotalRecords
        {
            private set;
            get;
        }

        public Repository(AssociationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetRangeAsync(Expression<Func<TEntity, bool>> filter = null,
                                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                       Expression<Func<TEntity, string>> sorting = null,
                                                       bool? sortOrder = null,
                                                       PaginationUrlQuery paginationUrlQuery = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            TotalRecords = await query.CountAsync();

            if (sorting != null)
            {
                if (sortOrder.Value)
                    query = query.OrderBy(sorting);
                else
                    query = query.OrderByDescending(sorting);
            }

            if (paginationUrlQuery != null)
            {
                query = query.Skip(paginationUrlQuery.PageSize * (paginationUrlQuery.PageNumber - 1))
                    .Take(paginationUrlQuery.PageSize);
            }

            return await query.ToListAsync();
        }


        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
                                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> CreateEntityAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public async Task DeleteAsync(TKey Id)
        {
            var entityToDelete = await _dbSet.FindAsync(Id);

            if (entityToDelete == null)
            {
                throw new Exception();
            }

            _dbSet.Remove(entityToDelete);
        }
        public async Task<List<TEntity>> GetAllEntitiesAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetEntityAsync(TKey Id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            query = query.Where(e => e.Id.CompareTo(Id) == 0);

            if (query.Count() < 1)
            {
                throw new Exception();
            }

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entityToUpdate = await _dbSet.FindAsync(entity.Id);

            if (entityToUpdate == null)
            {
                throw new Exception();
            }

            return _mapper.Map(entity, entityToUpdate);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _dbSet.AsNoTracking().CountAsync();
            }

            return _dbSet.AsNoTracking().CountAsync(predicate);
        }

    }
}
