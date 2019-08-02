// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.Annotations.ApplySwaggerSchemaFilterAttributes
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Linq;

namespace Swashbuckle.Swagger.Annotations
{
  public class ApplySwaggerSchemaFilterAttributes : ISchemaFilter
  {
    public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
    {
      foreach (SwaggerSchemaFilterAttribute schemaFilterAttribute in type.GetCustomAttributes(false).OfType<SwaggerSchemaFilterAttribute>())
        ((ISchemaFilter) Activator.CreateInstance(schemaFilterAttribute.FilterType)).Apply(schema, schemaRegistry, type);
    }
  }
}
