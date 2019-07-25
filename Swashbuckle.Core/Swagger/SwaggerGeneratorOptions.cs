// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.SwaggerGeneratorOptions
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace Swashbuckle.Swagger
{
  public class SwaggerGeneratorOptions
  {
    public SwaggerGeneratorOptions(
      Func<ApiDescription, string, bool> versionSupportResolver = null,
      IEnumerable<string> schemes = null,
      IDictionary<string, SecurityScheme> securityDefinitions = null,
      bool ignoreObsoleteActions = false,
      Func<ApiDescription, string> groupingKeySelector = null,
      IComparer<string> groupingKeyComparer = null,
      IDictionary<Type, Func<Schema>> customSchemaMappings = null,
      IEnumerable<ISchemaFilter> schemaFilters = null,
      IEnumerable<IModelFilter> modelFilters = null,
      bool ignoreObsoleteProperties = false,
      Func<Type, string> schemaIdSelector = null,
      bool describeAllEnumsAsStrings = false,
      bool describeStringEnumsInCamelCase = false,
      bool applyFiltersToAllSchemas = false,
      IEnumerable<IOperationFilter> operationFilters = null,
      IEnumerable<IDocumentFilter> documentFilters = null,
      Func<IEnumerable<ApiDescription>, ApiDescription> conflictingActionsResolver = null)
    {
      this.VersionSupportResolver = versionSupportResolver;
      this.Schemes = schemes;
      this.SecurityDefinitions = securityDefinitions;
      this.IgnoreObsoleteActions = ignoreObsoleteActions;
      this.GroupingKeySelector = groupingKeySelector ?? new Func<ApiDescription, string>(this.DefaultGroupingKeySelector);
      this.GroupingKeyComparer = groupingKeyComparer ?? (IComparer<string>) Comparer<string>.Default;
      this.CustomSchemaMappings = customSchemaMappings ?? (IDictionary<Type, Func<Schema>>) new Dictionary<Type, Func<Schema>>();
      this.SchemaFilters = schemaFilters ?? (IEnumerable<ISchemaFilter>) new List<ISchemaFilter>();
      this.ModelFilters = modelFilters ?? (IEnumerable<IModelFilter>) new List<IModelFilter>();
      this.IgnoreObsoleteProperties = ignoreObsoleteProperties;
      this.SchemaIdSelector = schemaIdSelector ?? new Func<Type, string>(SwaggerGeneratorOptions.DefaultSchemaIdSelector);
      this.DescribeAllEnumsAsStrings = describeAllEnumsAsStrings;
      this.DescribeStringEnumsInCamelCase = describeStringEnumsInCamelCase;
      this.ApplyFiltersToAllSchemas = applyFiltersToAllSchemas;
      this.OperationFilters = operationFilters ?? (IEnumerable<IOperationFilter>) new List<IOperationFilter>();
      this.DocumentFilters = documentFilters ?? (IEnumerable<IDocumentFilter>) new List<IDocumentFilter>();
      this.ConflictingActionsResolver = conflictingActionsResolver ?? new Func<IEnumerable<ApiDescription>, ApiDescription>(this.DefaultConflictingActionsResolver);
    }

    public Func<ApiDescription, string, bool> VersionSupportResolver { get; private set; }

    public IEnumerable<string> Schemes { get; private set; }

    public IDictionary<string, SecurityScheme> SecurityDefinitions { get; private set; }

    public bool IgnoreObsoleteActions { get; private set; }

    public Func<ApiDescription, string> GroupingKeySelector { get; private set; }

    public IComparer<string> GroupingKeyComparer { get; private set; }

    public IDictionary<Type, Func<Schema>> CustomSchemaMappings { get; private set; }

    public IEnumerable<ISchemaFilter> SchemaFilters { get; private set; }

    public IEnumerable<IModelFilter> ModelFilters { get; private set; }

    public bool IgnoreObsoleteProperties { get; private set; }

    public Func<Type, string> SchemaIdSelector { get; private set; }

    public bool DescribeAllEnumsAsStrings { get; private set; }

    public bool DescribeStringEnumsInCamelCase { get; private set; }

    public bool ApplyFiltersToAllSchemas { get; private set; }

    public IEnumerable<IOperationFilter> OperationFilters { get; private set; }

    public IEnumerable<IDocumentFilter> DocumentFilters { get; private set; }

    public Func<IEnumerable<ApiDescription>, ApiDescription> ConflictingActionsResolver { get; private set; }

    private string DefaultGroupingKeySelector(ApiDescription apiDescription)
    {
      return apiDescription.get_ActionDescriptor().get_ControllerDescriptor().get_ControllerName();
    }

    private static string DefaultSchemaIdSelector(Type type)
    {
      return type.FriendlyId(false);
    }

    private ApiDescription DefaultConflictingActionsResolver(
      IEnumerable<ApiDescription> apiDescriptions)
    {
      ApiDescription apiDescription = apiDescriptions.First<ApiDescription>();
      throw new NotSupportedException(string.Format("Not supported by Swagger 2.0: Multiple operations with path '{0}' and method '{1}'. See the config setting - \"ResolveConflictingActions\" for a potential workaround", (object) apiDescription.RelativePathSansQueryString(), (object) apiDescription.get_HttpMethod()));
    }
  }
}
