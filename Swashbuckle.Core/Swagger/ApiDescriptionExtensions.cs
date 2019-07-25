// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.ApiDescriptionExtensions
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http.Description;

namespace Swashbuckle.Swagger
{
  public static class ApiDescriptionExtensions
  {
    public static string FriendlyId(this ApiDescription apiDescription)
    {
      return string.Format("{0}_{1}", (object) apiDescription.get_ActionDescriptor().get_ControllerDescriptor().get_ControllerName(), (object) apiDescription.get_ActionDescriptor().get_ActionName());
    }

    public static IEnumerable<string> Consumes(this ApiDescription apiDescription)
    {
      return ((IEnumerable<MediaTypeFormatter>) apiDescription.get_SupportedRequestBodyFormatters()).SelectMany<MediaTypeFormatter, string>((Func<MediaTypeFormatter, IEnumerable<string>>) (formatter => formatter.get_SupportedMediaTypes().Select<MediaTypeHeaderValue, string>((Func<MediaTypeHeaderValue, string>) (mediaType => mediaType.MediaType)))).Distinct<string>();
    }

    public static IEnumerable<string> Produces(this ApiDescription apiDescription)
    {
      return ((IEnumerable<MediaTypeFormatter>) apiDescription.get_SupportedResponseFormatters()).SelectMany<MediaTypeFormatter, string>((Func<MediaTypeFormatter, IEnumerable<string>>) (formatter => formatter.get_SupportedMediaTypes().Select<MediaTypeHeaderValue, string>((Func<MediaTypeHeaderValue, string>) (mediaType => mediaType.MediaType)))).Distinct<string>();
    }

    public static string RelativePathSansQueryString(this ApiDescription apiDescription)
    {
      return ((IEnumerable<string>) apiDescription.get_RelativePath().Split('?')).First<string>();
    }

    public static Type ResponseType(this ApiDescription apiDesc)
    {
      PropertyInfo property1 = typeof (ApiDescription).GetProperty("ResponseDescription");
      if (property1 != (PropertyInfo) null)
      {
        object obj1 = property1.GetValue((object) apiDesc, (object[]) null);
        if (obj1 != null)
        {
          PropertyInfo property2 = obj1.GetType().GetProperty(nameof (ResponseType));
          if (property2 != (PropertyInfo) null)
          {
            object obj2 = property2.GetValue(obj1, (object[]) null);
            if (obj2 != null)
              return (Type) obj2;
          }
        }
      }
      return apiDesc.get_ActionDescriptor().get_ReturnType();
    }

    public static bool IsObsolete(this ApiDescription apiDescription)
    {
      return ((IEnumerable<ObsoleteAttribute>) apiDescription.get_ActionDescriptor().GetCustomAttributes<ObsoleteAttribute>()).Any<ObsoleteAttribute>();
    }

    public static IEnumerable<TAttribute> GetControllerAndActionAttributes<TAttribute>(
      this ApiDescription apiDesc)
      where TAttribute : class
    {
      return ((IEnumerable<TAttribute>) apiDesc.get_ActionDescriptor().get_ControllerDescriptor().GetCustomAttributes<TAttribute>()).Concat<TAttribute>((IEnumerable<TAttribute>) apiDesc.get_ActionDescriptor().GetCustomAttributes<TAttribute>());
    }
  }
}
