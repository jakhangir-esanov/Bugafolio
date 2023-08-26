namespace FinancialPortfolioManagement.Service.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string exception) : base(exception) { }

    public NotFoundException(string exception, Exception innerException) : base(exception, innerException) { }
}
