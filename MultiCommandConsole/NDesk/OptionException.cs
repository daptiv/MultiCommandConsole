using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Mono.Options
{
	[Serializable]
	public class OptionException : Exception
	{
		public string OptionName { get; private set; }

		public OptionException()
		{
		}

		public OptionException(string message, string optionName)
			: base(message)
		{
			this.OptionName = optionName;
		}

		public OptionException(string message, string optionName, Exception innerException)
			: base(message, innerException)
		{
			this.OptionName = optionName;
		}

		protected OptionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.OptionName = info.GetString("OptionName");
		}

		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("OptionName", OptionName);
		}
	}
}