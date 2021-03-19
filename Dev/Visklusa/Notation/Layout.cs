using System.Linq;
using System.Text;

namespace Visklusa.Notation
{
	public record Layout(CapabilityAssertion Assertion, Element[] Elements);

	public record CapabilityAssertion(string[] Assertions)
	{
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			builder.AppendJoin(",", Assertions);
			return true;
		}
	}

	public record Element(int Id, ICapability[] Capabilities)
	{
		public TCapability? GetCapability<TCapability>()
			where TCapability : ICapability
		{
			return Capabilities.OfType<TCapability>().FirstOrDefault();
		}

		protected virtual bool PrintMembers(StringBuilder builder)
		{
			var join = string.Join(", ", Capabilities.Select(x => x.ToString()));
			builder.Append($"Id = {Id}, Capabilities = {join}");
			return true;
		}
	}
}
