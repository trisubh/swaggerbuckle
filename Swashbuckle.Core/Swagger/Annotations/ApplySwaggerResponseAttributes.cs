// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.Annotations.ApplySwaggerResponseAttributes
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Description;

namespace Swashbuckle.Swagger.Annotations
{
  public class ApplySwaggerResponseAttributes : IOperationFilter
  {
    public void Apply(
      Operation operation,
      SchemaRegistry schemaRegistry,
      ApiDescription apiDescription)
    {
      if (apiDescription.GetControllerAndActionAttributes<SwaggerResponseRemoveDefaultsAttribute>().Any<SwaggerResponseRemoveDefaultsAttribute>())
        operation.responses.Clear();
      foreach (SwaggerResponseAttribute responseAttribute in (IEnumerable<SwaggerResponseAttribute>) apiDescription.GetControllerAndActionAttributes<SwaggerResponseAttribute>().OrderBy<SwaggerResponseAttribute, int>((Func<SwaggerResponseAttribute, int>) (attr => attr.StatusCode)))
      {
        string statusCode = responseAttribute.StatusCode.ToString();
        operation.responses[statusCode] = new Response()
        {
          description = responseAttribute.Description ?? this.InferDescriptionFrom(statusCode),
          schema = responseAttribute.Type != (Type) null ? schemaRegistry.GetOrRegister(responseAttribute.Type) : (Schema) null
        };
      }
    }

    private string InferDescriptionFrom(string statusCode)
    {
      HttpStatusCode result;
      if (System.Enum.TryParse<HttpStatusCode>(statusCode, true, out result))
        return result.ToString();
      return (string) null;
    }
  }
}
