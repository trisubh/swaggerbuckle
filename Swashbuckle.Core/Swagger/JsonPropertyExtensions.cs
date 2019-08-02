// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.JsonPropertyExtensions
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Swashbuckle.Swagger
{
  public static class JsonPropertyExtensions
  {
    public static bool IsRequired(this JsonProperty jsonProperty)
    {
      if (!jsonProperty.HasAttribute<RequiredAttribute>())
        return jsonProperty.get_Required() == 2;
      return true;
    }

    public static bool IsObsolete(this JsonProperty jsonProperty)
    {
      return jsonProperty.HasAttribute<ObsoleteAttribute>();
    }

    public static bool HasAttribute<T>(this JsonProperty jsonProperty)
    {
      PropertyInfo propertyInfo = jsonProperty.PropertyInfo();
      if (propertyInfo != (PropertyInfo) null)
        return Attribute.IsDefined((MemberInfo) propertyInfo, typeof (T));
      return false;
    }

    public static PropertyInfo PropertyInfo(this JsonProperty jsonProperty)
    {
      if (jsonProperty.get_UnderlyingName() == null)
        return (PropertyInfo) null;
      object obj = ((IEnumerable<object>) jsonProperty.get_DeclaringType().GetCustomAttributes(typeof (MetadataTypeAttribute), true)).FirstOrDefault<object>();
      return (obj != null ? ((MetadataTypeAttribute) obj).MetadataClassType : jsonProperty.get_DeclaringType()).GetProperty(jsonProperty.get_UnderlyingName(), jsonProperty.get_PropertyType());
    }
  }
}
