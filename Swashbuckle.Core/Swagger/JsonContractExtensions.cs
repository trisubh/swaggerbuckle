// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.JsonContractExtensions
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Swashbuckle.Swagger
{
  public static class JsonContractExtensions
  {
    private static IEnumerable<string> AmbiguousTypeNames = (IEnumerable<string>) new string[4]
    {
      "System.Object",
      "System.Net.Http.HttpRequestMessage",
      "System.Net.Http.HttpResponseMessage",
      "System.Web.Http.IHttpActionResult"
    };

    public static bool IsSelfReferencing(this JsonDictionaryContract dictionaryContract)
    {
      return ((JsonContract) dictionaryContract).get_UnderlyingType() == dictionaryContract.get_DictionaryValueType();
    }

    public static bool IsSelfReferencing(this JsonArrayContract arrayContract)
    {
      return ((JsonContract) arrayContract).get_UnderlyingType() == arrayContract.get_CollectionItemType();
    }

    public static bool IsAmbiguous(this JsonObjectContract objectContract)
    {
      return JsonContractExtensions.AmbiguousTypeNames.Contains<string>(((JsonContract) objectContract).get_UnderlyingType().FullName);
    }
  }
}
