// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.SwaggerDocsConfig
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json;
using Swashbuckle.Swagger;
using Swashbuckle.Swagger.Annotations;
using Swashbuckle.Swagger.FromUriParams;
using Swashbuckle.Swagger.XmlComments;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.XPath;

namespace Swashbuckle.Application
{
  public class SwaggerDocsConfig
  {
    private VersionInfoBuilder _versionInfoBuilder;
    private Func<ApiDescription, string, bool> _versionSupportResolver;
    private IEnumerable<string> _schemes;
    private IDictionary<string, SecuritySchemeBuilder> _securitySchemeBuilders;
    private bool _prettyPrint;
    private bool _ignoreObsoleteActions;
    private Func<ApiDescription, string> _groupingKeySelector;
    private IComparer<string> _groupingKeyComparer;
    private readonly IDictionary<Type, Func<Schema>> _customSchemaMappings;
    private readonly IList<Func<ISchemaFilter>> _schemaFilters;
    private readonly IList<Func<IModelFilter>> _modelFilters;
    private Func<Type, string> _schemaIdSelector;
    private bool _ignoreObsoleteProperties;
    private bool _describeAllEnumsAsStrings;
    private bool _describeStringEnumsInCamelCase;
    private bool _applyFiltersToAllSchemas;
    private readonly IList<Func<IOperationFilter>> _operationFilters;
    private readonly IList<Func<IDocumentFilter>> _documentFilters;
    private readonly IList<Func<XPathDocument>> _xmlDocFactories;
    private Func<IEnumerable<ApiDescription>, ApiDescription> _conflictingActionsResolver;
    private Func<HttpRequestMessage, string> _rootUrlResolver;
    private Func<ISwaggerProvider, ISwaggerProvider> _customProviderFactory;

    public SwaggerDocsConfig()
    {
      this._versionInfoBuilder = new VersionInfoBuilder();
      this._securitySchemeBuilders = (IDictionary<string, SecuritySchemeBuilder>) new Dictionary<string, SecuritySchemeBuilder>();
      this._prettyPrint = false;
      this._ignoreObsoleteActions = false;
      this._customSchemaMappings = (IDictionary<Type, Func<Schema>>) new Dictionary<Type, Func<Schema>>();
      this._schemaFilters = (IList<Func<ISchemaFilter>>) new List<Func<ISchemaFilter>>();
      this._modelFilters = (IList<Func<IModelFilter>>) new List<Func<IModelFilter>>();
      this._ignoreObsoleteProperties = false;
      this._describeAllEnumsAsStrings = false;
      this._describeStringEnumsInCamelCase = false;
      this._applyFiltersToAllSchemas = false;
      this._operationFilters = (IList<Func<IOperationFilter>>) new List<Func<IOperationFilter>>();
      this._documentFilters = (IList<Func<IDocumentFilter>>) new List<Func<IDocumentFilter>>();
      this._xmlDocFactories = (IList<Func<XPathDocument>>) new List<Func<XPathDocument>>();
      this._rootUrlResolver = new Func<HttpRequestMessage, string>(SwaggerDocsConfig.DefaultRootUrlResolver);
      this.SchemaFilter<ApplySwaggerSchemaFilterAttributes>();
      this.OperationFilter<HandleFromUriParams>();
      this.OperationFilter<ApplySwaggerOperationAttributes>();
      this.OperationFilter<ApplySwaggerResponseAttributes>();
      this.OperationFilter<ApplySwaggerOperationFilterAttributes>();
    }

    public InfoBuilder SingleApiVersion(string version, string title)
    {
      this._versionSupportResolver = (Func<ApiDescription, string, bool>) null;
      this._versionInfoBuilder = new VersionInfoBuilder();
      return this._versionInfoBuilder.Version(version, title);
    }

    public void MultipleApiVersions(
      Func<ApiDescription, string, bool> versionSupportResolver,
      Action<VersionInfoBuilder> configure)
    {
      this._versionSupportResolver = versionSupportResolver;
      this._versionInfoBuilder = new VersionInfoBuilder();
      configure(this._versionInfoBuilder);
    }

    public void Schemes(IEnumerable<string> schemes)
    {
      this._schemes = schemes;
    }

    public BasicAuthSchemeBuilder BasicAuth(string name)
    {
      BasicAuthSchemeBuilder authSchemeBuilder = new BasicAuthSchemeBuilder();
      this._securitySchemeBuilders[name] = (SecuritySchemeBuilder) authSchemeBuilder;
      return authSchemeBuilder;
    }

