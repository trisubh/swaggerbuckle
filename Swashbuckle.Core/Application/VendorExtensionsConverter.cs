// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.VendorExtensionsConverter
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Swashbuckle.Application
{
  public class VendorExtensionsConverter : JsonConverter
  {
    public virtual bool CanConvert(Type objectType)
    {
      return objectType.GetField("vendorExtensions") != (FieldInfo) null;
    }

    public virtual object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }

    public virtual void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      JsonObjectContract jsonObjectContract = (JsonObjectContract) serializer.get_ContractResolver().ResolveContract(value.GetType());
      writer.WriteStartObject();
      using (IEnumerator<JsonProperty> enumerator = ((Collection<JsonProperty>) jsonObjectContract.get_Properties()).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          JsonProperty current = enumerator.Current;
          object obj = current.get_ValueProvider().GetValue(value);
          if (obj != null || serializer.get_NullValueHandling() != 1)
          {
            if (current.get_PropertyName() == "vendorExtensions")
            {
              IDictionary<string, object> source = (IDictionary<string, object>) obj;
              if (source.Any<KeyValuePair<string, object>>())
              {
                foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) source)
                {
                  writer.WritePropertyName(keyValuePair.Key);
                  serializer.Serialize(writer, keyValuePair.Value);
                }
              }
            }
            else
            {
              writer.WritePropertyName(current.get_PropertyName());
              serializer.Serialize(writer, obj);
            }
          }
        }
      }
      writer.WriteEndObject();
    }

    public VendorExtensionsConverter()
    {
      base.\u002Ector();
    }
  }
}
