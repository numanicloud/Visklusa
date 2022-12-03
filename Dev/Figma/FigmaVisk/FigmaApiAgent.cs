using System.Threading.Tasks;
using FigmaSharp;
using FigmaSharp.Models;

namespace FigmaVisk;

internal class FigmaApiAgent
{
	public async Task<FigmaDocument> Download(StartupOption option)
	{
		var query = new FigmaFileQuery(option.FileId, option.Token);
		var response = await AppContext.Api.GetFileAsync(query);
		return response.document;
	}
}