    public ApiKeySchemeBuilder ApiKey(string name)
    {
      ApiKeySchemeBuilder keySchemeBuilder = new ApiKeySchemeBuilder();
      this._securitySchemeBuilders[name] = (SecuritySchemeBuilder) keySchemeBuilder;
      return keySchemeBuilder;
    }

    public OAuth2SchemeBuilder OAuth2(string name)
    {
      OAuth2SchemeBuilder oauth2SchemeBuilder = new OAuth2SchemeBuilder();
      this._securitySchemeBuilders[name] = (SecuritySchemeBuilder) oauth2SchemeBuilder;
      return oauth2SchemeBuilder;
    }

    public void PrettyPrint()
    {
      this._prettyPrint = true;
    }

    public void IgnoreObsoleteActions()
    {
      this._ignoreObsoleteActions = true;
    }

    public void GroupActionsBy(Func<ApiDescription, string> keySelector)
    {
      this._groupingKeySelector = keySelector;
    }

    public void OrderActionGroupsBy(IComparer<string> keyComparer)
    {
      this._groupingKeyComparer = keyComparer;
    }

    public void MapType<T>(Func<Schema> factory)
    {
      this.MapType(typeof (T), factory);
    }

    public void MapType(Type type, Func<Schema> factory)
    {
      this._customSchemaMappings.Add(type, factory);
    }

    public void SchemaFilter<TFilter>() where TFilter : ISchemaFilter, new()
    {
      this.SchemaFilter((Func<ISchemaFilter>) (() => (ISchemaFilter) new TFilter()));
    }

    public void SchemaFilter(Func<ISchemaFilter> factory)
    {
      this._schemaFilters.Add(factory);
    }

    internal void ModelFilter<TFilter>() where TFilter : IModelFilter, new()
    {
      this.ModelFilter((Func<IModelFilter>) (() => (IModelFilter) new TFilter()));
    }

    internal void ModelFilter(Func<IModelFilter> factory)
    {
      this._modelFilters.Add(factory);
    }

    public void SchemaId(Func<Type, string> schemaIdStrategy)
    {
      this._schemaIdSelector = schemaIdStrategy;
    }

    public void UseFullTypeNameInSchemaIds()
    {
      this._schemaIdSelector = (Func<Type, string>) (t => t.FriendlyId(true));
    }

    public void DescribeAllEnumsAsStrings(bool camelCase = false)
    {
      this._describeAllEnumsAsStrings = true;
      this._describeStringEnumsInCamelCase = camelCase;
    }

    public void IgnoreObsoleteProperties()
    {
      this._ignoreObsoleteProperties = true;
    }

    [Obsolete("This will be removed in 6.0.0; it will always be true.")]
    public void ApplyFiltersToAllSchemas()
    {
      this._applyFiltersToAllSchemas = true;
    }

    public void OperationFilter<TFilter>() where TFilter : IOperationFilter, new()
    {
      this.OperationFilter((Func<IOperationFilter>) (() => (IOperationFilter) new TFilter()));
    }

    public void OperationFilter(Func<IOperationFilter> factory)
    {
      this._operationFilters.Add(factory);
    }

    public void DocumentFilter<TFilter>() where TFilter : IDocumentFilter, new()
    {
      this.DocumentFilter((Func<IDocumentFilter>) (() => (IDocumentFilter) new TFilter()));
    }

    public void DocumentFilter(Func<IDocumentFilter> factory)
    {
      this._documentFilters.Add(factory);
    }

    public void IncludeXmlComments(Func<XPathDocument> xmlDocFactory)
    {
      this._xmlDocFactories.Add(xmlDocFactory);
    }

    public void IncludeXmlComments(string filePath)
    {
      this._xmlDocFactories.Add((Func<XPathDocument>) (() => new XPathDocument(filePath)));
    }

    public void ResolveConflictingActions(
      Func<IEnumerable<ApiDescription>, ApiDescription> conflictingActionsResolver)
    {
      this._conflictingActionsResolver = conflictingActionsResolver;
    }

    public void RootUrl(Func<HttpRequestMessage, string> rootUrlResolver)
    {
      this._rootUrlResolver = rootUrlResolver;
    }

    public void CustomProvider(
      Func<ISwaggerProvider, ISwaggerProvider> customProviderFactory)
    {
      this._customProviderFactory = customProviderFactory;
    }

