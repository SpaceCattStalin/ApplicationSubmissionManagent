using Application.Common.Interfaces;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
                return (IGenericRepository<T>)_repositories[typeof(T)];

            var repository = typeof(T).Name switch
            {
                nameof(Campus) => (IGenericRepository<T>)new GenericRepository<Campus>(_context.Campus),
                nameof(AcademicField) => (IGenericRepository<T>)new GenericRepository<AcademicField>(_context.AcademicFields),
                nameof(Specialization) => (IGenericRepository<T>)new GenericRepository<Specialization>(_context.Specializations),
                nameof(CampusAcademicField) => (IGenericRepository<T>)new GenericRepository<CampusAcademicField>(_context.CampusAcademicFields),
                nameof(Fee) => (IGenericRepository<T>)new GenericRepository<Fee>(_context.Fees),
                nameof(SpecializationFee) => (IGenericRepository<T>)new GenericRepository<SpecializationFee>(_context.SpecializationFees),
                _ => throw new ArgumentException($"Repository for {typeof(T).Name} is not supported.")
            };

            _repositories[typeof(T)] = repository;
            return repository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            (_context as IDisposable)?.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public async Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>> include = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);
            return await query.FirstOrDefaultAsync(x => EF.Property<Guid>(x, "Id") == id, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> include = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);
            return await query.ToListAsync(cancellationToken);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}