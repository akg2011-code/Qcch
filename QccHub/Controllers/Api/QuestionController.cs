using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QccHub.Data;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using QccHub.DTOS;
using QccHub.Logic.Helpers;

namespace QccHub.Controllers.Api
{
    public class QuestionController : BaseApiController
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public QuestionController(IQuestionRepository questionRepository,
                                    CurrentSession currentSession,
                                    IHttpContextAccessor contextAccessor,
                                    IUnitOfWork unitOfWork) : base(currentSession,contextAccessor)
        {
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddQustion(QuestionDTO question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("can't add , some info is wrong");
            }
            Question newQuestion = new Question
            {
                CreatedBy = question.UserID,
                UserID = question.UserID,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Title = question.Title
            };
            _questionRepository.Add(newQuestion);
            await _unitOfWork.SaveChangesAsync();
            return Created("question added",question);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var result = await _questionRepository.GetAllAsync();
            return Ok(result); 
        }

        [HttpGet("{questionID}")]
        public async Task<IActionResult> GetQuestion(int questionID)
        {
            var result = await _questionRepository.GetByIdAsync(questionID);
            if (result == null)
            {
                return NotFound("No question for this ID");
            }
            return Ok(result);
        }

        [HttpPut("{questionID}")]
        public async Task<IActionResult> EditQuestion(int questionID , Question editedQuestion)
        {
            Question question = await _questionRepository.GetByIdAsync(questionID);
            if (question == null)
            {
                return NotFound("No question for this ID");
            }

            question.Title = editedQuestion.Title;
            question.UserID = editedQuestion.UserID;
            await _unitOfWork.SaveChangesAsync();
            return Ok("question edited");
        }

        // ------------------------------ Question Answers --------------------------------
        [HttpPost]
        public async Task<IActionResult> AddAnswer(Answers answers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _questionRepository.AddAnswer(answers);
            return Created("answer added", result);
        }

        [HttpGet("{questionID}")]
        public async Task<IActionResult> GetQuestionAnswers(int questionID)
        {
            if (_questionRepository.GetByIdAsync(questionID) == null)
            {
                return NotFound("no question for this ID");
            }
            var result = await _questionRepository.GetQuestionAnswers(questionID);
            return Ok(result);
        }

        [HttpPut("{answerID}")]
        public IActionResult EditAnswer(int answerID,Answers editedAnswer)
        {
            var answer = _questionRepository.GetAnswerByID(answerID);
            if (answer == null)
            {
                return NotFound("No answer for this ID");
            }
            answer.Text = editedAnswer.Text;
            answer.CreatedDate = DateTime.Now;
            return Ok(answer);
        }

        [HttpDelete("{answerID}")]
        public async Task<IActionResult> DeleteAnswer(int answerID)
        {
            Answers answer = _questionRepository.GetAnswerByID(answerID);
            if (answer == null)
            {
                return NotFound("no answer for this ID");
            }
            var result = await _questionRepository.DeleteAnswer(answerID);
            return Ok(result);
        }
    }
}