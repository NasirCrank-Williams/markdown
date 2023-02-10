using markdown.Shared.Models;

namespace markdown.Server.Data.Repositories
{
	public interface IMarkdownRepository
	{
		public Task<Markdown> GetMarkdown(Guid markdownId, Guid userId);
		public Task AddMarkdown(Markdown markdown);
		public Task UpdateMarkdown(Markdown markdown);
		public Task DeleteMarkdown(Guid markdownId, Guid userId);
	}
}
