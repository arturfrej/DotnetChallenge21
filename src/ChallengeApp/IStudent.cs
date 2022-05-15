namespace ChallengeApp
{
    public interface IStudent
    {
        public string Name {get;}
        void AddGrade(double grade);
        void ClearGrades();
        Statistics GetStatistics();
        event GradeAddedDelegate GradeAdded;
    }
}