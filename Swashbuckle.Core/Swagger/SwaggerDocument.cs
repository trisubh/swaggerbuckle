// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.SwaggerDocument
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System.Collections.Generic;

namespace Swashbuckle.Swagger
{
  public class SwaggerDocument
  {
    public readonly string swagger = "2.0";
    public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    public Info info;
    public string host;
    public string basePath;
    public IList<string> schemes;
    public IList<string> consumes;
    public IList<string> produces;
    public IDictionary<string, PathItem> paths;
    public IDictionary<string, Schema> definitions;
    public IDictionary<string, Parameter> parameters;
    public IDictionary<string, Response> responses;
    public IDictionary<string, SecurityScheme> securityDefinitions;
    public IList<IDictionary<string, IEnumerable<string>>> security;
    public IList<Tag> tags;
    public ExternalDocs externalDocs;
  }
}
