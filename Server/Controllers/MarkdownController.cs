using markdown.Server.Data.Repositories;
using markdown.Shared.Models;
using markdown.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace markdown.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MarkdownController : Controller
	{
		private readonly IMarkdownRepository _markdownRepository;

		public MarkdownController(IMarkdownRepository markdownRepository)
		{
			this._markdownRepository = markdownRepository ?? throw new ArgumentNullException(nameof(markdownRepository));
		}

		// GET: api/<MarkdownController>/GetMarkdown
		[HttpGet]
		public async Task<ActionResult> GetMarkdown(Guid markdownId = default, Guid userId = default)
		{
			try
			{
				Markdown markdown = await _markdownRepository.GetMarkdown(markdownId, userId);
				return Ok(markdown);
			}
			catch (ArgumentNullException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// POST api/<MarkdownController>/PostMarkdown
		[HttpPost]
		public async Task<ActionResult> PostMarkdown(CreateMarkdownDto markdownDto)
		{
			Markdown markdownToCreate = new Markdown() 
			{ 
				Content = markdownDto.Content,
				DateTime = markdownDto.DateTime,
				UserId = markdownDto.UserId,
			};
			try
			{
				await _markdownRepository.AddMarkdown(markdownToCreate);
				return Ok("Markdown added");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// PUT api/<MarkdownController>/PutMarkdown
		[HttpPut]
		public async Task<ActionResult> PutMarkdown(Markdown markdown)
		{
			try
			{
				await _markdownRepository.UpdateMarkdown(markdown);
				return Ok("Markdown updated");
			}
			catch (ArgumentNullException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// DELETE api/<MarkdownController>/DeleteMarkdown
		[HttpDelete]
		public async Task<ActionResult> DeleteMarkdown(Guid markdownId = default, Guid userId = default)
		{
			try
			{
				await _markdownRepository.DeleteMarkdown(markdownId, userId);
				return Ok("Markdown deleted");
			}
			catch (ArgumentNullException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
