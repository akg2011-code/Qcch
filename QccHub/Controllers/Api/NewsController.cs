using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using QccHub.DTOS;
using QccHub.Logic.Helpers;

namespace QccHub.Controllers.Api
{
    public class NewsController : BaseApiController
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public NewsController(INewsRepository newsRepository,
            IUnitOfWork unitOfWork,
            CurrentSession currentSession,
            IHttpContextAccessor contextAccessor,
            IUserRepository userRepository) : base(currentSession, contextAccessor)
        {
            _newsRepository = newsRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(News news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _newsRepository.Add(news);
            await _unitOfWork.SaveChangesAsync();
            return Created("news added", news);
        }

        //[HttpPut("{newsID}")]
        //public async Task<IActionResult> EditNews(int newsID, News editedNews)
        //{
        //    News news = await _newsRepository.GetByIdAsync(newsID);
        //    if (news == null && news.IsDeleted == false)
        //    {
        //        return NotFound("No news for this ID");
        //    }
        //    news.Time = editedNews.Time;
        //    news.Title = editedNews.Title;
        //    news.Details = editedNews.Details;
        //    await _unitOfWork.SaveChangesAsync();
        //    return Ok("news edited");
        //}

        [HttpDelete("{newsID}")]
        public async Task<IActionResult> DeleteNews(int newsID)
        {
            News news = await _newsRepository.GetByIdAsync(newsID);
            if (news == null && news.IsDeleted == false)
            {
                return NotFound("No news for this ID");
            }
            news.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return Ok("news is deleted");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var news = await _newsRepository.GetAllAsync();
            return Ok(news);
        }

        [HttpGet("{newsID}")]
        public async Task<IActionResult> GetNewsByID(int newsID)
        {
            var news =await _newsRepository.GetByIdAsync(newsID);
            if (news == null || news.IsDeleted == true)
            {
                return NotFound("no news for this ID");
            }
            return Ok(news);
        }

        // ----------------------------- news users and comments -------------------------------
        [HttpGet("{newsID}")]
        public async Task<IActionResult> GetNewsComments(int newsID)
        {
            News news = await _newsRepository.GetByIdAsync(newsID);
            if (news == null || news.IsDeleted == true)
            {
                return NotFound("No news for this ID");
            }
            return Ok(_newsRepository.GetNewsComments(newsID));
        }

        [HttpGet("{commentID}")]
        public IActionResult GetCommentByID(int commentID)
        {
            var comment = _newsRepository.GetCommentByID(commentID);
            if (comment == null || comment.IsDeleted == true)
            {
                return NotFound("No comment for this ID");
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(NewsDTO DTO)
        {
            //var news = _newsRepository.GetByIdAsync(DTO.NewsID);
            //var user = _userRepository.GetUserByIdAsync(DTO.UserID);
            //if (news.Result == null || user.Result == null || news.Result.IsDeleted == true)
            //{
            //    return NotFound();
            //}
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            NewsComments comment = new NewsComments
            {
                NewsID = DTO.NewsID,
                UserID = DTO.UserID,
                Comment = DTO.Comment,
                CreatedBy = DTO.UserID,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Time = DateTime.Now
            };
            await _newsRepository.AddComment(comment);
            await _unitOfWork.SaveChangesAsync();
            return Created("comment is added",comment);
        }

        [HttpDelete("{commentID}")]
        public async Task<IActionResult> DeleteComment(int commentID)
        {
            NewsComments comment = _newsRepository.GetCommentByID(commentID);
            if (comment == null)
            {
                return NotFound("No comment for this ID");
            }
            var result = await _newsRepository.DeleteComment(commentID);
            return Ok(result);
        }

        [HttpPut("{commentID}")]
        public async Task<IActionResult> EditComment(int commentID , NewsComments comment)
        {
            var _comment = _newsRepository.GetCommentByID(commentID);
            if (_comment == null || _comment.IsDeleted == true)
            {
                return NotFound("No comment for this ID");
            }
            var result = await _newsRepository.EditComment(commentID, comment);
            return Ok(result);
        }
    }
}