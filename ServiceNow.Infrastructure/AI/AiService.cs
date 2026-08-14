using ServiceNow.ServiceNow.Application.Interfaces;
using System.Text.Json;
using System.Text;
namespace ServiceNow.ServiceNow.Infrastructure.AI
{
    public class AiService : IAiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AiService(
            IConfiguration configuration,
            HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<string> GenerateTicketSummary(
            string title,
            string description,
            string status,
            string priority,
            List<string> comments)
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            var model = _configuration["Gemini:Model"]
                        ?? "gemini-2.5-flash-lite";

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    "Gemini API key is not configured.");
            }

            var discussion = comments.Count == 0
                ? "No comments yet."
                : string.Join(
                    "\n",
                    comments.Select(
                        (comment, index) =>
                            $"{index + 1}. {comment}"
                    )
                );

            var prompt = $"""
                You are an IT support assistant.

                Analyze this support ticket and its current discussion.

                TICKET TITLE:
                {title}

                DESCRIPTION:
                {description}

                STATUS:
                {status}

                PRIORITY:
                {priority}

                CURRENT DISCUSSION:
                {discussion}

                Generate a concise professional summary for a support agent.

                Include:

                1. Issue Summaries
                2. Current Discussion
                3. Current Situation
                4. Recommended Next Step

                Use ONLY the information provided.
                Do not invent facts.
                Keep the response concise.
                """;

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = prompt
                            }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}"
            );

            request.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(request);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Gemini API failed: {responseBody}");
            }

            using var document =
                JsonDocument.Parse(responseBody);

            var text =
                document.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

            return text ?? "Unable to generate summary.";
        }
    }
}
