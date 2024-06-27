using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazor.Models;

namespace Zamazor.DataAccess.Repos.IRepository
{
	public interface IShoppingCartRepository : IRepository<ShoppingCart>
	{
		void Update(ShoppingCart obj);
	}
}
