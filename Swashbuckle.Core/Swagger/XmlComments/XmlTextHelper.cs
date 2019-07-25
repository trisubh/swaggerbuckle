// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.XmlComments.XmlTextHelper
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Swashbuckle.Swagger.XmlComments
{
  public static class XmlTextHelper
  {
    public static string NormalizeIndentation(string xmlText)
    {
      if (xmlText == null)
        throw new ArgumentNullException(nameof (xmlText));
      string[] lines = xmlText.Split('\n');
      string leadingWhitespace = XmlTextHelper.GetCommonLeadingWhitespace(lines);
      int num = leadingWhitespace == null ? 0 : leadingWhitespace.Length;
      int index = 0;
      for (int length = lines.Length; index < length; ++index)
      {
        string str = lines[index].TrimEnd('\r');
        if (num != 0 && str.Length >= num && str.Substring(0, num) == leadingWhitespace)
          str = str.Substring(num);
        lines[index] = str;
      }
      return string.Join("\r\n", ((IEnumerable<string>) lines).SkipWhile<string>((Func<string, bool>) (x => string.IsNullOrWhiteSpace(x)))).TrimEnd();
    }

    private static string GetCommonLeadingWhitespace(string[] lines)
    {
      if (lines == null)
        throw new ArgumentException(nameof (lines));
      if (lines.Length == 0)
        return (string) null;
      string[] array = ((IEnumerable<string>) lines).Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x))).ToArray<string>();
      if (array.Length < 1)
        return (string) null;
      int length1 = 0;
      string seed = array[0];
      int i = 0;
      for (int length2 = seed.Length; i < length2 && char.IsWhiteSpace(seed, i) && !((IEnumerable<string>) array).Any<string>((Func<string, bool>) (line => (int) line[i] != (int) seed[i])); ++i)
        ++length1;
      if (length1 > 0)
        return seed.Substring(0, length1);
      return (string) null;
    }
  }
}
