// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.StringExtensions
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

namespace Swashbuckle.Swagger
{
  internal static class StringExtensions
  {
    internal static string ToCamelCase(this string value)
    {
      if (string.IsNullOrEmpty(value))
        return value;
      return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
    }
  }
}
