// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.Operation
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System.Collections.Generic;

namespace Swashbuckle.Swagger
{
  public class Operation
  {
    public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    public IList<string> tags;
    public string summary;
    public string description;
    public ExternalDocs externalDocs;
    public string operationId;
    public IList<string> consumes;
    public IList<string> produces;
    public IList<Parameter> parameters;
    public IDictionary<string, Response> responses;
    public IList<string> schemes;
    public bool? deprecated;
    public IList<IDictionary<string, IEnumerable<string>>> security;
  }
}
