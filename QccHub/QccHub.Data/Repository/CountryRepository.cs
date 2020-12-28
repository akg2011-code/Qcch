using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QccHub.Data.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}