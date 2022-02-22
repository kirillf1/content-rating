using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;

namespace Web.Client.Http
{
    public class CookieHttpClientHandler : DelegatingHandler
    {
     

        public CookieHttpClientHandler() : base()
        {
            
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var cookie = await JSRuntime.InvokeAsync<string>("blazorExtensions.GetCookie", new[] { ".AspNetCore.Cookies" });
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            //request.Headers.Add(".AspNetCore.Cookies", $"{cookie}");
            return await base.SendAsync(request, cancellationToken);
        }
        
    }
}
