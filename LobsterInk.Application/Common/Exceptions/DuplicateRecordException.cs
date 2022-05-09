namespace LobsterInk.Application.Common.Exceptions;

public class DuplicateRecordException : Exception
{
    public DuplicateRecordException(string name, string message)
        : base($"Entity \"{name}\" with ({message}) already exists.")
    {
    }
}