using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QccHub.Data.Interfaces
{
    public interface INewsRepository : IGenericRepository<News>
    {
        NewsComments GetCommentByID(int commentID);
        Task<IEnumerable<NewsComments>> GetNewsComments(int newsID);
        Task<NewsComments> AddComment(NewsComments comment);
        Task<string> EditComment(int commentID, NewsComments newsComment);
        Task<string> DeleteComment(int commentID);
    }
}