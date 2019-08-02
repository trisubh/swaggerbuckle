// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.PathItem
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Swashbuckle.Swagger
{
  public class PathItem
  {
    public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    [JsonProperty("$ref")]
    public string @ref;
    public Operation get;
    public Operation put;
    public Operation post;
    public Operation delete;
    public Operation options;
    public Operation head;
    public Operation patch;
    public IList<Parameter> parameters;
  }
}
