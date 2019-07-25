// Decompiled with JetBrains decompiler
// Type: Swashbuckle.SwaggerUi.StreamExtensions
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Swashbuckle.SwaggerUi
{
  public static class StreamExtensions
  {
    public static Stream FindAndReplace(
      this Stream stream,
      IDictionary<string, string> replacements)
    {
      StringBuilder stringBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());
      foreach (KeyValuePair<string, string> replacement in (IEnumerable<KeyValuePair<string, string>>) replacements)
        stringBuilder.Replace(replacement.Key, replacement.Value);
      return (Stream) new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
    }
  }
}
