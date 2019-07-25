// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.SchemaExtensions
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Swashbuckle.Swagger
{
  public static class SchemaExtensions
  {
    public static Schema WithValidationProperties(
      this Schema schema,
      JsonProperty jsonProperty)
    {
      PropertyInfo propertyInfo = jsonProperty.PropertyInfo();
      if (propertyInfo == (PropertyInfo) null)
        return schema;
      foreach (object customAttribute in propertyInfo.GetCustomAttributes(false))
      {
        RegularExpressionAttribute expressionAttribute = customAttribute as RegularExpressionAttribute;
        if (expressionAttribute != null)
          schema.pattern = expressionAttribute.Pattern;
        RangeAttribute rangeAttribute = customAttribute as RangeAttribute;
        if (rangeAttribute != null)
        {
          int result1;
          if (int.TryParse(rangeAttribute.Maximum.ToString(), out result1))
            schema.maximum = new int?(result1);
          int result2;
          if (int.TryParse(rangeAttribute.Minimum.ToString(), out result2))
            schema.minimum = new int?(result2);
        }
        StringLengthAttribute stringLengthAttribute = customAttribute as StringLengthAttribute;
        if (stringLengthAttribute != null)
        {
          schema.maxLength = new int?(stringLengthAttribute.MaximumLength);
          schema.minLength = new int?(stringLengthAttribute.MinimumLength);
        }
      }
      if (!jsonProperty.get_Writable())
        schema.readOnly = new bool?(true);
      return schema;
    }

    public static void PopulateFrom(this PartialSchema partialSchema, Schema schema)
    {
      if (schema == null)
        return;
      partialSchema.type = schema.type;
      partialSchema.format = schema.format;
      partialSchema.vendorExtensions = schema.vendorExtensions;
      if (schema.items != null)
      {
        partialSchema.items = new PartialSchema();
        partialSchema.items.PopulateFrom(schema.items);
      }
      partialSchema.@default = schema.@default;
      partialSchema.maximum = schema.maximum;
      partialSchema.exclusiveMaximum = schema.exclusiveMaximum;
      partialSchema.minimum = schema.minimum;
      partialSchema.exclusiveMinimum = schema.exclusiveMinimum;
      partialSchema.maxLength = schema.maxLength;
      partialSchema.minLength = schema.minLength;
      partialSchema.pattern = schema.pattern;
      partialSchema.maxItems = schema.maxItems;
      partialSchema.minItems = schema.minItems;
      partialSchema.uniqueItems = schema.uniqueItems;
      partialSchema.@enum = schema.@enum;
      partialSchema.multipleOf = schema.multipleOf;
    }
  }
}
