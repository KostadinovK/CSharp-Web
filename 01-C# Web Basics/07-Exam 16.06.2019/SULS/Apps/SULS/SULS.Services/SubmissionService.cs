using System;
using System.Collections.Generic;
using System.Text;
using SULS.Data;
using SULS.Models;

namespace SULS.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly SULSContext context;
        private readonly IProblemService problemService;

        public SubmissionService(SULSContext context, IProblemService problemService)
        {
            this.context = context;
            this.problemService = problemService;
        }

        public string CreateSubmissionToProblem(string code, string problemId, string userId)
        {
            var random = new Random();

            var problem = problemService.GetById(problemId);

            var submission = new Submission
            {
                Code = code,
                ProblemId = problemId,
                UserId = userId,
                User = context.Users.Find(userId),
                CreatedOn = DateTime.UtcNow,
                AchievedResult = random.Next(0, problem.Points)
            };

            context.Submissions.Add(submission);
            context.SaveChanges();

            return submission.Id;
        }
    }
}
