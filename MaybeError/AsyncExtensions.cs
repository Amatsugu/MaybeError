using MaybeError.Errors;

namespace MaybeError;

public static class AsyncExtensions
{
	#region ValueMaybe

	public static async Task<T> ValueOrDefaultAsync<T, E>(this Task<IValueMaybe<T, E>> task, T defaultValue) where T : struct where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.ValueOrDefault(defaultValue);
	}

	public static async ValueTask<T> ValueOrDefaultAsync<T, E>(this ValueTask<IValueMaybe<T, E>> task, T defaultValue) where T : struct where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.ValueOrDefault(defaultValue);
	}

	public static async Task<T?> ValueOrDefaultAsync<T, E>(this Task<IValueMaybe<T, E>> task) where T : struct where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.ValueOrDefault();
	}

	public static async ValueTask<T?> ValueOrDefaultAsync<T, E>(this ValueTask<IValueMaybe<T, E>> task) where T : struct where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.ValueOrDefault();
	}

	public static async Task<R> AsAsync<R, T, E>(this Task<IValueMaybe<T, E>> task, Func<T, R> predicate) where T : struct where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.As(predicate);
	}

	public static async ValueTask<R> AsAsync<R, T, E>(this ValueTask<IValueMaybe<T, E>> task, Func<T, R> predicate) where T : struct where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.As(predicate);
	}

	public static async Task<R?> AsOrDefaultAsync<R, T, E>(this Task<IValueMaybe<T, E>> task, Func<T, R> predicate) where T : struct where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.AsOrDefault(predicate);
	}

	public static async ValueTask<R?> AsOrDefaultAsync<R, T, E>(this ValueTask<IValueMaybe<T, E>> task, Func<T, R> predicate) where T : struct where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.AsOrDefault(predicate);
	}

	public static async Task<R> AsOrDefaultAsync<R, T, E>(this Task<IValueMaybe<T, E>> task, Func<T, R> predicate, R defaultValue) where T : struct where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.AsOrDefault(predicate, defaultValue);
	}

	public static async ValueTask<R> AsOrDefaultAsync<R, T, E>(this ValueTask<IValueMaybe<T, E>> task, Func<T, R> predicate, R defaultValue) where T : struct where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.AsOrDefault(predicate, defaultValue);
	}

	#endregion ValueMaybe

	#region Maybe

	public static async Task<T> ValueOrDefaultAsync<T, E>(this Task<IMaybe<T, E>> task, T defaultValue) where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.ValueOrDefault(defaultValue);
	}

	public static async Task<T?> ValueOrDefaultAsync<T, E>(this Task<IMaybe<T, E>> task) where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.ValueOrDefault();
	}

	public static async Task<R> AsAsync<R, T, E>(this Task<IMaybe<T, E>> task, Func<T, R> predicate) where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.As(predicate);
	}

	public static async Task<R?> AsOrDefaultAsync<R, T, E>(this Task<IMaybe<T, E>> task, Func<T, R> predicate) where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.AsOrDefault(predicate);
	}

	public static async Task<R> AsOrDefaultAsync<R, T, E>(this Task<IMaybe<T, E>> task, Func<T, R> predicate, R defaultValue) where E : Error
	{
		var maybe = await task.ConfigureAwait(false);
		return maybe.AsOrDefault(predicate, defaultValue);
	}

	#endregion Maybe
}