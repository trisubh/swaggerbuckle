// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.SwaggerGenerator
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Description;

namespace Swashbuckle.Swagger
{
  public class SwaggerGenerator : ISwaggerProvider
  {
    private readonly IApiExplorer _apiExplorer;
    private readonly JsonSerializerSettings _jsonSerializerSettings;
    private readonly IDictionary<string, Info> _apiVersions;
    private readonly SwaggerGeneratorOptions _options;

    public SwaggerGenerator(
      IApiExplorer apiExplorer,
      JsonSerializerSettings jsonSerializerSettings,
      IDictionary<string, Info> apiVersions,
      SwaggerGeneratorOptions options = null)
    {
      this._apiExplorer = apiExplorer;
      this._jsonSerializerSettings = jsonSerializerSettings;
      this._apiVersions = apiVersions;
      this._options = options ?? new SwaggerGeneratorOptions((Func<ApiDescription, string, bool>) null, (IEnumerable<string>) null, (IDictionary<string, SecurityScheme>) null, false, (Func<ApiDescription, string>) null, (IComparer<string>) null, (IDictionary<Type, Func<Schema>>) null, (IEnumerable<ISchemaFilter>) null, (IEnumerable<IModelFilter>) null, false, (Func<Type, string>) null, false, false, false, (IEnumerable<IOperationFilter>) null, (IEnumerable<IDocumentFilter>) null, (Func<IEnumerable<ApiDescription>, ApiDescription>) null);
    }

    public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
    {
      SchemaRegistry schemaRegistry = new SchemaRegistry(this._jsonSerializerSettings, this._options.CustomSchemaMappings, this._options.SchemaFilters, this._options.ModelFilters, this._options.IgnoreObsoleteProperties, this._options.SchemaIdSelector, this._options.DescribeAllEnumsAsStrings, this._options.DescribeStringEnumsInCamelCase, this._options.ApplyFiltersToAllSchemas);
      Info info;
      this._apiVersions.TryGetValue(apiVersion, out info);
      if (info == null)
        throw new UnknownApiVersion(apiVersion);
      Dictionary<string, PathItem> dictionary = ((IEnumerable<ApiDescription>) this.GetApiDescriptionsFor(apiVersion).Where<ApiDescription>((Func<ApiDescription, bool>) (apiDesc =>
      {
        if (this._options.IgnoreObsoleteActions)
          return !apiDesc.IsObsolete();
        return true;
      })).OrderBy<ApiDescription, string>(this._options.GroupingKeySelector, this._options.GroupingKeyComparer)).GroupBy<ApiDescription, string>((Func<ApiDescription, string>) (apiDesc => apiDesc.RelativePathSansQueryString())).ToDictionary<IGrouping<string, ApiDescription>, string, PathItem>((Func<IGrouping<string, ApiDescription>, string>) (group => "/" + group.Key), (Func<IGrouping<string, ApiDescription>, PathItem>) (group => this.CreatePathItem((IEnumerable<ApiDescription>) group, schemaRegistry)));
      Uri uri = new Uri(rootUrl);
      string str = !uri.IsDefaultPort ? ":" + (object) uri.Port : string.Empty;
      SwaggerDocument swaggerDocument1 = new SwaggerDocument();
      swaggerDocument1.info = info;
      swaggerDocument1.host = uri.Host + str;
      swaggerDocument1.basePath = uri.AbsolutePath != "/" ? uri.AbsolutePath : (string) null;
      SwaggerDocument swaggerDocument2 = swaggerDocument1;
      List<string> list;
      if (this._options.Schemes == null)
        list = ((IEnumerable<string>) new string[1]
        {
          uri.Scheme
        }).ToList<string>();
      else
        list = this._options.Schemes.ToList<string>();
      swaggerDocument2.schemes = (IList<string>) list;
      swaggerDocument1.paths = (IDictionary<string, PathItem>) dictionary;
      swaggerDocument1.definitions = schemaRegistry.Definitions;
      swaggerDocument1.securityDefinitions = this._options.SecurityDefinitions;
      SwaggerDocument swaggerDoc = swaggerDocument1;
      foreach (IDocumentFilter documentFilter in this._options.DocumentFilters)
        documentFilter.Apply(swaggerDoc, schemaRegistry, this._apiExplorer);
      return swaggerDoc;
    }

    private IEnumerable<ApiDescription> GetApiDescriptionsFor(
      string apiVersion)
    {
      if (this._options.VersionSupportResolver != null)
        return ((IEnumerable<ApiDescription>) this._apiExplorer.get_ApiDescriptions()).Where<ApiDescription>((Func<ApiDescription, bool>) (apiDesc => this._options.VersionSupportResolver(apiDesc, apiVersion)));
      return (IEnumerable<ApiDescription>) this._apiExplorer.get_ApiDescriptions();
    }

