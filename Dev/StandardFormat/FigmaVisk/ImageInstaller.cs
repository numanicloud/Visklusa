using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FigmaVisk.Capability;
using Visklusa.Abstraction.Notation;
using Visklusa.Abstraction.Semantics;
using Visklusa.IO;

namespace FigmaVisk
{
	// ノードに画像パスを結びつけるステップと、画像をダウンロードするステップは独立できるはず
	internal class ImageInstaller
	{
		private readonly ImageRetriever _retriever;

		public ImageInstaller(ImageRetriever retriever)
		{
			_retriever = retriever;
		}

		public IEnumerable<IImageInstallation> Convert(Element[] source)
		{
			foreach (var item in source)
			{
				if (item.GetCapability<ImageRef>() is {} imageRef)
				{
					var element = item with
					{
						Capabilities = item.Capabilities
							.Where(x => x is not ImageRef)
							.Append(new Image($"{imageRef.Url}.png"))
							.ToArray()
					};
					yield return new Load(imageRef.Url, _retriever, element);
				}
				else
				{
					yield return new Skip(item);
				}
			}
		}

		public interface IImageInstallation
		{
			public Element Element { get; }
			ValueTask OnSaveAsync(VisklusaSaver saver);
		}

		public record Load(string ImageRef, ImageRetriever Retriever, Element Element) : IImageInstallation
		{
			public async ValueTask OnSaveAsync(VisklusaSaver saver)
			{
				saver.AddAsset(await Retriever.DownloadAsync(ImageRef), $"{ImageRef}.png");
			}
		}

		public record Skip(Element Element) : IImageInstallation
		{
			public ValueTask OnSaveAsync(VisklusaSaver saver) => ValueTask.CompletedTask;
		}
	}
}
