using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zamazor.DataAccess.Repos.IRepository;
using ZamazorWeb.DataAccess.Data;

namespace Zamazor.DataAccess.Repos
{
	public class Repository<T> : IRepository<T> where T : class
	{
		//dependency injection
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
			_db = db;
			this.dbSet = _db.Set<T>();
			//_db.Categories == dbSet
			_db.Products.Include(u => u.Category).Include(u => u.CategoryId);
			
        }
        public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			query = query.Where(filter);
			return query.FirstOrDefault();
		}

		//Category, CoverType
		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if(!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.ToList();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}
	}
}
