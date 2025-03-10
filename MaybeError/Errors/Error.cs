using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaybeError.Errors;

/// <summary>
/// An information about an error that occured
/// </summary>
/// <param name="message">Message</param>
/// <param name="details">Detailed information of the error</param>
/// <param name="devDetails">Even more detailed information that is useful for logging</param>
public class Error(string message, string? details = null, string? devDetails = null)
{
	public readonly string Message = message;
	public readonly string? Details = details;
	public readonly string? DevDetails = devDetails;

#if DEBUG
	private readonly StackTrace _stackTrace = new();
#endif

	public virtual Exception GetException()
	{
#if DEBUG
		return new Exception(ToString());
#else
		return new Exception(Message);
#endif
	}

	public override string ToString()
	{
#if DEBUG
		return $"{Message}\n{Details}\n{DevDetails}\nTrace:{_stackTrace}";
#else
		return $"{Message}\n{Details}";
#endif
	}

	public static implicit operator string(Error e)
	{
		return e.ToString();
	}
}

