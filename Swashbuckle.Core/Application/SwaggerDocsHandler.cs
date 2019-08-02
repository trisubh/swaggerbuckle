// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.SwaggerDocsHandler
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Swashbuckle.Application
{
  public class SwaggerDocsHandler : HttpMessageHandler
  {
    private readonly SwaggerDocsConfig _config;

    public SwaggerDocsHandler(SwaggerDocsConfig config)
    {
      this._config = config;
    }

    protected override Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      ISwaggerProvider swaggerProvider = this._config.GetSwaggerProvider(request);
      string rootUrl = this._config.GetRootUrl(request);
      string apiVersion = HttpRequestMessageExtensions.GetRouteData(request).get_Values()["apiVersion"].ToString();
      try
      {
        SwaggerDocument swagger = swaggerProvider.GetSwagger(rootUrl, apiVersion);
        HttpContent httpContent = this.ContentFor(request, swagger);
        return this.TaskFor(new HttpResponseMessage()
        {
          Content = httpContent
        });
      }
      catch (UnknownApiVersion ex)
      {
        return this.TaskFor(HttpRequestMessageExtensions.CreateErrorResponse(request, HttpStatusCode.NotFound, (Exception) ex));
      }
    }

    private HttpContent ContentFor(
      HttpRequestMessage request,
      SwaggerDocument swaggerDoc)
    {
      ContentNegotiationResult negotiationResult = ServicesExtensions.GetContentNegotiator(HttpRequestMessageExtensions.GetConfiguration(request).get_Services()).Negotiate(typeof (SwaggerDocument), request, this.GetSupportedSwaggerFormatters());
      return (HttpContent) new ObjectContent(typeof (SwaggerDocument), (object) swaggerDoc, negotiationResult.get_Formatter(), negotiationResult.get_MediaType());
    }

    private IEnumerable<MediaTypeFormatter> GetSupportedSwaggerFormatters()
    {
      JsonMediaTypeFormatter mediaTypeFormatter1 = new JsonMediaTypeFormatter();
      JsonMediaTypeFormatter mediaTypeFormatter2 = mediaTypeFormatter1;
      JsonSerializerSettings serializerSettings1 = new JsonSerializerSettings();
      serializerSettings1.set_NullValueHandling((NullValueHandling) 1);
      serializerSettings1.set_Formatting(this._config.GetFormatting());
      serializerSettings1.set_Converters((IList<JsonConverter>) new VendorExtensionsConverter[1]
      {
        new VendorExtensionsConverter()
      });
      JsonSerializerSettings serializerSettings2 = serializerSettings1;
      mediaTypeFormatter2.set_SerializerSettings((JsonSerializerSettings) serializerSettings2);
      return (IEnumerable<MediaTypeFormatter>) new JsonMediaTypeFormatter[1]
      {
        mediaTypeFormatter1
      };
    }

    private Task<HttpResponseMessage> TaskFor(HttpResponseMessage response)
    {
      TaskCompletionSource<HttpResponseMessage> completionSource = new TaskCompletionSource<HttpResponseMessage>();
      completionSource.SetResult(response);
      return completionSource.Task;
    }
  }
}
