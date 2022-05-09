namespace ChallengeApp
{
    public interface IStudent
    {
        void AddGrade(double grade);
        void ClearGrades();
        Statistics GetStatistics();
        event GradeAddedDelegate GradeAdded;
    }
}