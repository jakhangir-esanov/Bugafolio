namespace FinancialPortfolioManagement.Service.Exceptions;

public class InsufficientAmount : Exception
{
    public InsufficientAmount(string excpetion) : base(excpetion) { }

    public InsufficientAmount(string excpetion, Exception innerException) : base(excpetion, innerException) { }
}
