// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.RedirectHandler
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Swashbuckle.Application
{
  public class RedirectHandler : HttpMessageHandler
  {
    private readonly Func<HttpRequestMessage, string> _rootUrlResolver;
    private readonly string _redirectPath;

    public RedirectHandler(Func<HttpRequestMessage, string> rootUrlResolver, string redirectPath)
    {
      this._rootUrlResolver = rootUrlResolver;
      this._redirectPath = redirectPath;
    }

    protected override Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      string uriString = this._rootUrlResolver(request) + "/" + this._redirectPath;
      HttpResponseMessage response = HttpRequestMessageExtensions.CreateResponse(request, HttpStatusCode.MovedPermanently);
      response.Headers.Location = new Uri(uriString);
      TaskCompletionSource<HttpResponseMessage> completionSource = new TaskCompletionSource<HttpResponseMessage>();
      completionSource.SetResult(response);
      return completionSource.Task;
    }
  }
}
