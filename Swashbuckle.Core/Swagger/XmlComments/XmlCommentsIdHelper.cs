// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.XmlComments.XmlCommentsIdHelper
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Reflection;
using System.Text;

namespace Swashbuckle.Swagger.XmlComments
{
  public class XmlCommentsIdHelper
  {
    public static string GetCommentIdForMethod(MethodInfo methodInfo)
    {
      StringBuilder builder = new StringBuilder("M:");
      XmlCommentsIdHelper.AppendFullTypeName(methodInfo.DeclaringType, builder, false);
      builder.Append(".");
      XmlCommentsIdHelper.AppendMethodName(methodInfo, builder);
      return builder.ToString();
    }

    public static string GetCommentIdForType(Type type)
    {
      StringBuilder builder = new StringBuilder("T:");
      XmlCommentsIdHelper.AppendFullTypeName(type, builder, false);
      return builder.ToString();
    }

    public static string GetCommentIdForProperty(PropertyInfo propertyInfo)
    {
      StringBuilder builder = new StringBuilder("P:");
      XmlCommentsIdHelper.AppendFullTypeName(propertyInfo.DeclaringType, builder, false);
      builder.Append(".");
      XmlCommentsIdHelper.AppendPropertyName(propertyInfo, builder);
      return builder.ToString();
    }

    private static void AppendFullTypeName(
      Type type,
      StringBuilder builder,
      bool expandGenericArgs = false)
    {
      if (type.Namespace != null)
      {
        builder.Append(type.Namespace);
        builder.Append(".");
      }
      XmlCommentsIdHelper.AppendTypeName(type, builder, expandGenericArgs);
    }

    private static void AppendTypeName(Type type, StringBuilder builder, bool expandGenericArgs)
    {
      if (type.IsNested)
      {
        XmlCommentsIdHelper.AppendTypeName(type.DeclaringType, builder, false);
        builder.Append(".");
      }
      builder.Append(type.Name);
      if (!expandGenericArgs)
        return;
      XmlCommentsIdHelper.ExpandGenericArgsIfAny(type, builder);
    }

    public static void ExpandGenericArgsIfAny(Type type, StringBuilder builder)
    {
      if (type.IsGenericType)
      {
        StringBuilder builder1 = new StringBuilder("{");
        Type[] genericArguments = type.GetGenericArguments();
        foreach (Type type1 in genericArguments)
        {
          XmlCommentsIdHelper.AppendFullTypeName(type1, builder1, true);
          builder1.Append(",");
        }
        builder1.Replace(",", "}", builder1.Length - 1, 1);
        builder.Replace(string.Format("`{0}", (object) genericArguments.Length), builder1.ToString());
      }
      else
      {
        if (!type.IsArray)
          return;
        XmlCommentsIdHelper.ExpandGenericArgsIfAny(type.GetElementType(), builder);
      }
    }

    private static void AppendMethodName(MethodInfo methodInfo, StringBuilder builder)
    {
      builder.Append(methodInfo.Name);
      ParameterInfo[] parameters = methodInfo.GetParameters();
      if (parameters.Length == 0)
        return;
      builder.Append("(");
      foreach (ParameterInfo parameterInfo in parameters)
      {
        XmlCommentsIdHelper.AppendFullTypeName(parameterInfo.ParameterType, builder, true);
        builder.Append(",");
      }
      builder.Replace(",", ")", builder.Length - 1, 1);
    }

    private static void AppendPropertyName(PropertyInfo propertyInfo, StringBuilder builder)
    {
      builder.Append(propertyInfo.Name);
    }
  }
}
