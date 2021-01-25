using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using QccHub.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Controllers.Api
{
    public class MarketInfoController : BaseApiController
    {
        private readonly IMarketInfoRepository _marketInfoRepo;
        private readonly IUnitOfWork _unitOfWork;

        public MarketInfoController(IMarketInfoRepository marketRepository,
            IUnitOfWork unitOfWork,
            CurrentSession currentSession,
            IHttpContextAccessor contextAccessor) : base(currentSession, contextAccessor)
        {
            _marketInfoRepo = marketRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Add(MarketInfo marketInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _marketInfoRepo.Add(marketInfo);
            await _unitOfWork.SaveChangesAsync();
            return Created("marketInfo added", marketInfo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var marketInfo = await _marketInfoRepo.GetAllAsync();
            return Ok(marketInfo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var marketInfo = await _marketInfoRepo.GetByIdAsync(id);
            if (marketInfo == null || marketInfo.IsDeleted == true)
            {
                return NotFound("market info not found");
            }
            return Ok(marketInfo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var marketInfo = await _marketInfoRepo.GetByIdAsync(id);
            if (marketInfo == null)
            {
                return NotFound("market info not found");
            }
            _marketInfoRepo.Delete(marketInfo);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