    private PathItem CreatePathItem(
      IEnumerable<ApiDescription> apiDescriptions,
      SchemaRegistry schemaRegistry)
    {
      PathItem pathItem = new PathItem();
      using (IEnumerator<IGrouping<string, ApiDescription>> enumerator = apiDescriptions.GroupBy<ApiDescription, string>((Func<ApiDescription, string>) (apiDesc => apiDesc.get_HttpMethod().Method.ToLower())).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          IGrouping<string, ApiDescription> current = enumerator.Current;
          string key = current.Key;
          ApiDescription apiDesc = ((IEnumerable<ApiDescription>) current).Count<ApiDescription>() == 1 ? ((IEnumerable<ApiDescription>) current).First<ApiDescription>() : this._options.ConflictingActionsResolver((IEnumerable<ApiDescription>) current);
          switch (key)
          {
            case "get":
              pathItem.get = this.CreateOperation(apiDesc, schemaRegistry);
              continue;
            case "put":
              pathItem.put = this.CreateOperation(apiDesc, schemaRegistry);
              continue;
            case "post":
              pathItem.post = this.CreateOperation(apiDesc, schemaRegistry);
              continue;
            case "delete":
              pathItem.delete = this.CreateOperation(apiDesc, schemaRegistry);
              continue;
            case "options":
              pathItem.options = this.CreateOperation(apiDesc, schemaRegistry);
              continue;
            case "head":
              pathItem.head = this.CreateOperation(apiDesc, schemaRegistry);
              continue;
            case "patch":
              pathItem.patch = this.CreateOperation(apiDesc, schemaRegistry);
              continue;
            default:
              continue;
          }
        }
      }
      return pathItem;
    }

    private Operation CreateOperation(
      ApiDescription apiDesc,
      SchemaRegistry schemaRegistry)
    {
      List<Parameter> list = ((IEnumerable<ApiParameterDescription>) apiDesc.get_ParameterDescriptions()).Select<ApiParameterDescription, Parameter>((Func<ApiParameterDescription, Parameter>) (paramDesc => this.CreateParameter(this.GetParameterLocation(apiDesc, paramDesc), paramDesc, schemaRegistry))).ToList<Parameter>();
      Dictionary<string, Response> dictionary = new Dictionary<string, Response>();
      Type type = apiDesc.ResponseType();
      if (type == (Type) null || type == typeof (void))
        dictionary.Add("204", new Response()
        {
          description = "No Content"
        });
      else
        dictionary.Add("200", new Response()
        {
          description = "OK",
          schema = schemaRegistry.GetOrRegister(type)
        });
      Operation operation = new Operation()
      {
        tags = (IList<string>) new string[1]
        {
          this._options.GroupingKeySelector(apiDesc)
        },
        operationId = apiDesc.FriendlyId(),
        produces = (IList<string>) apiDesc.Produces().ToList<string>(),
        consumes = (IList<string>) apiDesc.Consumes().ToList<string>(),
        parameters = list.Any<Parameter>() ? (IList<Parameter>) list : (IList<Parameter>) null,
        responses = (IDictionary<string, Response>) dictionary,
        deprecated = apiDesc.IsObsolete() ? new bool?(true) : new bool?()
      };
      foreach (IOperationFilter operationFilter in this._options.OperationFilters)
        operationFilter.Apply(operation, schemaRegistry, apiDesc);
      return operation;
    }

    private string GetParameterLocation(ApiDescription apiDesc, ApiParameterDescription paramDesc)
    {
      if (apiDesc.RelativePathSansQueryString().Contains("{" + paramDesc.get_Name() + "}"))
        return "path";
      return paramDesc.get_Source() == 1 && apiDesc.get_HttpMethod() != HttpMethod.Get ? "body" : "query";
    }

    private Parameter CreateParameter(
      string location,
      ApiParameterDescription paramDesc,
      SchemaRegistry schemaRegistry)
    {
      Parameter partialSchema = new Parameter()
      {
        @in = location,
        name = paramDesc.get_Name()
      };
      if (paramDesc.get_ParameterDescriptor() == null)
      {
        partialSchema.type = "string";
        partialSchema.required = new bool?(true);
        return partialSchema;
      }
      partialSchema.required = new bool?(location == "path" || !paramDesc.get_ParameterDescriptor().get_IsOptional());
      partialSchema.@default = paramDesc.get_ParameterDescriptor().get_DefaultValue();
      Schema orRegister = schemaRegistry.GetOrRegister(paramDesc.get_ParameterDescriptor().get_ParameterType());
      if (partialSchema.@in == "body")
        partialSchema.schema = orRegister;
      else
        partialSchema.PopulateFrom(orRegister);
      return partialSchema;
    }
  }
}
