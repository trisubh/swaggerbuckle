// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.SchemaRegistry
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Swashbuckle.Swagger
{
  public class SchemaRegistry
  {
    private readonly JsonSerializerSettings _jsonSerializerSettings;
    private readonly IDictionary<Type, Func<Schema>> _customSchemaMappings;
    private readonly IEnumerable<ISchemaFilter> _schemaFilters;
    private readonly IEnumerable<IModelFilter> _modelFilters;
    private readonly Func<Type, string> _schemaIdSelector;
    private readonly bool _ignoreObsoleteProperties;
    private readonly bool _describeAllEnumsAsStrings;
    private readonly bool _describeStringEnumsInCamelCase;
    private readonly bool _applyFiltersToAllSchemas;
    private readonly IContractResolver _contractResolver;
    private IDictionary<Type, SchemaRegistry.WorkItem> _workItems;

    public SchemaRegistry(
      JsonSerializerSettings jsonSerializerSettings,
      IDictionary<Type, Func<Schema>> customSchemaMappings,
      IEnumerable<ISchemaFilter> schemaFilters,
      IEnumerable<IModelFilter> modelFilters,
      bool ignoreObsoleteProperties,
      Func<Type, string> schemaIdSelector,
      bool describeAllEnumsAsStrings,
      bool describeStringEnumsInCamelCase,
      bool applyFiltersToAllSchemas)
    {
      this._jsonSerializerSettings = jsonSerializerSettings;
      this._customSchemaMappings = customSchemaMappings;
      this._schemaFilters = schemaFilters;
      this._modelFilters = modelFilters;
      this._schemaIdSelector = schemaIdSelector;
      this._ignoreObsoleteProperties = ignoreObsoleteProperties;
      this._describeAllEnumsAsStrings = describeAllEnumsAsStrings;
      this._describeStringEnumsInCamelCase = describeStringEnumsInCamelCase;
      this._applyFiltersToAllSchemas = applyFiltersToAllSchemas;
      this._contractResolver = jsonSerializerSettings.get_ContractResolver() ?? (IContractResolver) new DefaultContractResolver();
      this._workItems = (IDictionary<Type, SchemaRegistry.WorkItem>) new Dictionary<Type, SchemaRegistry.WorkItem>();
      this.Definitions = (IDictionary<string, Schema>) new Dictionary<string, Schema>();
    }

    public Schema GetOrRegister(Type type)
    {
      Schema inlineSchema = this.CreateInlineSchema(type);
      SchemaRegistry.WorkItem workItem;
      for (; this._workItems.Any<KeyValuePair<Type, SchemaRegistry.WorkItem>>((Func<KeyValuePair<Type, SchemaRegistry.WorkItem>, bool>) (entry =>
      {
        if (entry.Value.Schema == null)
          return !entry.Value.InProgress;
        return false;
      })); workItem.InProgress = false)
      {
        KeyValuePair<Type, SchemaRegistry.WorkItem> keyValuePair = this._workItems.First<KeyValuePair<Type, SchemaRegistry.WorkItem>>((Func<KeyValuePair<Type, SchemaRegistry.WorkItem>, bool>) (entry =>
        {
          if (entry.Value.Schema == null)
            return !entry.Value.InProgress;
          return false;
        }));
        workItem = keyValuePair.Value;
        workItem.InProgress = true;
        workItem.Schema = this.CreateDefinitionSchema(keyValuePair.Key);
        this.Definitions.Add(workItem.SchemaId, workItem.Schema);
      }
      return inlineSchema;
    }

    public IDictionary<string, Schema> Definitions { get; private set; }

    private Schema CreateInlineSchema(Type type)
    {
      JsonContract jsonContract = this._contractResolver.ResolveContract(type);
      if (this._customSchemaMappings.ContainsKey(type))
        return this.FilterSchema(this._customSchemaMappings[type](), jsonContract);
      if (jsonContract is JsonPrimitiveContract)
        return this.FilterSchema(this.CreatePrimitiveSchema((JsonPrimitiveContract) jsonContract), jsonContract);
      JsonDictionaryContract dictionaryContract = jsonContract as JsonDictionaryContract;
      if (dictionaryContract != null)
      {
        if (!dictionaryContract.IsSelfReferencing())
          return this.FilterSchema(this.CreateDictionarySchema(dictionaryContract), jsonContract);
        return this.CreateRefSchema(type);
      }
      JsonArrayContract arrayContract = jsonContract as JsonArrayContract;
      if (arrayContract != null)
      {
        if (!arrayContract.IsSelfReferencing())
          return this.FilterSchema(this.CreateArraySchema(arrayContract), jsonContract);
        return this.CreateRefSchema(type);
      }
      JsonObjectContract objectContract = jsonContract as JsonObjectContract;
      if (objectContract != null && !objectContract.IsAmbiguous())
        return this.CreateRefSchema(type);
      return this.FilterSchema(new Schema()
      {
        type = "object"
      }, jsonContract);
    }

    private Schema CreateDefinitionSchema(Type type)
    {
      JsonContract jsonContract = this._contractResolver.ResolveContract(type);
      if (jsonContract is JsonDictionaryContract)
        return this.FilterSchema(this.CreateDictionarySchema((JsonDictionaryContract) jsonContract), jsonContract);
      if (jsonContract is JsonArrayContract)
        return this.FilterSchema(this.CreateArraySchema((JsonArrayContract) jsonContract), jsonContract);
      if (jsonContract is JsonObjectContract)
        return this.FilterSchema(this.CreateObjectSchema((JsonObjectContract) jsonContract), jsonContract);
      throw new InvalidOperationException(string.Format("Unsupported type - {0} for Defintitions. Must be Dictionary, Array or Object", (object) type));
    }

    private Schema CreatePrimitiveSchema(JsonPrimitiveContract primitiveContract)
    {
      Type underlyingType = Nullable.GetUnderlyingType(((JsonContract) primitiveContract).get_UnderlyingType());
      if ((object) underlyingType == null)
        underlyingType = ((JsonContract) primitiveContract).get_UnderlyingType();
      Type type = underlyingType;
      if (type.IsEnum)
        return this.CreateEnumSchema(primitiveContract, type);
      switch (type.FullName)
      {
        case "System.Boolean":
          return new Schema() { type = "boolean" };
        case "System.Byte":
        case "System.SByte":
        case "System.Int16":
        case "System.UInt16":
        case "System.Int32":
        case "System.UInt32":
          return new Schema()
          {
            type = "integer",
            format = "int32"
          };
        case "System.Int64":
        case "System.UInt64":
          return new Schema()
          {
            type = "integer",
            format = "int64"
          };
        case "System.Single":
          return new Schema()
          {
            type = "number",
            format = "float"
          };
        case "System.Double":
        case "System.Decimal":
          return new Schema()
          {
            type = "number",
            format = "double"
          };
        case "System.Byte[]":
          return new Schema()
          {
            type = "string",
            format = "byte"
          };
        case "System.DateTime":
        case "System.DateTimeOffset":
          return new Schema()
          {
            type = "string",
            format = "date-time"
          };
        case "System.Guid":
          return new Schema()
          {
            type = "string",
            format = "uuid",
            example = (object) Guid.Empty
          };
        default:
          return new Schema() { type = "string" };
      }
    }

    private Schema CreateEnumSchema(JsonPrimitiveContract primitiveContract, Type type)
    {
      StringEnumConverter stringEnumConverter = ((JsonContract) primitiveContract).get_Converter() as StringEnumConverter ?? ((IEnumerable) this._jsonSerializerSettings.get_Converters()).OfType<StringEnumConverter>().FirstOrDefault<StringEnumConverter>();
      if (this._describeAllEnumsAsStrings || stringEnumConverter != null)
      {
        bool flag = this._describeStringEnumsInCamelCase || stringEnumConverter != null && stringEnumConverter.get_CamelCaseText();
        return new Schema()
        {
          type = "string",
          @enum = flag ? (IList<object>) ((IEnumerable<string>) type.GetEnumNamesForSerialization()).Select<string, string>((Func<string, string>) (name => name.ToCamelCase())).ToArray<string>() : (IList<object>) type.GetEnumNamesForSerialization()
        };
      }
      return new Schema()
      {
        type = "integer",
        format = "int32",
        @enum = (IList<object>) type.GetEnumValues().Cast<object>().ToArray<object>()
      };
    }

    private Schema CreateDictionarySchema(JsonDictionaryContract dictionaryContract)
    {
      Type type1 = dictionaryContract.get_DictionaryKeyType();
      if ((object) type1 == null)
        type1 = typeof (object);
      Type enumType = type1;
      Type type2 = dictionaryContract.get_DictionaryValueType();
      if ((object) type2 == null)
        type2 = typeof (object);
      Type valueType = type2;
      if (enumType.IsEnum)
        return new Schema()
        {
          type = "object",
          properties = (IDictionary<string, Schema>) ((IEnumerable<string>) Enum.GetNames(enumType)).ToDictionary<string, string, Schema>((Func<string, string>) (name => dictionaryContract.get_DictionaryKeyResolver()(name)), (Func<string, Schema>) (name => this.CreateInlineSchema(valueType)))
        };
      return new Schema()
      {
        type = "object",
        additionalProperties = this.CreateInlineSchema(valueType)
      };
    }

    private Schema CreateArraySchema(JsonArrayContract arrayContract)
    {
      Type type1 = arrayContract.get_CollectionItemType();
      if ((object) type1 == null)
        type1 = typeof (object);
      Type type2 = type1;
      return new Schema()
      {
        type = "array",
        items = this.CreateInlineSchema(type2)
      };
    }

    private Schema CreateObjectSchema(JsonObjectContract jsonContract)
    {
      Dictionary<string, Schema> dictionary = ((IEnumerable<JsonProperty>) jsonContract.get_Properties()).Where<JsonProperty>((Func<JsonProperty, bool>) (p => !p.get_Ignored())).Where<JsonProperty>((Func<JsonProperty, bool>) (p =>
      {
        if (this._ignoreObsoleteProperties)
          return !p.IsObsolete();
        return true;
      })).ToDictionary<JsonProperty, string, Schema>((Func<JsonProperty, string>) (prop => prop.get_PropertyName()), (Func<JsonProperty, Schema>) (prop => this.CreateInlineSchema(prop.get_PropertyType()).WithValidationProperties(prop)));
      List<string> list = ((IEnumerable<JsonProperty>) jsonContract.get_Properties()).Where<JsonProperty>((Func<JsonProperty, bool>) (prop => prop.IsRequired())).Select<JsonProperty, string>((Func<JsonProperty, string>) (propInfo => propInfo.get_PropertyName())).ToList<string>();
      return new Schema()
      {
        required = list.Any<string>() ? (IList<string>) list : (IList<string>) null,
        properties = (IDictionary<string, Schema>) dictionary,
        type = "object"
      };
    }

    private Schema CreateRefSchema(Type type)
    {
      if (!this._workItems.ContainsKey(type))
      {
        string schemaId = this._schemaIdSelector(type);
        if (this._workItems.Any<KeyValuePair<Type, SchemaRegistry.WorkItem>>((Func<KeyValuePair<Type, SchemaRegistry.WorkItem>, bool>) (entry => entry.Value.SchemaId == schemaId)))
        {
          Type key = this._workItems.First<KeyValuePair<Type, SchemaRegistry.WorkItem>>((Func<KeyValuePair<Type, SchemaRegistry.WorkItem>, bool>) (entry => entry.Value.SchemaId == schemaId)).Key;
          throw new InvalidOperationException(string.Format("Conflicting schemaIds: Duplicate schemaIds detected for types {0} and {1}. See the config setting - \"UseFullTypeNameInSchemaIds\" for a potential workaround", (object) type.FullName, (object) key.FullName));
        }
        this._workItems.Add(type, new SchemaRegistry.WorkItem()
        {
          SchemaId = schemaId
        });
      }
      return new Schema()
      {
        @ref = "#/definitions/" + this._workItems[type].SchemaId
      };
    }

    private Schema FilterSchema(Schema schema, JsonContract jsonContract)
    {
      if (schema.type == "object" || this._applyFiltersToAllSchemas)
      {
        JsonObjectContract jsonObjectContract = jsonContract as JsonObjectContract;
        if (jsonObjectContract != null)
        {
          ModelFilterContext context = new ModelFilterContext(((JsonContract) jsonObjectContract).get_UnderlyingType(), jsonObjectContract, this);
          foreach (IModelFilter modelFilter in this._modelFilters)
            modelFilter.Apply(schema, context);
        }
        foreach (ISchemaFilter schemaFilter in this._schemaFilters)
          schemaFilter.Apply(schema, this, jsonContract.get_UnderlyingType());
      }
      return schema;
    }

    private class WorkItem
    {
      public string SchemaId;
      public bool InProgress;
      public Schema Schema;
    }
  }
}
