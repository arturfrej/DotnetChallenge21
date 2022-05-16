using System.Collections.Generic;

namespace ChallengeApp
{
    public interface IStudent
    {
        public string Name {get;}
        void AddGrade(double grade);
        void AddGrade(string grade);
        void ClearGrades();
        public abstract List<double> GetGrades();
        Statistics GetStatistics();
        event GradeAddedDelegate GradeAdded;
    }
}