// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.Annotations.ApplySwaggerOperationAttributes
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace Swashbuckle.Swagger.Annotations
{
  public class ApplySwaggerOperationAttributes : IOperationFilter
  {
    public void Apply(
      Operation operation,
      SchemaRegistry schemaRegistry,
      ApiDescription apiDescription)
    {
      SwaggerOperationAttribute operationAttribute = ((IEnumerable<SwaggerOperationAttribute>) apiDescription.get_ActionDescriptor().GetCustomAttributes<SwaggerOperationAttribute>()).FirstOrDefault<SwaggerOperationAttribute>();
      if (operationAttribute == null)
        return;
      if (operationAttribute.OperationId != null)
        operation.operationId = operationAttribute.OperationId;
      if (operationAttribute.Tags != null)
        operation.tags = (IList<string>) operationAttribute.Tags;
      if (operationAttribute.Schemes == null)
        return;
      operation.schemes = (IList<string>) operationAttribute.Schemes;
    }
  }
}
