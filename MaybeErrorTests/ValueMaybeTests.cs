using MaybeError;

namespace MaybeErrorTests;

public class ValueMaybeTests
{
	[Test]
	public void HasValue()
	{
		ValueMaybe<int> value = 1;
		if(value.HasValue)
			Assert.That(value.Value, Is.EqualTo(1));
		else
			Assert.Fail();
		Assert.That(value.Error, Is.Null);
	}

	[Test]
	public void HasError()
	{
		ValueMaybe<int> value = new InvalidOperationException();
		if (value.HasError)
			Assert.That(value.Error, Is.Not.Null);
		else
			Assert.Fail();
		Assert.Throws<InvalidOperationException>(() => value.Value.ToString());
	}

	[Test]
	public void ImplicitValue()
	{
		ValueMaybe<int> value = 1;
		Assert.That(value == 1, Is.True);
	}

	[Test]
	public void ImplicitError()
	{
		ValueMaybe<int> value = new InvalidOperationException();
		Assert.Throws<InvalidOperationException>(() => value.Value.ToString());
	}

	[Test]
	public void ExplicitError()
	{
		ValueMaybe<int, ExceptionError> value = new ExceptionError(new InvalidOperationException());
		Assert.Throws<InvalidOperationException>(() => value.Value.ToString());
	}

	[Test]
	public void Error()
	{
		ValueMaybe<int> value = new Error("Test Error");
		Assert.Throws<Exception>(() => value.Value.ToString());
		Assert.That(value.HasError);
		Assert.That(value.Error?.Message, Is.EqualTo("Test Error"));
	}

	[Test]
	public void ValueOrDefault()
	{
		ValueMaybe<int> value = 1;
		Assert.That(value.ValueOrDefault(), Is.EqualTo(1));

		value = new InvalidOperationException();
		Assert.That(value.ValueOrDefault(), Is.Null);
	}

	[Test]
	public void ValueOrDefaultWithDefault()
	{
		ValueMaybe<int> value = 1;
		Assert.That(value.ValueOrDefault(3), Is.EqualTo(1));

		value = new InvalidOperationException();
		Assert.That(value.ValueOrDefault(3), Is.EqualTo(3));
	}

	[Test]
	public void As()
	{
		ValueMaybe<int> value = 1;
		Assert.That(value.As(v => v.ToString()), Is.EqualTo("1"));

		value = new InvalidOperationException();
		Assert.Throws<InvalidOperationException>(() => value.As(v => v.ToString()));
	}

	[Test]
	public void AsOrDefault()
	{
		ValueMaybe<int> value = 1;
		Assert.That(value.AsOrDefault(v => v.ToString()), Is.EqualTo("1"));

		value = new InvalidOperationException();
		Assert.That(value.AsOrDefault(v => v.ToString()), Is.Null);
	}

	[Test]
	public void AsOrDefaultWithValue()
	{
		ValueMaybe<int> value = 1;
		Assert.That(value.AsOrDefault(v => v.ToString(), "err"), Is.EqualTo("1"));

		value = new InvalidOperationException();
		Assert.That(value.AsOrDefault(v => v.ToString(), "err"), Is.EqualTo("err"));
	}
}