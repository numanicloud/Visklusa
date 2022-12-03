using System;

namespace Visklusa.Abstraction.Semantics;

public interface ICapability
{
	string Id { get; }
}

public interface ISerializableCapability : ICapability
{
	static abstract string IdToRead { get; }
}