// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.Schema
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Swashbuckle.Swagger
{
  public class Schema
  {
    public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    [JsonProperty("$ref")]
    public string @ref;
    public string format;
    public string title;
    public string description;
    public object @default;
    public int? multipleOf;
    public int? maximum;
    public bool? exclusiveMaximum;
    public int? minimum;
    public bool? exclusiveMinimum;
    public int? maxLength;
    public int? minLength;
    public string pattern;
    public int? maxItems;
    public int? minItems;
    public bool? uniqueItems;
    public int? maxProperties;
    public int? minProperties;
    public IList<string> required;
    public IList<object> @enum;
    public string type;
    public Schema items;
    public IList<Schema> allOf;
    public IDictionary<string, Schema> properties;
    public Schema additionalProperties;
    public string discriminator;
    public bool? readOnly;
    public Xml xml;
    public ExternalDocs externalDocs;
    public object example;
  }
}
