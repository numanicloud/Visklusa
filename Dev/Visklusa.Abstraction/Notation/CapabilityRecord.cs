using System.Text.Json.Serialization;

namespace Visklusa.Abstraction.Notation
{
	public interface ICapability
	{
		string CapabilityId { get; }
	}

	public abstract record CapabilityRecord(string CapabilityId) : ICapability
	{
		[JsonIgnore]
		public string CapabilityId { get; } = CapabilityId;
	}

	public record NothingCapability() : CapabilityRecord("Visklusa.Nothing");
}
