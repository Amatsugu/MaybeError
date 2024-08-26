using System.Diagnostics.CodeAnalysis;

namespace MaybeError;

public interface IMaybe<T, E> where T: class where E : Error
{
	[MemberNotNullWhen(true, nameof(Error))]
	bool HasError { get; }
	bool HasValue { get; }
	E? Error { get; }
	T Value { get; }

	public T ValueOrDefault(T defaultValue)
	{
		if (HasError)
			return defaultValue;
		return Value;
	}

	public T? ValueOrDefault()
	{
		if (HasError)
			return default;
		return Value;
	}

	public R As<R>(Func<T, R> predicate)
	{
		return predicate(Value);
	}

	public R? AsOrDefault<R>(Func<T, R> predicate)
	{
		if (HasError)
			return default;
		return predicate(Value);
	}

	public R AsOrDefault<R>(Func<T, R> predicate, R defaultValue)
	{
		if (HasError)
			return defaultValue;
		return predicate(Value);
	}
}

public readonly struct Maybe<T, E> : IMaybe<T, E> where T : class where E: Error
{
	[MemberNotNullWhen(true, nameof(Error))]
	public readonly bool HasError { get; }
	public readonly bool HasValue { get; }
	public readonly E? Error { get; }
	public readonly T Value => HasError ? throw Error.GetException() : _value!;

	private readonly T? _value;

	public Maybe()
	{
		_value = default;
	}

	public Maybe(T value)
	{
		_value = value;
		HasValue = true;
	}

	public Maybe(E e)
	{
		Error = e;
		HasError = true;
		_value = default;
	}

	public static implicit operator Maybe<T, E>(T value)
	{
		return new Maybe<T, E>(value);
	}

	public static implicit operator Maybe<T, E>(E e)
	{
		return new Maybe<T, E>(e);
	}

	public static implicit operator T(Maybe<T, E> value)
	{
		if (value.HasError)
			throw value.Error.GetException();
		return value.Value;
	}
}


public readonly struct Maybe<T> : IMaybe<T, Error> where T : class
{
	[MemberNotNullWhen(true, nameof(Error))]
	public readonly bool HasError { get; }
	public readonly bool HasValue { get; }
	public readonly Error? Error { get; }
	public readonly T Value => HasError ? throw Error.GetException() : _value!;

	private readonly T? _value;

	public Maybe()
	{
		_value = default;
	}

	public Maybe(T value)
	{
		_value = value;
		HasValue = true;
	}

	public Maybe(Exception e)
	{
		Error = new ExceptionError(e);
		HasError = true;
		_value = default;
	}

	public Maybe(Error e)
	{
		Error = e;
		HasError = true;
		_value = default;
	}

	public static implicit operator Maybe<T>(T value)
	{
		return new Maybe<T>(value);
	}

	public static implicit operator Maybe<T>(Exception e)
	{
		return new Maybe<T>(e);
	}

	public static implicit operator Maybe<T>(Error e)
	{
		return new Maybe<T>(e);
	}

	public static implicit operator T(Maybe<T> value)
	{
		if (value.HasError)
			throw value.Error.GetException();
		return value.Value;
	}

	
}

public class Error(string message, string? details = null, string? debugDetails = null)
{
	public readonly string Message = message;
	public readonly string? Details = details;
	public readonly string? DebugDetails = debugDetails;

	public virtual Exception GetException()
	{
		return new Exception(Message);
	}

	public override string ToString()
	{
		return $"{Message}\n{Details}\n{DebugDetails}";
	}

	public static implicit operator string(Error e)
	{
		return e.ToString();
	}
}

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

	public ExceptionError(T exception) : base(exception.Message, debugDetails: exception.StackTrace)
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