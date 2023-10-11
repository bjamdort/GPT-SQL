using GPT_Test.Server.Data;
using GPT_Test.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_API;

namespace GPT_Test.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GptQueryController : ControllerBase
	{
		private readonly ILogger<GptQueryController> _logger;
		private string _sqlTables;
		private readonly Context _dbContext;
		private readonly OpenAIAPI _gptApi;

		public GptQueryController(ILogger<GptQueryController> logger, Context dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
			_gptApi = new OpenAIAPI("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");
			_sqlTables = _dbContext.Database.GenerateCreateScript();
		}

		[HttpGet]
		public async Task<GptQuery> Get([FromQuery] string query)
		{
			var gptQuery = new GptQuery();
			gptQuery.SqlQuery = await GenerateSqlQuery(query);
			var tableDataString = _dbContext.ExecuteRawSqlQuery(gptQuery.SqlQuery);
			var tableData = tableDataString.Split("\n\n").Select(row => row.Split(",").ToList()).ToList();
			gptQuery.TableResponse = new GptTableData
			{
				ColumnNames = tableData.First(),
				RowData = tableData.Skip(1).ToList(),
			};
			gptQuery.TextResponse = await GenerateExplanation(tableDataString);

			return gptQuery;
		}

		private async Task<string> GenerateSqlQuery(string query)
		{
			var chat = _gptApi.Chat.CreateConversation();

			string context = $"""
                            The database is using the following tables:
                            {_sqlTables}
                            """;

			chat.AppendSystemMessage($"You are outputting pure SQL queries to give answers to questions from users\n\n{context}");

			chat.AppendUserInput(query);

			return await chat.GetResponseFromChatbotAsync();
		}

		private async Task<string> GenerateExplanation(string query)
		{
			var chat = _gptApi.Chat.CreateConversation();

			chat.AppendSystemMessage(@$"You are a data scientist that draws conclusions from datasets, you write in plain text");

			chat.AppendUserInput($@"Make observations and draw conclusions from these results in 4 sentences:
										```SQL
										{query}
										```");

			return await chat.GetResponseFromChatbotAsync();
		}
	}
}