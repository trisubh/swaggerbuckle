// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.Parameter
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json;

namespace Swashbuckle.Swagger
{
  public class Parameter : PartialSchema
  {
    [JsonProperty("$ref")]
    public string @ref;
    public string name;
    public string @in;
    public string description;
    public bool? required;
    public Schema schema;
  }
}
