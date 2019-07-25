// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.ApiKeySchemeBuilder
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Swashbuckle.Swagger;

namespace Swashbuckle.Application
{
  public class ApiKeySchemeBuilder : SecuritySchemeBuilder
  {
    private string _description;
    private string _name;
    private string _in;

    public ApiKeySchemeBuilder Description(string description)
    {
      this._description = description;
      return this;
    }

    public ApiKeySchemeBuilder Name(string name)
    {
      this._name = name;
      return this;
    }

    public ApiKeySchemeBuilder In(string @in)
    {
      this._in = @in;
      return this;
    }

    internal override SecurityScheme Build()
    {
      return new SecurityScheme()
      {
        type = "apiKey",
        description = this._description,
        name = this._name,
        @in = this._in
      };
    }
  }
}