    internal ISwaggerProvider GetSwaggerProvider(HttpRequestMessage swaggerRequest)
    {
      HttpConfiguration configuration = HttpRequestMessageExtensions.GetConfiguration(swaggerRequest);
      Dictionary<string, SecurityScheme> dictionary = this._securitySchemeBuilders.Any<KeyValuePair<string, SecuritySchemeBuilder>>() ? this._securitySchemeBuilders.ToDictionary<KeyValuePair<string, SecuritySchemeBuilder>, string, SecurityScheme>((Func<KeyValuePair<string, SecuritySchemeBuilder>, string>) (kvp => kvp.Key), (Func<KeyValuePair<string, SecuritySchemeBuilder>, SecurityScheme>) (kvp => kvp.Value.Build())) : (Dictionary<string, SecurityScheme>) null;
      List<IModelFilter> list1 = this._modelFilters.Select<Func<IModelFilter>, IModelFilter>((Func<Func<IModelFilter>, IModelFilter>) (factory => factory())).ToList<IModelFilter>();
      List<IOperationFilter> list2 = this._operationFilters.Select<Func<IOperationFilter>, IOperationFilter>((Func<Func<IOperationFilter>, IOperationFilter>) (factory => factory())).ToList<IOperationFilter>();
      foreach (Func<XPathDocument> xmlDocFactory in (IEnumerable<Func<XPathDocument>>) this._xmlDocFactories)
      {
        XPathDocument xmlDoc = xmlDocFactory();
        list1.Insert(0, (IModelFilter) new ApplyXmlTypeComments(xmlDoc));
        list2.Insert(0, (IOperationFilter) new ApplyXmlActionComments(xmlDoc));
      }
      SwaggerGeneratorOptions options = new SwaggerGeneratorOptions(this._versionSupportResolver, this._schemes, (IDictionary<string, SecurityScheme>) dictionary, this._ignoreObsoleteActions, this._groupingKeySelector, this._groupingKeyComparer, this._customSchemaMappings, (IEnumerable<ISchemaFilter>) this._schemaFilters.Select<Func<ISchemaFilter>, ISchemaFilter>((Func<Func<ISchemaFilter>, ISchemaFilter>) (factory => factory())).ToList<ISchemaFilter>(), (IEnumerable<IModelFilter>) list1, this._ignoreObsoleteProperties, this._schemaIdSelector, this._describeAllEnumsAsStrings, this._describeStringEnumsInCamelCase, this._applyFiltersToAllSchemas, (IEnumerable<IOperationFilter>) list2, (IEnumerable<IDocumentFilter>) this._documentFilters.Select<Func<IDocumentFilter>, IDocumentFilter>((Func<Func<IDocumentFilter>, IDocumentFilter>) (factory => factory())).ToList<IDocumentFilter>(), this._conflictingActionsResolver);
      SwaggerGenerator swaggerGenerator = new SwaggerGenerator(ServicesExtensions.GetApiExplorer(configuration.get_Services()), configuration.SerializerSettingsOrDefault(), this._versionInfoBuilder.Build(), options);
      if (this._customProviderFactory == null)
        return (ISwaggerProvider) swaggerGenerator;
      return this._customProviderFactory((ISwaggerProvider) swaggerGenerator);
    }

    internal string GetRootUrl(HttpRequestMessage swaggerRequest)
    {
      return this._rootUrlResolver(swaggerRequest);
    }

    internal IEnumerable<string> GetApiVersions()
    {
      return this._versionInfoBuilder.Build().Select<KeyValuePair<string, Info>, string>((Func<KeyValuePair<string, Info>, string>) (entry => entry.Key));
    }

    internal Formatting GetFormatting()
    {
      if (!this._prettyPrint)
        return (Formatting) 0;
      return (Formatting) 1;
    }

    public static string DefaultRootUrlResolver(HttpRequestMessage request)
    {
      return new UriBuilder(SwaggerDocsConfig.GetHeaderValue(request, "X-Forwarded-Proto") ?? request.RequestUri.Scheme, SwaggerDocsConfig.GetHeaderValue(request, "X-Forwarded-Host") ?? request.RequestUri.Host, int.Parse(SwaggerDocsConfig.GetHeaderValue(request, "X-Forwarded-Port") ?? request.RequestUri.Port.ToString((IFormatProvider) CultureInfo.InvariantCulture)), (SwaggerDocsConfig.GetHeaderValue(request, "X-Forwarded-Prefix") ?? string.Empty) + HttpRequestMessageExtensions.GetConfiguration(request).get_VirtualPathRoot()).Uri.AbsoluteUri.TrimEnd('/');
    }

    private static string GetHeaderValue(HttpRequestMessage request, string headerName)
    {
      IEnumerable<string> values;
      if (!request.Headers.TryGetValues(headerName, out values))
        return (string) null;
      return values.FirstOrDefault<string>();
    }
  }
}
