using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazor.DataAccess.Repos.IRepository;
using ZamazorWeb.DataAccess.Data;

namespace Zamazor.DataAccess.Repos
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _db;
		public ICategoryRepository Category {get; private set;}

        public IProductRepository Product {get; private set;}

        public ICompanyRepository Company { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);
			Product = new ProductRepository(_db);
			Company = new CompanyRepository(_db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
