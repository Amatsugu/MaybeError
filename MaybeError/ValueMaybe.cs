
using MaybeError.Errors;

using System.Diagnostics.CodeAnalysis;

namespace MaybeError;

public interface IValueMaybe<T, E> where T : struct where E : Error
{
	[MemberNotNullWhen(true, nameof(Error))]
	bool HasError { get; }
	bool HasValue => !HasError;
	E? Error { get; }
	T Value { get; }
}

public readonly struct ValueMaybe<T, E> : IValueMaybe<T, E> where T : struct where E : Error
{
	[MemberNotNullWhen(true, nameof(Error))]
	public readonly bool HasError { get; }
	public readonly bool HasValue => !HasError;
	public readonly E? Error { get; }
	public readonly T Value => HasError ? throw Error.GetException() : _value;

	private readonly T _value;

	/// <summary>
	/// Creates a new <see cref="ValueMaybe{T, E}"/> with a default value
	/// </summary>
	public ValueMaybe()
	{
		_value = default;
	}

	/// <summary>
	/// Creates a new <see cref="ValueMaybe{T, E}"/> with a <paramref name="value"/>
	/// </summary>
	public ValueMaybe(T value)
	{
		_value = value;
		HasError = false;
	}

	/// <summary>
	/// Creates a new <see cref="ValueMaybe{T, E}"/> with an error <paramref name="e"/>
	/// </summary>
	public ValueMaybe(E e)
	{
		Error = e;
		HasError = true;
		_value = default;
	}

	public static implicit operator ValueMaybe<T, E>(T value)
	{
		return new ValueMaybe<T, E>(value);
	}

	public static implicit operator ValueMaybe<T, E>(E e)
	{
		return new ValueMaybe<T, E>(e);
	}

	public static implicit operator T(ValueMaybe<T, E> value)
	{
		if (value.HasError)
			throw value.Error.GetException();
		return value.Value;
	}
}

public readonly struct ValueMaybe<T> : IValueMaybe<T, Error> where T : struct
{
	[MemberNotNullWhen(true, nameof(Error))]
	public readonly bool HasError { get; }
	public readonly bool HasValue => !HasError;
	public readonly Error? Error { get; }
	public readonly T Value => HasError ? throw Error.GetException() : _value;

	private readonly T _value;

	/// <summary>
	/// Creates a new <see cref="ValueMaybe{T}"/> with a default value
	/// </summary>
	public ValueMaybe()
	{
		_value = default;
	}

	/// <summary>
	/// Creates a new <see cref="ValueMaybe{T}"/> with a <paramref name="value"/>
	/// </summary>
	public ValueMaybe(T value)
	{
		_value = value;
		HasError = false;
	}

	/// <summary>
	/// Creates a new <see cref="ValueMaybe{T}"/> with an exception <paramref name="e"/>
	/// </summary>
	public ValueMaybe(Exception e)
	{
		Error = new ExceptionError(e);
		HasError = true;
		_value = default;
	}

	/// <summary>
	/// Creates a new <see cref="ValueMaybe{T}"/> with an error <paramref name="e"/>
	/// </summary>
	public ValueMaybe(Error e)
	{
		Error = e;
		HasError = true;
		_value = default;
	}

	public static implicit operator ValueMaybe<T>(T value)
	{
		return new ValueMaybe<T>(value);
	}

	public static implicit operator ValueMaybe<T>(Exception e)
	{
		return new ValueMaybe<T>(e);
	}

	public static implicit operator ValueMaybe<T>(Error e)
	{
		return new ValueMaybe<T>(e);
	}

	public static implicit operator T(ValueMaybe<T> value)
	{
		if (value.HasError)
			throw value.Error.GetException();
		return value.Value;
	}
}