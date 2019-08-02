// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.SwaggerEnabledConfiguration
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Swashbuckle.Application
{
  public class SwaggerEnabledConfiguration
  {
    private static readonly string DefaultRouteTemplate = "swagger/ui/{*assetPath}";
    private readonly HttpConfiguration _httpConfig;
    private readonly Func<HttpRequestMessage, string> _rootUrlResolver;
    private readonly IEnumerable<string> _discoveryPaths;

    public SwaggerEnabledConfiguration(
      HttpConfiguration httpConfig,
      Func<HttpRequestMessage, string> rootUrlResolver,
      IEnumerable<string> discoveryPaths)
    {
      this._httpConfig = httpConfig;
      this._rootUrlResolver = rootUrlResolver;
      this._discoveryPaths = discoveryPaths;
    }

    public void EnableSwaggerUi(Action<SwaggerUiConfig> configure = null)
    {
      this.EnableSwaggerUi(SwaggerEnabledConfiguration.DefaultRouteTemplate, configure);
    }

    public void EnableSwaggerUi(string routeTemplate, Action<SwaggerUiConfig> configure = null)
    {
      SwaggerUiConfig config = new SwaggerUiConfig(this._discoveryPaths, this._rootUrlResolver);
      if (configure != null)
        configure(config);
      HttpRouteCollectionExtensions.MapHttpRoute(this._httpConfig.get_Routes(), "swagger_ui" + routeTemplate, routeTemplate, (object) null, (object) new
      {
        assetPath = ".+"
      }, (HttpMessageHandler) new SwaggerUiHandler(config));
      if (!(routeTemplate == SwaggerEnabledConfiguration.DefaultRouteTemplate))
        return;
      HttpRouteCollectionExtensions.MapHttpRoute(this._httpConfig.get_Routes(), "swagger_ui_shortcut", "swagger", (object) null, (object) new
      {
        uriResolution = new HttpRouteDirectionConstraint((HttpRouteDirection) 0)
      }, (HttpMessageHandler) new RedirectHandler(this._rootUrlResolver, "swagger/ui/index"));
    }
  }
}
