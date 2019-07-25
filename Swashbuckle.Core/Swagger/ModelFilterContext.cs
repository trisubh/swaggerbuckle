// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.ModelFilterContext
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json.Serialization;
using System;

namespace Swashbuckle.Swagger
{
  public class ModelFilterContext
  {
    public ModelFilterContext(
      Type systemType,
      JsonObjectContract jsonObjectContract,
      SchemaRegistry schemaRegistry)
    {
      this.SystemType = systemType;
      this.JsonObjectContract = jsonObjectContract;
      this.SchemaRegistry = schemaRegistry;
    }

    public Type SystemType { get; private set; }

    public JsonObjectContract JsonObjectContract { get; private set; }

    public SchemaRegistry SchemaRegistry { get; private set; }
  }
}
