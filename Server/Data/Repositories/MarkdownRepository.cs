using markdown.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace markdown.Server.Data.Repositories
{
	public class MarkdownRepository : IMarkdownRepository
	{
		private readonly UserMarkdownDbContext _userMarkdownDbContext;

		public MarkdownRepository(UserMarkdownDbContext userMarkdownDbContext)
		{
			this._userMarkdownDbContext = userMarkdownDbContext ?? throw new ArgumentNullException(nameof(userMarkdownDbContext));
		}

		public Task AddMarkdown(Markdown markdown)
		{
			try
			{
				markdown.MarkdownId = Guid.NewGuid();
				_userMarkdownDbContext.Markdowns.Add(markdown);
				_userMarkdownDbContext.SaveChanges();
			}
			catch
			{
				throw new Exception("Failed to create markdown");
			}

			return Task.CompletedTask;
		}

		public async Task DeleteMarkdown(Guid markdownId = default, Guid userId = default)
		{
			// TRUE = parameter is not empty, FALSE = parameter is empty
			bool isMarkdownId = !Guid.Empty.Equals(markdownId);
			bool isUserId = !Guid.Empty.Equals(userId);

			try
			{
				Markdown? markdown = null;
				switch ((isMarkdownId, isUserId))
				{
					case (true, false):
						markdown = await _userMarkdownDbContext.Markdowns.FirstOrDefaultAsync(mark => mark.MarkdownId == markdownId);
						break;
					case (false, true):
						markdown = await _userMarkdownDbContext.Markdowns.FirstOrDefaultAsync(mark => mark.UserId == userId);
						break;
					case (true, true):
						markdown = await _userMarkdownDbContext.Markdowns.FirstOrDefaultAsync(mark => mark.MarkdownId == markdownId && mark.UserId == userId);
						break;
				}
				if (markdown != null)
				{
					_userMarkdownDbContext.Markdowns.Remove(markdown);
				}
				else
				{
					throw new ArgumentNullException();
				}
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("Markdown does not exist");
			}
			catch (Exception)
			{
				throw new Exception("Failed to delete markdown");
			}
		}

		public async Task<Markdown> GetMarkdown(Guid markdownId = default, Guid userId = default)
		{
			// TRUE = parameter is not empty, FALSE = parameter is empty
			bool isMarkdownId = !Guid.Empty.Equals(markdownId);
			bool isUserId = !Guid.Empty.Equals(userId);

			try
			{
				Markdown? markdown = null;
				switch ((isMarkdownId, isUserId))
				{
					case (true, false):
						markdown = await _userMarkdownDbContext.Markdowns.FirstOrDefaultAsync(mark => mark.MarkdownId == markdownId);
						break;
					case (false, true):
						markdown = await _userMarkdownDbContext.Markdowns.FirstOrDefaultAsync(mark => mark.UserId == userId);
						break;
					case (true, true):
						markdown = await _userMarkdownDbContext.Markdowns.FirstOrDefaultAsync(mark => mark.MarkdownId == markdownId && mark.UserId == userId);
						break;
				}
				if (markdown != null)
				{
					return markdown;
				}
				else
				{
					throw new ArgumentNullException();
				}
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("Failed to find markdown");
			}
			catch (Exception)
			{
				throw new Exception("Failed to get markdown");
			}
		}

		public async Task UpdateMarkdown(Markdown markdown)
		{
			try
			{
				Markdown? markdownToUpdate = await _userMarkdownDbContext.Markdowns.FirstOrDefaultAsync(mark => mark.MarkdownId == markdown.MarkdownId);
				if (markdownToUpdate != null)
				{
					markdownToUpdate.Content = markdown.Content;
					markdownToUpdate.DateTime = markdown.DateTime;

					_userMarkdownDbContext.Markdowns.Update(markdownToUpdate);
					_userMarkdownDbContext.SaveChanges();
				}
				else
				{
					throw new Exception();
				}
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("Failed to find markdown");
			}
			catch (Exception)
			{
				throw new Exception("Failed to update markdown");
			}
		}
	}
}
