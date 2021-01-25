using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Repository
{
    public class MarketInfoRepository : GenericRepository<MarketInfo>, IMarketInfoRepository
    {
        public MarketInfoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
