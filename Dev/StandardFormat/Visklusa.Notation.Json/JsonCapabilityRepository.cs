using System.Collections.Generic;
using Visklusa.Abstraction.Semantics;

namespace Visklusa.Notation.Json;

public class JsonCapabilityRepository
{
	private readonly Dictionary<string, IJsonCapabilityConverter> _table = new ();

	public void Register<T>() where T : ISerializableCapability
	{
		var converter = new JsonCapabilityBase<T>();
		_table.Add(T.IdToRead, converter);
	}

	public IJsonCapabilityConverter? Get(string capabilityId)
	{
		return _table.GetValueOrDefault(capabilityId);
	}
}