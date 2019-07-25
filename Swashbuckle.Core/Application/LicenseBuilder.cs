// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.LicenseBuilder
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Swashbuckle.Swagger;

namespace Swashbuckle.Application
{
  public class LicenseBuilder
  {
    private string _name;
    private string _url;

    public LicenseBuilder Name(string name)
    {
      this._name = name;
      return this;
    }

    public LicenseBuilder Url(string url)
    {
      this._url = url;
      return this;
    }

    internal License Build()
    {
      switch (this._name ?? this._url)
      {
        case null:
          return (License) null;
        default:
          return new License()
          {
            name = this._name,
            url = this._url
          };
      }
    }
  }
}
