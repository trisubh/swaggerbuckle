// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.OAuth2SchemeBuilder
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;

namespace Swashbuckle.Application
{
  public class OAuth2SchemeBuilder : SecuritySchemeBuilder
  {
    private IDictionary<string, string> _scopes = (IDictionary<string, string>) new Dictionary<string, string>();
    private string _description;
    private string _flow;
    private string _authorizationUrl;
    private string _tokenUrl;

    public OAuth2SchemeBuilder Description(string description)
    {
      this._description = description;
      return this;
    }

    public OAuth2SchemeBuilder Flow(string flow)
    {
      this._flow = flow;
      return this;
    }

    public OAuth2SchemeBuilder AuthorizationUrl(string authorizationUrl)
    {
      this._authorizationUrl = authorizationUrl;
      return this;
    }

    public OAuth2SchemeBuilder TokenUrl(string tokenUrl)
    {
      this._tokenUrl = tokenUrl;
      return this;
    }

    public OAuth2SchemeBuilder Scopes(
      Action<IDictionary<string, string>> configure)
    {
      configure(this._scopes);
      return this;
    }

    internal override SecurityScheme Build()
    {
      return new SecurityScheme()
      {
        type = "oauth2",
        flow = this._flow,
        authorizationUrl = this._authorizationUrl,
        tokenUrl = this._tokenUrl,
        scopes = this._scopes,
        description = this._description
      };
    }
  }
}
