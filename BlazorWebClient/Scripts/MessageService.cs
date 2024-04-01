using System.Net;
using System.Net.Http.Json;
using BlazorWebClient.Scripts;

namespace BlazorWebClient.Scripts
{
    public class MessageService
    {
        public event Action OnMessageRefresh;
        private Response _message;
        public Response Response
        {
            get => _message;
            set => _message = value;
        }

        public void ShowMessage(Response message)
        {
            _message = message;
            OnMessageRefresh?.Invoke();
        }
        public async Task ShowMessage(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                ShowMessage(new Response()
                {
                    StatusCode = response.StatusCode,
                    Message = "Success!"
                });
            }
            else
            {
                Response error = new Response();
                try
                {
                    error = await response.Content.ReadFromJsonAsync<Response>();
                }
                catch
                {
                    ShowMessage(new Response()
                    {
                        StatusCode = response.StatusCode,
                        Message = await response.Content.ReadAsStringAsync()
                    });

                    return;
                }
                ShowMessage(new Response()
                {
                    StatusCode = error.StatusCode,
                    Message = error.Message
                });
                //finally
                //{
                //    Console.WriteLine("asdfasfasf");
                //    ShowMessage(new Response()
                //    {
                //        Code = error.Code,
                //        Message = error.Message
                //    });
                //}
            }
        }
        public async Task ShowMessage(string message)
        {
            ShowMessage(new Response()
            {
                Message = message
            });
        }
    }
}
