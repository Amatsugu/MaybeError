namespace MaybeError.Errors;

public class ExceptionError : ExceptionError<Exception>
{
	public ExceptionError(string message, Exception exception) : base(message, exception)
	{
	}
	public ExceptionError(Exception exception) : base(exception)
	{
	}
}

public class ExceptionError<T> : Error where T : Exception
{
	public T Exception { get; set; }

	public ExceptionError(string message, T exception) : base(message, exception.Message, exception.StackTrace)
	{
		Exception = exception;
	}

	public ExceptionError(T exception) : base(exception.Message, devDetails: exception.StackTrace)
	{
		Exception = exception;
	}

	public override T GetException()
	{
		return Exception;
	}

	public override string ToString()
	{
		return $"{Message}\n{Exception}";
	}
}