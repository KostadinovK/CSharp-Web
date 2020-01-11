using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SULS.Models;

namespace SULS.Services
{
    public class ProblemService : IProblemService
    {
        private readonly SULSContext context;

        public ProblemService(SULSContext context)
        {
            this.context = context;
        }

        public IQueryable<Problem> GetAll()
        {
            return context.Problems.AsQueryable();
        }

        public string CreateProblem(string name, int points)
        {
            if (context.Problems.Any(p => p.Name == name && p.Points == points))
            {
                return null;
            }

            var problem = new Problem()
            {
                Name = name,
                Points = points,
            };

            context.Problems.Add(problem);
            context.SaveChanges();

            return problem.Id;
        }

        public Problem GetById(string id)
        {
            return context.Problems.Include(p => p.Submissions).ThenInclude(s => s.User).SingleOrDefault(p => p.Id == id);
        }
    }
}
