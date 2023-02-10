using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace markdown.Shared.Models
{
	public class Markdown
	{
		public Guid MarkdownId { get; set; }
		public string Content { get; set; } = string.Empty;
		public DateTime DateTime { get; set; }
		public Guid UserId { get; set; }
	}
}
