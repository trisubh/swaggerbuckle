// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.SwaggerUiConfig
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json;
using Swashbuckle.SwaggerUi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Swashbuckle.Application
{
  public class SwaggerUiConfig
  {
    private readonly Dictionary<string, EmbeddedAssetDescriptor> _pathToAssetMap;
    private readonly Dictionary<string, string> _templateParams;
    private readonly Func<HttpRequestMessage, string> _rootUrlResolver;

    public SwaggerUiConfig(
      IEnumerable<string> discoveryPaths,
      Func<HttpRequestMessage, string> rootUrlResolver)
    {
      this._pathToAssetMap = new Dictionary<string, EmbeddedAssetDescriptor>();
      this._templateParams = new Dictionary<string, string>()
      {
        {
          "%(DocumentTitle)",
          "Swagger UI"
        },
        {
          "%(StylesheetIncludes)",
          ""
        },
        {
          "%(DiscoveryPaths)",
          string.Join("|", discoveryPaths)
        },
        {
          "%(BooleanValues)",
          "true|false"
        },
        {
          "%(ValidatorUrl)",
          ""
        },
        {
          "%(CustomScripts)",
          ""
        },
        {
          "%(DocExpansion)",
          "none"
        },
        {
          "%(SupportedSubmitMethods)",
          "get|put|post|delete|options|head|patch"
        },
        {
          "%(OAuth2Enabled)",
          "false"
        },
        {
          "%(OAuth2ClientId)",
          ""
        },
        {
          "%(OAuth2ClientSecret)",
          ""
        },
        {
          "%(OAuth2Realm)",
          ""
        },
        {
          "%(OAuth2AppName)",
          ""
        },
        {
          "%(OAuth2ScopeSeperator)",
          " "
        },
        {
          "%(OAuth2AdditionalQueryStringParams)",
          "{}"
        },
        {
          "%(ApiKeyName)",
          "api_key"
        },
        {
          "%(ApiKeyIn)",
          "query"
        }
      };
      this._rootUrlResolver = rootUrlResolver;
      this.MapPathsForSwaggerUiAssets();
      Assembly assembly = this.GetType().Assembly;
      this.CustomAsset("index", assembly, "Swashbuckle.SwaggerUi.CustomAssets.index.html", true);
      this.CustomAsset("css/screen-css", assembly, "Swashbuckle.SwaggerUi.CustomAssets.screen.css", false);
      this.CustomAsset("css/typography-css", assembly, "Swashbuckle.SwaggerUi.CustomAssets.typography.css", false);
    }

    public void InjectStylesheet(
      Assembly resourceAssembly,
      string resourceName,
      string media = "screen",
      bool isTemplate = false)
    {
      string path = "ext/" + resourceName.Replace(".", "-");
      StringBuilder stringBuilder = new StringBuilder(this._templateParams["%(StylesheetIncludes)"]);
      stringBuilder.AppendLine("<link href='" + path + "' media='" + media + "' rel='stylesheet' type='text/css' />");
      this._templateParams["%(StylesheetIncludes)"] = stringBuilder.ToString();
      this.CustomAsset(path, resourceAssembly, resourceName, isTemplate);
    }

    public void BooleanValues(IEnumerable<string> values)
    {
      this._templateParams["%(BooleanValues)"] = string.Join("|", values);
    }

    public void DocumentTitle(string title)
    {
      this._templateParams["%(DocumentTitle)"] = title;
    }

    public void SetValidatorUrl(string url)
    {
      this._templateParams["%(ValidatorUrl)"] = url;
    }

    public void DisableValidator()
    {
      this._templateParams["%(ValidatorUrl)"] = "null";
    }

    public void InjectJavaScript(Assembly resourceAssembly, string resourceName, bool isTemplate = false)
    {
      string path = "ext/" + resourceName.Replace(".", "-");
      StringBuilder stringBuilder = new StringBuilder(this._templateParams["%(CustomScripts)"]);
      if (stringBuilder.Length > 0)
        stringBuilder.Append("|");
      stringBuilder.Append(path);
      this._templateParams["%(CustomScripts)"] = stringBuilder.ToString();
      this.CustomAsset(path, resourceAssembly, resourceName, isTemplate);
    }

    public void DocExpansion(DocExpansion docExpansion)
    {
      this._templateParams["%(DocExpansion)"] = docExpansion.ToString().ToLower();
    }

    public void SupportedSubmitMethods(params string[] methods)
    {
      this._templateParams["%(SupportedSubmitMethods)"] = string.Join("|", methods).ToLower();
    }

    public void CustomAsset(
      string path,
      Assembly resourceAssembly,
      string resourceName,
      bool isTemplate = false)
    {
      if (path == "index")
        isTemplate = true;
      this._pathToAssetMap[path] = new EmbeddedAssetDescriptor(resourceAssembly, resourceName, isTemplate);
    }

    public void EnableDiscoveryUrlSelector()
    {
      this.InjectJavaScript(this.GetType().Assembly, "Swashbuckle.SwaggerUi.CustomAssets.discoveryUrlSelector.js", false);
    }

    public void EnableOAuth2Support(string clientId, string realm, string appName)
    {
      this.EnableOAuth2Support(clientId, "N/A", realm, appName, " ", (Dictionary<string, string>) null);
    }

    public void EnableOAuth2Support(
      string clientId,
      string clientSecret,
      string realm,
      string appName,
      string scopeSeperator = " ",
      Dictionary<string, string> additionalQueryStringParams = null)
    {
      this._templateParams["%(OAuth2Enabled)"] = "true";
      this._templateParams["%(OAuth2ClientId)"] = clientId;
      this._templateParams["%(OAuth2ClientSecret)"] = clientSecret;
      this._templateParams["%(OAuth2Realm)"] = realm;
      this._templateParams["%(OAuth2AppName)"] = appName;
      this._templateParams["%(OAuth2ScopeSeperator)"] = scopeSeperator;
      if (additionalQueryStringParams == null)
        return;
      this._templateParams["%(OAuth2AdditionalQueryStringParams)"] = JsonConvert.SerializeObject((object) additionalQueryStringParams);
    }

    public void EnableApiKeySupport(string name, string apiKeyIn)
    {
      this._templateParams["%(ApiKeyName)"] = name;
      this._templateParams["%(ApiKeyIn)"] = apiKeyIn;
    }

    internal IAssetProvider GetSwaggerUiProvider()
    {
      return (IAssetProvider) new EmbeddedAssetProvider((IDictionary<string, EmbeddedAssetDescriptor>) this._pathToAssetMap, (IDictionary<string, string>) this._templateParams);
    }

    internal string GetRootUrl(HttpRequestMessage swaggerRequest)
    {
      return this._rootUrlResolver(swaggerRequest);
    }

    private void MapPathsForSwaggerUiAssets()
    {
      Assembly assembly = this.GetType().Assembly;
      foreach (string manifestResourceName in assembly.GetManifestResourceNames())
      {
        if (!manifestResourceName.Contains("Swashbuckle.SwaggerUi.CustomAssets"))
        {
          string index = manifestResourceName.Replace("\\", "/").Replace(".", "-");
          this._pathToAssetMap[index] = new EmbeddedAssetDescriptor(assembly, manifestResourceName, index == "index");
        }
      }
    }
  }
}
