using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT_Test.Shared
{
	public class GptQuery
	{
		public string TextResponse { get; set; }
		public string SqlQuery { get; set; }
		public GptTableData TableResponse { get; set; }
	}
}