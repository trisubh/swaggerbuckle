// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.InfoBuilder
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Swashbuckle.Swagger;
using System;

namespace Swashbuckle.Application
{
  public class InfoBuilder
  {
    private readonly ContactBuilder _contactBuilder = new ContactBuilder();
    private readonly LicenseBuilder _licenseBuilder = new LicenseBuilder();
    private string _version;
    private string _title;
    private string _description;
    private string _termsOfService;

    public InfoBuilder(string version, string title)
    {
      this._version = version;
      this._title = title;
    }

    public InfoBuilder Description(string description)
    {
      this._description = description;
      return this;
    }

    public InfoBuilder TermsOfService(string termsOfService)
    {
      this._termsOfService = termsOfService;
      return this;
    }

    public InfoBuilder Contact(Action<ContactBuilder> contact)
    {
      contact(this._contactBuilder);
      return this;
    }

    public InfoBuilder License(Action<LicenseBuilder> license)
    {
      license(this._licenseBuilder);
      return this;
    }

    internal Info Build()
    {
      return new Info()
      {
        version = this._version,
        title = this._title,
        description = this._description,
        termsOfService = this._termsOfService,
        contact = this._contactBuilder.Build(),
        license = this._licenseBuilder.Build()
      };
    }
  }
}
