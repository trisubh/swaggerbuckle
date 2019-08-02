// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.SecurityScheme
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System.Collections.Generic;

namespace Swashbuckle.Swagger
{
  public class SecurityScheme
  {
    public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    public string type;
    public string description;
    public string name;
    public string @in;
    public string flow;
    public string authorizationUrl;
    public string tokenUrl;
    public IDictionary<string, string> scopes;
  }
}
