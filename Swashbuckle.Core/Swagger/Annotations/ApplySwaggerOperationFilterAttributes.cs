// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.Annotations.ApplySwaggerOperationFilterAttributes
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Web.Http.Description;

namespace Swashbuckle.Swagger.Annotations
{
  public class ApplySwaggerOperationFilterAttributes : IOperationFilter
  {
    public void Apply(
      Operation operation,
      SchemaRegistry schemaRegistry,
      ApiDescription apiDescription)
    {
      foreach (SwaggerOperationFilterAttribute andActionAttribute in apiDescription.GetControllerAndActionAttributes<SwaggerOperationFilterAttribute>())
        ((IOperationFilter) Activator.CreateInstance(andActionAttribute.FilterType)).Apply(operation, schemaRegistry, apiDescription);
    }
  }
}
