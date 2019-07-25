// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.Annotations.SwaggerOperationAttribute
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;

namespace Swashbuckle.Swagger.Annotations
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class SwaggerOperationAttribute : Attribute
  {
    public SwaggerOperationAttribute(string operationId = null)
    {
      this.OperationId = operationId;
    }

    public string OperationId { get; set; }

    public string[] Tags { get; set; }

    public string[] Schemes { get; set; }
  }
}
