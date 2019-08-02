// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.XmlComments.ApplyXmlActionComments
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Xml.XPath;

namespace Swashbuckle.Swagger.XmlComments
{
  public class ApplyXmlActionComments : IOperationFilter
  {
    private const string MemberXPath = "/doc/members/member[@name='{0}']";
    private const string SummaryXPath = "summary";
    private const string RemarksXPath = "remarks";
    private const string ParamXPath = "param[@name='{0}']";
    private const string ResponseXPath = "response";
    private readonly XPathDocument _xmlDoc;

    public ApplyXmlActionComments(string filePath)
      : this(new XPathDocument(filePath))
    {
    }

    public ApplyXmlActionComments(XPathDocument xmlDoc)
    {
      this._xmlDoc = xmlDoc;
    }

    public void Apply(
      Operation operation,
      SchemaRegistry schemaRegistry,
      ApiDescription apiDescription)
    {
      ReflectedHttpActionDescriptor actionDescriptor = apiDescription.get_ActionDescriptor() as ReflectedHttpActionDescriptor;
      if (actionDescriptor == null)
        return;
      XPathNavigator navigator;
      lock (this._xmlDoc)
        navigator = this._xmlDoc.CreateNavigator();
      string commentIdForMethod = XmlCommentsIdHelper.GetCommentIdForMethod(actionDescriptor.get_MethodInfo());
      XPathNavigator methodNode = navigator.SelectSingleNode(string.Format("/doc/members/member[@name='{0}']", (object) commentIdForMethod));
      if (methodNode == null)
        return;
      XPathNavigator node1 = methodNode.SelectSingleNode("summary");
      if (node1 != null)
        operation.summary = node1.ExtractContent();
      XPathNavigator node2 = methodNode.SelectSingleNode("remarks");
      if (node2 != null)
        operation.description = node2.ExtractContent();
      ApplyXmlActionComments.ApplyParamComments(operation, methodNode, actionDescriptor.get_MethodInfo());
      ApplyXmlActionComments.ApplyResponseComments(operation, methodNode);
    }

    private static void ApplyParamComments(
      Operation operation,
      XPathNavigator methodNode,
      MethodInfo method)
    {
      if (operation.parameters == null)
        return;
      foreach (Parameter parameter1 in (IEnumerable<Parameter>) operation.parameters)
      {
        Parameter parameter = parameter1;
        ParameterInfo parameterInfo = ((IEnumerable<ParameterInfo>) method.GetParameters()).FirstOrDefault<ParameterInfo>((Func<ParameterInfo, bool>) (paramInfo =>
        {
          if (!ApplyXmlActionComments.HasBoundName(paramInfo, parameter.name))
            return paramInfo.Name == parameter.name;
          return true;
        }));
        if (parameterInfo != null)
        {
          XPathNavigator node = methodNode.SelectSingleNode(string.Format("param[@name='{0}']", (object) parameterInfo.Name));
          if (node != null)
            parameter.description = node.ExtractContent();
        }
      }
    }

    private static void ApplyResponseComments(Operation operation, XPathNavigator methodNode)
    {
      XPathNodeIterator xpathNodeIterator = methodNode.Select("response");
      if (xpathNodeIterator.Count <= 0)
        return;
      Response response1 = operation.responses.First<KeyValuePair<string, Response>>().Value;
      operation.responses.Clear();
      while (xpathNodeIterator.MoveNext())
      {
        string attribute = xpathNodeIterator.Current.GetAttribute("code", "");
        string content = xpathNodeIterator.Current.ExtractContent();
        Response response2 = new Response()
        {
          description = content,
          schema = attribute.StartsWith("2") ? response1.schema : (Schema) null
        };
        operation.responses[attribute] = response2;
      }
    }

    private static bool HasBoundName(ParameterInfo paramInfo, string name)
    {
      FromUriAttribute fromUriAttribute = paramInfo.GetCustomAttributes(false).OfType<FromUriAttribute>().FirstOrDefault<FromUriAttribute>();
      if (fromUriAttribute != null)
        return ((ModelBinderAttribute) fromUriAttribute).get_Name() == name;
      return false;
    }
  }
}
