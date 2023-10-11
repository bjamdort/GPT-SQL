using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT_Test.Shared
{
	public class GptTableData
	{
		public List<string> ColumnNames { get; set; }
		public List<List<string>> RowData { get; set; }
	}
}
