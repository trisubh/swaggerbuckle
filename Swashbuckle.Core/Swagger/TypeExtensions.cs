// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.TypeExtensions
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Swashbuckle.Swagger
{
  public static class TypeExtensions
  {
    public static string FriendlyId(this Type type, bool fullyQualified = false)
    {
      string str = fullyQualified ? type.FullNameSansTypeParameters().Replace("+", ".") : type.Name;
      if (!type.IsGenericType)
        return str;
      string[] array = ((IEnumerable<Type>) type.GetGenericArguments()).Select<Type, string>((Func<Type, string>) (t => t.FriendlyId(fullyQualified))).ToArray<string>();
      return new StringBuilder(str).Replace(string.Format("`{0}", (object) ((IEnumerable<string>) array).Count<string>()), string.Empty).Append(string.Format("[{0}]", (object) string.Join(",", array).TrimEnd(','))).ToString();
    }

    public static string FullNameSansTypeParameters(this Type type)
    {
      string str = type.FullName;
      if (string.IsNullOrEmpty(str))
        str = type.Name;
      int length = str.IndexOf("[[");
      if (length != -1)
        return str.Substring(0, length);
      return str;
    }

    public static string[] GetEnumNamesForSerialization(this Type enumType)
    {
      return ((IEnumerable<FieldInfo>) enumType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)).Select<FieldInfo, string>((Func<FieldInfo, string>) (fieldInfo =>
      {
        EnumMemberAttribute enumMemberAttribute = fieldInfo.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault<EnumMemberAttribute>();
        if (enumMemberAttribute != null && !string.IsNullOrWhiteSpace(enumMemberAttribute.Value))
          return enumMemberAttribute.Value;
        return fieldInfo.Name;
      })).ToArray<string>();
    }
  }
}
