using markdown.Shared.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace markdown.Server.Controllers
{
	public class FileController : Controller
	{
		// GET: api/FileController/GetText
		[HttpGet]
		[Route("api/FileController/GetText")]
		public ActionResult GetText(string fileName)
		{
			string fileText = System.IO.File.ReadAllText($"{Directory.GetCurrentDirectory()}/Data/Documents/{fileName}");
			FileHandler fileHandler = new() { FileText = fileText };
			string json = JsonSerializer.Serialize(fileHandler);
			return Ok(fileHandler);
		}
	}
}
