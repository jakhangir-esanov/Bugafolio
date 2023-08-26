namespace FinancialPortfolioManagement.Service.Exceptions;

public class AlreadyExistException : Exception
{
    public AlreadyExistException(string exception) : base(exception) { }

    public AlreadyExistException(string exception, Exception innerException) : base (exception, innerException) { }
}
