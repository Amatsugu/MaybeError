using MaybeError.Errors;

using System.Diagnostics.CodeAnalysis;

namespace MaybeError;

public interface IMaybe<T, E> where E : Error
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

public readonly struct MaybeEx<T, Ex> : IMaybe<T, ExceptionError<Ex>> where Ex : Exception
{
	[MemberNotNullWhen(true, nameof(Error))]
	[MemberNotNullWhen(false, nameof(_value))]
	public readonly bool HasError { get; }
	public readonly bool HasValue => !HasError;
	public readonly ExceptionError<Ex>? Error { get; init; }
	public readonly T Value => HasError ? throw Error.GetException() : _value;
	private readonly T? _value;

	/// <summary>
	/// Creates a new <see cref="Maybe{T, Ex}"/> with a default(null) value
	/// </summary>
	public MaybeEx()
	{
		_value = default;
	}

	/// <summary>
	/// Creates a new <see cref="Maybe{T, Ex}"/> with a <paramref name="value"/>
	/// </summary>
	public MaybeEx(T value)
	{
		_value = value;
	}

	/// <summary>
	/// Creates a new <see cref="Maybe{T, Ex}"/> with an exception error <paramref name="e"/>
	/// </summary>
	public MaybeEx(Ex e)
	{
		Error = e;
		HasError = true;
		_value = default;
	}


	public static implicit operator MaybeEx<T, Ex>(T value)
	{
		return new MaybeEx<T, Ex>(value);
	}

	public static implicit operator MaybeEx<T, Ex>(Ex e)
	{
		return new MaybeEx<T, Ex>(e);
	}

	public static implicit operator Maybe<T, ExceptionError<Ex>>(MaybeEx<T, Ex> value)
	{
		return value;
	}

	public static implicit operator Maybe<T>(MaybeEx<T, Ex> value)
	{
		return value;
	}

	public static implicit operator T(MaybeEx<T, Ex> value)
	{
		if (value.HasError)
			throw value.Error.GetException();
		return value.Value;
	}
}

public readonly struct Maybe<T, E> : IMaybe<T, E> where E: Error
{
	[MemberNotNullWhen(true, nameof(Error))]
	[MemberNotNullWhen(false, nameof(_value))]
	public readonly bool HasError { get; }
	public readonly bool HasValue => !HasError;
	public readonly E? Error { get; init; }
	public readonly T Value => HasError ? throw Error.GetException() : _value;

	private readonly T? _value;

	/// <summary>
	/// Creates a new <see cref="Maybe{T, E}"/> with a default(null) value
	/// </summary>
	public Maybe()
	{
		_value = default;
	}

	/// <summary>
	/// Creates a new <see cref="Maybe{T, E}"/> with a <paramref name="value"/>
	/// </summary>
	public Maybe(T value)
	{
		_value = value;
	}

	/// <summary>
	/// Creates a new <see cref="Maybe{T, E}"/> with an error <paramref name="e"/>
	/// </summary>
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

	public static implicit operator Maybe<T>(Maybe<T, E> value)
	{
		return value;
	}

	public static implicit operator T(Maybe<T, E> value)
	{
		if (value.HasError)
			throw value.Error.GetException();
		return value.Value;
	}
}


public readonly struct Maybe<T> : IMaybe<T, Error> 
{
	[MemberNotNullWhen(true, nameof(Error))]
	[MemberNotNullWhen(false, nameof(_value))]
	public readonly bool HasError { get; }
	public readonly bool HasValue => !HasError;
	public readonly Error? Error { get; }
	public readonly T Value => HasError ? throw Error.GetException() : _value;

	private readonly T? _value;

	/// <summary>
	/// Creates a new <see cref="Maybe{T}"/> with a default(null) value
	/// </summary>
	public Maybe()
	{
		_value = default;
	}

	/// <summary>
	/// Creates a new <see cref="Maybe{T}"/> with a <paramref name="value"/>
	/// </summary>
	public Maybe(T value)
	{
		_value = value;
	}

	/// <summary>
	/// Creates a new <see cref="Maybe{T}"/> with an exception <paramref name="e"/>
	/// </summary>
	public Maybe(Exception e)
	{
		Error = new ExceptionError(e);
		HasError = true;
		_value = default;
	}

	/// <summary>
	/// Creates a new <see cref="Maybe{T}"/> with an error <paramref name="e"/>
	/// </summary>
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

