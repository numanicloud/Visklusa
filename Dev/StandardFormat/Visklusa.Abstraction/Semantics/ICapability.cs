namespace Visklusa.Abstraction.Semantics;

public interface ICapability
{
	string CapabilityId { get; }
}

public interface IStaticCapability
{
	static abstract string CapabilityId { get; }
}