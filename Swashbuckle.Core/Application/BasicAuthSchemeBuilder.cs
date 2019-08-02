// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.BasicAuthSchemeBuilder
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Swashbuckle.Swagger;

namespace Swashbuckle.Application
{
  public class BasicAuthSchemeBuilder : SecuritySchemeBuilder
  {
    private string _description;

    public BasicAuthSchemeBuilder Description(string description)
    {
      this._description = description;
      return this;
    }

    internal override SecurityScheme Build()
    {
      return new SecurityScheme()
      {
        type = "basic",
        description = this._description
      };
    }
  }
}
