// Decompiled with JetBrains decompiler
// Type: Swashbuckle.SwaggerUi.EmbeddedAssetProvider
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Swashbuckle.SwaggerUi
{
  public class EmbeddedAssetProvider : IAssetProvider
  {
    private readonly IDictionary<string, EmbeddedAssetDescriptor> _pathToAssetMap;
    private readonly IDictionary<string, string> _templateParams;

    public EmbeddedAssetProvider(
      IDictionary<string, EmbeddedAssetDescriptor> pathToAssetMap,
      IDictionary<string, string> templateParams)
    {
      this._pathToAssetMap = pathToAssetMap;
      this._templateParams = templateParams;
    }

    public Asset GetAsset(string rootUrl, string path)
    {
      if (!this._pathToAssetMap.ContainsKey(path))
        throw new AssetNotFound(string.Format("Mapping not found - {0}", (object) path));
      EmbeddedAssetDescriptor pathToAsset = this._pathToAssetMap[path];
      return new Asset(this.GetEmbeddedResourceStreamFor(pathToAsset, rootUrl), EmbeddedAssetProvider.InferMediaTypeFrom(pathToAsset.Name));
    }

    private Stream GetEmbeddedResourceStreamFor(
      EmbeddedAssetDescriptor resourceDescriptor,
      string rootUrl)
    {
      Stream manifestResourceStream = resourceDescriptor.Assembly.GetManifestResourceStream(resourceDescriptor.Name);
      if (manifestResourceStream == null)
        throw new AssetNotFound(string.Format("Embedded resource not found - {0}", (object) resourceDescriptor.Name));
      if (!resourceDescriptor.IsTemplate)
        return manifestResourceStream;
      Dictionary<string, string> dictionary = this._templateParams.Union<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>) new KeyValuePair<string, string>[1]
      {
        new KeyValuePair<string, string>("%(RootUrl)", rootUrl)
      }).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (entry => entry.Key), (Func<KeyValuePair<string, string>, string>) (entry => entry.Value));
      return manifestResourceStream.FindAndReplace((IDictionary<string, string>) dictionary);
    }

    private static string InferMediaTypeFrom(string path)
    {
      switch (((IEnumerable<string>) path.Split('.')).Last<string>())
      {
        case "css":
          return "text/css";
        case "js":
          return "text/javascript";
        case "gif":
          return "image/gif";
        case "png":
          return "image/png";
        case "eot":
          return "application/vnd.ms-fontobject";
        case "woff":
          return "application/font-woff";
        case "woff2":
          return "application/font-woff2";
        case "otf":
          return "application/font-sfnt";
        case "ttf":
          return "application/font-sfnt";
        case "svg":
          return "image/svg+xml";
        default:
          return "text/html";
      }
    }
  }
}
