// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.XmlComments.ApplyXmlTypeComments
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml.XPath;

namespace Swashbuckle.Swagger.XmlComments
{
  public class ApplyXmlTypeComments : IModelFilter
  {
    private const string MemberXPath = "/doc/members/member[@name='{0}']";
    private const string SummaryTag = "summary";
    private readonly XPathDocument _xmlDoc;

    public ApplyXmlTypeComments(string filePath)
      : this(new XPathDocument(filePath))
    {
    }

    public ApplyXmlTypeComments(XPathDocument xmlDoc)
    {
      this._xmlDoc = xmlDoc;
    }

    public void Apply(Schema model, ModelFilterContext context)
    {
      XPathNavigator navigator;
      lock (this._xmlDoc)
        navigator = this._xmlDoc.CreateNavigator();
      string commentIdForType = XmlCommentsIdHelper.GetCommentIdForType(context.SystemType);
      XPathNavigator xpathNavigator = navigator.SelectSingleNode(string.Format("/doc/members/member[@name='{0}']", (object) commentIdForType));
      if (xpathNavigator != null)
      {
        XPathNavigator node = xpathNavigator.SelectSingleNode("summary");
        if (node != null)
          model.description = node.ExtractContent();
      }
      if (model.properties == null)
        return;
      foreach (KeyValuePair<string, Schema> property1 in (IEnumerable<KeyValuePair<string, Schema>>) model.properties)
      {
        JsonProperty property2 = ((KeyedCollection<string, JsonProperty>) context.JsonObjectContract.get_Properties())[property1.Key];
        if (property2 != null)
          ApplyXmlTypeComments.ApplyPropertyComments(navigator, property1.Value, property2.PropertyInfo());
      }
    }

    private static void ApplyPropertyComments(
      XPathNavigator navigator,
      Schema propertySchema,
      PropertyInfo propertyInfo)
    {
      if (propertyInfo == (PropertyInfo) null)
        return;
      string commentIdForProperty = XmlCommentsIdHelper.GetCommentIdForProperty(propertyInfo);
      XPathNavigator xpathNavigator = navigator.SelectSingleNode(string.Format("/doc/members/member[@name='{0}']", (object) commentIdForProperty));
      if (xpathNavigator == null)
        return;
      XPathNavigator node = xpathNavigator.SelectSingleNode("summary");
      if (node == null)
        return;
      propertySchema.description = node.ExtractContent();
    }
  }
}
