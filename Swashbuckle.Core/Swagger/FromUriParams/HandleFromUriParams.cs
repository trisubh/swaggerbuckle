// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.FromUriParams.HandleFromUriParams
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace Swashbuckle.Swagger.FromUriParams
{
  public class HandleFromUriParams : IOperationFilter
  {
    public void Apply(
      Operation operation,
      SchemaRegistry schemaRegistry,
      ApiDescription apiDescription)
    {
      if (operation.parameters == null)
        return;
      HandleFromUriParams.HandleFromUriArrayParams(operation);
      this.HandleFromUriObjectParams(operation, schemaRegistry, apiDescription);
    }

    private static void HandleFromUriArrayParams(Operation operation)
    {
      foreach (PartialSchema partialSchema in operation.parameters.Where<Parameter>((Func<Parameter, bool>) (param =>
      {
        if (param.@in == "query")
          return param.type == "array";
        return false;
      })).ToArray<Parameter>())
        partialSchema.collectionFormat = "multi";
    }

    private void HandleFromUriObjectParams(
      Operation operation,
      SchemaRegistry schemaRegistry,
      ApiDescription apiDescription)
    {
      foreach (Parameter parameter in operation.parameters.Where<Parameter>((Func<Parameter, bool>) (param =>
      {
        if (param.@in == "query")
          return param.type == null;
        return false;
      })).ToArray<Parameter>())
      {
        Parameter objectParam = parameter;
        Type parameterType = ((IEnumerable<ApiParameterDescription>) apiDescription.get_ParameterDescriptions()).Single<ApiParameterDescription>((Func<ApiParameterDescription, bool>) (paramDesc => paramDesc.get_Name() == objectParam.name)).get_ParameterDescriptor().get_ParameterType();
        Schema orRegister = schemaRegistry.GetOrRegister(parameterType);
        this.ExtractAndAddQueryParams(schemaRegistry.Definitions[orRegister.@ref.Replace("#/definitions/", "")], string.IsNullOrEmpty(objectParam.name) ? "" : objectParam.name + ".", objectParam.required, schemaRegistry, operation.parameters);
        operation.parameters.Remove(objectParam);
      }
    }

    private void ExtractAndAddQueryParams(
      Schema sourceSchema,
      string sourceQualifier,
      bool? sourceRequired,
      SchemaRegistry schemaRegistry,
      IList<Parameter> operationParams)
    {
      foreach (KeyValuePair<string, Schema> property in (IEnumerable<KeyValuePair<string, Schema>>) sourceSchema.properties)
      {
        Schema schema = property.Value;
        bool? nullable1 = schema.readOnly;
        if ((!nullable1.GetValueOrDefault() ? 0 : (nullable1.HasValue ? 1 : 0)) == 0)
        {
          bool? nullable2 = sourceRequired;
          bool flag = (!nullable2.GetValueOrDefault() ? 0 : (nullable2.HasValue ? 1 : 0)) != 0 && sourceSchema.required != null && sourceSchema.required.Contains(property.Key);
          if (schema.@ref != null)
          {
            this.ExtractAndAddQueryParams(schemaRegistry.Definitions[schema.@ref.Replace("#/definitions/", "")], sourceQualifier + property.Key.ToCamelCase() + ".", new bool?(flag), schemaRegistry, operationParams);
          }
          else
          {
            Parameter partialSchema = new Parameter()
            {
              name = sourceQualifier + property.Key.ToCamelCase(),
              @in = "query",
              required = new bool?(flag),
              description = property.Value.description
            };
            partialSchema.PopulateFrom(property.Value);
            if (partialSchema.type == "array")
              partialSchema.collectionFormat = "multi";
            operationParams.Add(partialSchema);
          }
        }
      }
    }
  }
}
