﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazor.DataAccess.Repos.IRepository
{
	public interface IUnitOfWork
	{

		ICategoryRepository Category { get; }
		IProductRepository Product { get; }
		ICompanyRepository Company { get; }
		IShoppingCartRepository ShoppingCart { get; }
		IApplicationUserRepository ApplicationUser { get; }

		void Save();
	}
}
