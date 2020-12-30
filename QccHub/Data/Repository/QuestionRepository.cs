using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QccHub.Data.Repository
{
    public class QuestionRepository : GenericRepository<Question> , IQuestionRepository
    {
        private ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Answers> AddAnswer(Answers answer)
        {
            answer.CreatedBy = answer.UserID;
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return await Task.FromResult(answer);
        }

        public async Task<string> DeleteAnswer(int answerID)
        {
            var record = _context.Answers.Find(answerID);
            if (record.IsDeleted == true)
            {
                return await Task.FromResult("Answer for this ID not found");
            }
            record.IsDeleted = true;
            await _context.SaveChangesAsync();
            return await Task.FromResult("is Deleted");
        }

        public async Task<Answers> EditAnswer(int answerID, Answers answers)
        {
            var record = _context.Answers.Find(answerID);
            record.QuestionID = answers.QuestionID;
            record.Text = answers.Text;
            await _context.SaveChangesAsync();
            return await Task.FromResult(record);
        }

        public override Task<List<Question>> GetAllAsync()
        {
            return _context.Question.Where(a => a.IsDeleted == false).OrderByDescending(q => q.CreatedDate).Include(q => q.User).ToListAsync();
        }

        public Answers GetAnswerByID(int answerID)
        {
            Answers answer = _context.Answers.Find(answerID);
            return answer;
        }

        public async Task<IEnumerable<Answers>> GetQuestionAnswers(int questionID)
        {
            IEnumerable<Answers> answers = _context.Answers.Where(a => a.QuestionID == questionID).Include(a => a.Question).Include(a=>a.User);
            return await Task.FromResult(answers);
        }
    }
}
