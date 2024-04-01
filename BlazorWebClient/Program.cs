using BlazorWebClient.Components;
using BlazorWebClient.Scripts;
using Microsoft.JSInterop;

namespace BlazorWebClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<MessageService>();

            var app = builder.Build();
            app.UseExceptionHandler("/Error");

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
