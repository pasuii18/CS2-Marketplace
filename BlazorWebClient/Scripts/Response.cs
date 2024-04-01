using System.Net;

namespace BlazorWebClient.Scripts
{
    public class Response
    {
        public HttpStatusCode? StatusCode {  get; set; }
        public string? Message { get; set; }
    }
}
