// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.HttpRouteDirectionConstraint
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Swashbuckle.Application
{
  public class HttpRouteDirectionConstraint : IHttpRouteConstraint
  {
    private readonly HttpRouteDirection _allowedDirection;

    public HttpRouteDirectionConstraint(HttpRouteDirection allowedDirection)
    {
      this._allowedDirection = allowedDirection;
    }

    public bool Match(
      HttpRequestMessage request,
      IHttpRoute route,
      string parameterName,
      IDictionary<string, object> values,
      HttpRouteDirection routeDirection)
    {
      return routeDirection == this._allowedDirection;
    }
  }
}
