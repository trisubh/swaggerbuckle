// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.PartialSchema
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System.Collections.Generic;

namespace Swashbuckle.Swagger
{
  public class PartialSchema
  {
    public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    public string type;
    public string format;
    public PartialSchema items;
    public string collectionFormat;
    public object @default;
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
    public IList<object> @enum;
    public int? multipleOf;
  }
}
