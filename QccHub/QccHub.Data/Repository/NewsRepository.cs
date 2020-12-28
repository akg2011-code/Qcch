using Microsoft.EntityFrameworkCore;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QccHub.Data.Repository
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        private ApplicationDbContext _context;
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<NewsComments> AddComment(NewsComments comment)
        {
            comment.CreatedDate = DateTime.Now;
            comment.CreatedBy = comment.UserID;
            comment.IsDeleted = false;
            _context.NewsComments.Add(comment);
            await _context.SaveChangesAsync();
            return await Task.FromResult(comment);
        }

        public async Task<string> DeleteComment(int commentID)
        {
            var record = _context.NewsComments.Find(commentID);
            if (record.IsDeleted == true)
            {
                return await Task.FromResult("this Comment for this ID not found");
            }
            record.IsDeleted = true;
            await _context.SaveChangesAsync();
            return await Task.FromResult("is Deleted");
        }

        public async Task<string> EditComment(int commentID, NewsComments newsComment)
        {
            var record = _context.NewsComments.Find(commentID);
            if (record == null || record.IsDeleted == true)
            {
                return await Task.FromResult("can't find this comment");
            }
            record.Comment = newsComment.Comment;
            record.Time = newsComment.Time;

            await _context.SaveChangesAsync();
            return await Task.FromResult("comment edited");
        }

        public NewsComments GetCommentByID(int commentID)
        {
            var answer = _context.NewsComments.Find(commentID);
            return answer;
        }

        public async Task<IEnumerable<NewsComments>> GetNewsComments(int newsID)
        {
            IEnumerable<NewsComments> comments = _context.NewsComments.Where(c=>c.NewsID == newsID).Include(c=>c.News).Include(c=>c.User);
            return await Task.FromResult(comments);
        }
    }
}