// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.XmlComments.XPathNavigatorExtensions
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace Swashbuckle.Swagger.XmlComments
{
  public static class XPathNavigatorExtensions
  {
    private static Regex ParamPattern = new Regex("<(see|paramref) (name|cref)=\"([TPF]{1}:)?(?<display>.+?)\" />");
    private static Regex ConstPattern = new Regex("<c>(?<display>.+?)</c>");

    public static string ExtractContent(this XPathNavigator node)
    {
      if (node == null)
        return (string) null;
      return XmlTextHelper.NormalizeIndentation(XPathNavigatorExtensions.ConstPattern.Replace(XPathNavigatorExtensions.ParamPattern.Replace(node.InnerXml, new MatchEvaluator(XPathNavigatorExtensions.GetParamRefName)), new MatchEvaluator(XPathNavigatorExtensions.GetConstRefName)));
    }

    private static string GetConstRefName(Match match)
    {
      if (match.Groups.Count != 2)
        return (string) null;
      return match.Groups["display"].Value;
    }

    private static string GetParamRefName(Match match)
    {
      if (match.Groups.Count != 5)
        return (string) null;
      return "{" + match.Groups["display"].Value + "}";
    }
  }
}
