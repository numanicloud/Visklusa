using Visklusa.IO;
using Visklusa.Notation.Json;

namespace Visklusa.JsonZip
{
	public class ViskJzFactory
	{
		public static VisklusaLoader GetLoader
			(string packagePath, JsonCapabilityRepository repository)
		{
			var variant = new JsonZipVariant(packagePath, repository);
			return new VisklusaLoader(variant);
		}

		public static VisklusaSaver GetSaver
			(string packagePath, JsonCapabilityRepository repository)
		{
			var variant = new JsonZipVariant(packagePath, repository);
			return new VisklusaSaver(variant);
		}
	}
}
