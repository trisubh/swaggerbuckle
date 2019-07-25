// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.Annotations.SwaggerResponseAttribute
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Net;

namespace Swashbuckle.Swagger.Annotations
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
  public class SwaggerResponseAttribute : Attribute
  {
    public SwaggerResponseAttribute(HttpStatusCode statusCode)
    {
      this.StatusCode = (int) statusCode;
    }

    public SwaggerResponseAttribute(HttpStatusCode statusCode, string description = null, Type type = null)
      : this(statusCode)
    {
      this.Description = description;
      this.Type = type;
    }

    public SwaggerResponseAttribute(int statusCode)
    {
      this.StatusCode = statusCode;
    }

    public SwaggerResponseAttribute(int statusCode, string description = null, Type type = null)
      : this(statusCode)
    {
      this.Description = description;
      this.Type = type;
    }

    public int StatusCode { get; private set; }

    public string Description { get; set; }

    public Type Type { get; set; }
  }
}
