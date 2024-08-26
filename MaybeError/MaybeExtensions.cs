#nullable enable

namespace MaybeError;

public static class MaybeExtensions
{
	public static T ValueOrDefault<T, E>(this IValueMaybe<T, E> maybe, T defaultValue) where T : struct where E : Error
	{
		if (maybe.HasError)
			return defaultValue;
		return maybe.Value;
	}

	public static T? ValueOrDefault<T, E>(this IValueMaybe<T, E> maybe) where T : struct where E : Error
	{
		if (maybe.HasError)
			return default;
		return maybe.Value;
	}

	public static R As<R, T, E>(this IValueMaybe<T, E> maybe, Func<T, R> predicate) where T : struct where E : Error
	{
		return predicate(maybe.Value);
	}

	public static R? AsOrDefault<R, T, E>(this IValueMaybe<T, E> maybe, Func<T, R> predicate) where T : struct where E : Error
	{
		if (maybe.HasError)
			return default;
		return predicate(maybe.Value);
	}

	public static R AsOrDefault<R, T, E>(this IValueMaybe<T, E> maybe, Func<T, R> predicate, R defaultValue) where T : struct where E : Error
	{
		if (maybe.HasError)
			return defaultValue;
		return predicate(maybe.Value);
	}


	public static T ValueOrDefault<T, E>(this IMaybe<T, E> maybe, T defaultValue) where T : class where E : Error
	{
		if (maybe.HasError)
			return defaultValue;
		return maybe.Value;
	}

	public static T? ValueOrDefault<T, E>(this IMaybe<T, E> maybe) where T : class where E : Error
	{
		if (maybe.HasError)
			return default;
		return maybe.Value;
	}

	public static R As<R, T, E>(this IMaybe<T, E> maybe, Func<T, R> predicate) where T : class where E : Error
	{
		return predicate(maybe.Value);
	}

	public static R? AsOrDefault<R, T, E>(this IMaybe<T, E> maybe, Func<T, R> predicate) where T : class where E : Error
	{
		if (maybe.HasError)
			return default;
		return predicate(maybe.Value);
	}

	public static R AsOrDefault<R, T, E>(this IMaybe<T, E> maybe, Func<T, R> predicate, R defaultValue) where T : class where E : Error
	{
		if (maybe.HasError)
			return defaultValue;
		return predicate(maybe.Value);
	}
}
