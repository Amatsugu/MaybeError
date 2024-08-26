using MaybeError;
using MaybeError.Errors;

namespace MaybeErrorTests;

public class MaybeTests
{
	[Test]
	public void HasValue()
	{
		Maybe<string> value = "Test";
		if (value.HasValue)
			Assert.That(value.Value, Is.EqualTo("Test"));
		else
			Assert.Fail();
		Assert.That(value.Error, Is.Null);
	}

	[Test]
	public void HasError()
	{
		Maybe<string> value = new InvalidOperationException();
		if (value.HasError)
			Assert.That(value.Error, Is.Not.Null);
		else
			Assert.Fail();
		Assert.Throws<InvalidOperationException>(() => value.Value.ToLower());
	}

	[Test]
	public void ImplicitValue()
	{
		Maybe<string> value = "Test";
		Assert.Multiple(() =>
		{
			Assert.That(value.HasValue);
			Assert.That(value == "Test", Is.True);
		});
	}

	[Test]
	public void ImplicitError()
	{
		Maybe<string> value = new InvalidOperationException();
		Assert.That(value.HasError);
		Assert.Throws<InvalidOperationException>(() => value.Value.ToString());
	}

	[Test]
	public void ExplicitError()
	{
		Maybe<string, ExceptionError> value = new ExceptionError(new InvalidOperationException());
		Assert.That(value.HasError);
		Assert.Throws<InvalidOperationException>(() => value.Value.ToString());
	}

	[Test]
	public void Error()
	{
		Maybe<string> value = new Error("Test Error");
		Assert.Throws<Exception>(() => value.Value.ToLower());
		Assert.Multiple(() =>
		{
			Assert.That(value.HasError);
			Assert.That(value.Error?.Message, Is.EqualTo("Test Error"));
		});
	}

	[Test]
	public void ValueOrDefault()
	{
		Maybe<string> value = "Test";
		Assert.That(value.ValueOrDefault(), Is.EqualTo("Test"));

		value = new InvalidOperationException();
		Assert.That(value.ValueOrDefault(), Is.Null);
	}

	[Test]
	public void ValueOrDefaultWithDefault()
	{
		Maybe<string> value = "Test";
		Assert.That(value.ValueOrDefault("Err"), Is.EqualTo("Test"));

		value = new InvalidOperationException();
		Assert.That(value.ValueOrDefault("Err"), Is.EqualTo("Err"));
	}

	[Test]
	public void As()
	{
		Maybe<string> value = "Test";
		Assert.That(value.As(v => v.Length), Is.EqualTo(4));

		value = new InvalidOperationException();
		Assert.Throws<InvalidOperationException>(() => value.As(v => v.Length));
	}

	[Test]
	public void AsOrDefault()
	{
		Maybe<string> value = "Test";
		Assert.That(value.AsOrDefault(v => v.Length), Is.EqualTo(4));

		value = new InvalidOperationException();
		Assert.That(value.AsOrDefault(v => v.Length), Is.EqualTo(0));
	}

	[Test]
	public void AsOrDefaultWithValue()
	{
		Maybe<string> value = "Test";
		Assert.That(value.AsOrDefault(v => v.Length, -4), Is.EqualTo(4));

		value = new InvalidOperationException();
		Assert.That(value.AsOrDefault(v => v.Length, -4), Is.EqualTo(-4));
	}
}