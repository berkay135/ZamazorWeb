using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zamazor.DataAccess.Repos.IRepository;
using Zamazor.Models;
using ZamazorWeb.DataAccess.Data;

namespace Zamazor.DataAccess.Repos
{
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
		private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
	}
}
