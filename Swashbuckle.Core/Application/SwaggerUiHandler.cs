// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.SwaggerUiHandler
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Swashbuckle.SwaggerUi;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Swashbuckle.Application
{
  public class SwaggerUiHandler : HttpMessageHandler
  {
    private readonly SwaggerUiConfig _config;

    public SwaggerUiHandler(SwaggerUiConfig config)
    {
      this._config = config;
    }

    protected override Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      IAssetProvider swaggerUiProvider = this._config.GetSwaggerUiProvider();
      string rootUrl = this._config.GetRootUrl(request);
      string assetPath = HttpRequestMessageExtensions.GetRouteData(request).get_Values()["assetPath"].ToString();
      try
      {
        HttpContent httpContent = this.ContentFor(swaggerUiProvider.GetAsset(rootUrl, assetPath));
        return this.TaskFor(new HttpResponseMessage()
        {
          Content = httpContent,
          RequestMessage = request
        });
      }
      catch (AssetNotFound ex)
      {
        return this.TaskFor(HttpRequestMessageExtensions.CreateErrorResponse(request, HttpStatusCode.NotFound, (Exception) ex));
      }
    }

    private HttpContent ContentFor(Asset webAsset)
    {
      int bufferSize = webAsset.Stream.Length > (long) int.MaxValue ? int.MaxValue : (int) webAsset.Stream.Length;
      StreamContent streamContent = new StreamContent(webAsset.Stream, bufferSize);
      streamContent.Headers.ContentType = new MediaTypeHeaderValue(webAsset.MediaType);
      return (HttpContent) streamContent;
    }

    private Task<HttpResponseMessage> TaskFor(HttpResponseMessage response)
    {
      TaskCompletionSource<HttpResponseMessage> completionSource = new TaskCompletionSource<HttpResponseMessage>();
      completionSource.SetResult(response);
      return completionSource.Task;
    }
  }
}
