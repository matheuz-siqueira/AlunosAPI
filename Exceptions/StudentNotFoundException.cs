namespace AlunosAPI.Exceptions;

public class StudentNotFoundException : Exception
{
    public StudentNotFoundException(string message) : base(message)
    { }
}
