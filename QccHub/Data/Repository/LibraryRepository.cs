using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Repository
{
    public class LibraryRepository : GenericRepository<LibraryItem>, ILibraryRepository
    {
        public LibraryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
