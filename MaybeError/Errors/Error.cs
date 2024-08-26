using System;
using System.Collections.Generic;
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

	public virtual Exception GetException()
	{
		return new Exception(Message);
	}

	public override string ToString()
	{
#if DEBUG
		return $"{Message}\n{Details}\n{DevDetails}";
#else
		return $"{Message}\n{Details}";
#endif
	}

	public static implicit operator string(Error e)
	{
		return e.ToString();
	}
}

