using System.Collections.Generic;

namespace Visklusa.Notation.Json
{
	public class JsonCapabilityRepository
	{
		private readonly Dictionary<string, IJsonCapabilityConverter> _table = new ();

		public void Register(IJsonCapabilityConverter capability)
		{
			_table.Add(capability.Id, capability);
		}

		public IJsonCapabilityConverter? Get(string capabilityId)
		{
			return _table.GetValueOrDefault(capabilityId);
		}
	}
}
