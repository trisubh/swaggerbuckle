// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.HttpConfigurationExtensions
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Swashbuckle.Application
{
  public static class HttpConfigurationExtensions
  {
    private static readonly string DefaultRouteTemplate = "swagger/docs/{apiVersion}";

    public static SwaggerEnabledConfiguration EnableSwagger(
      this HttpConfiguration httpConfig,
      Action<SwaggerDocsConfig> configure = null)
    {
      return httpConfig.EnableSwagger(HttpConfigurationExtensions.DefaultRouteTemplate, configure);
    }

    public static SwaggerEnabledConfiguration EnableSwagger(
      this HttpConfiguration httpConfig,
      string routeTemplate,
      Action<SwaggerDocsConfig> configure = null)
    {
      SwaggerDocsConfig config = new SwaggerDocsConfig();
      if (configure != null)
        configure(config);
      HttpRouteCollectionExtensions.MapHttpRoute(httpConfig.get_Routes(), "swagger_docs" + routeTemplate, routeTemplate, (object) null, (object) new
      {
        apiVersion = ".+"
      }, (HttpMessageHandler) new SwaggerDocsHandler(config));
      return new SwaggerEnabledConfiguration(httpConfig, new Func<HttpRequestMessage, string>(config.GetRootUrl), config.GetApiVersions().Select<string, string>((Func<string, string>) (version => routeTemplate.Replace("{apiVersion}", version))));
    }

    internal static JsonSerializerSettings SerializerSettingsOrDefault(
      this HttpConfiguration httpConfig)
    {
      JsonMediaTypeFormatter jsonFormatter = httpConfig.get_Formatters().get_JsonFormatter();
      if (jsonFormatter != null)
        return (JsonSerializerSettings) jsonFormatter.get_SerializerSettings();
      return new JsonSerializerSettings();
    }
  }
}
