using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SULS.Models;

namespace SULS.Services
{
    public interface IProblemService
    {
        IQueryable<Problem> GetAll();

        string CreateProblem(string name, int points);

        Problem GetById(string id);
    }
}
