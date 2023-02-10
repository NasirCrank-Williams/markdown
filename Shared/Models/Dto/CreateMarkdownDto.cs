using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace markdown.Shared.Models.Dto
{
	public class CreateMarkdownDto
	{
		public string Content { get; set; } = string.Empty;
		public DateTime DateTime { get; set; }
		public Guid UserId { get; set; }
	}
}
