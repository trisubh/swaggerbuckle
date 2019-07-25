// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.ContactBuilder
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Swashbuckle.Swagger;

namespace Swashbuckle.Application
{
  public class ContactBuilder
  {
    private string _name;
    private string _url;
    private string _email;

    public ContactBuilder Name(string name)
    {
      this._name = name;
      return this;
    }

    public ContactBuilder Url(string url)
    {
      this._url = url;
      return this;
    }

    public ContactBuilder Email(string email)
    {
      this._email = email;
      return this;
    }

    internal Contact Build()
    {
      switch (this._name ?? this._url ?? this._email)
      {
        case null:
          return (Contact) null;
        default:
          return new Contact()
          {
            name = this._name,
            url = this._url,
            email = this._email
          };
      }
    }
  }
}
