using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QccHub.Data.Interfaces
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Answers GetAnswerByID(int answerID);
        Task<IEnumerable<Answers>> GetQuestionAnswers(int questionID);
        Task<Answers> AddAnswer(Answers answer);
        Task<Answers> EditAnswer(int answerID, Answers answers);
        Task<string> DeleteAnswer(int answerID);
    }
}
