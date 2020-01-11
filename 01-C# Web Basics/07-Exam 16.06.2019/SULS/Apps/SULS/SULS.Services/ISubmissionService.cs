using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Services
{
    public interface ISubmissionService
    {
        string CreateSubmissionToProblem(string code, string problemId, string userId);
    }
}
