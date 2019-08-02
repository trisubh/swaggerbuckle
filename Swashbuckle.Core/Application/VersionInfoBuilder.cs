// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Application.VersionInfoBuilder
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swashbuckle.Application
{
  public class VersionInfoBuilder
  {
    private readonly Dictionary<string, InfoBuilder> _versionInfos;

    public VersionInfoBuilder()
    {
      this._versionInfos = new Dictionary<string, InfoBuilder>();
    }

    public InfoBuilder Version(string version, string title)
    {
      InfoBuilder infoBuilder = new InfoBuilder(version, title);
      this._versionInfos[version] = infoBuilder;
      return infoBuilder;
    }

    public IDictionary<string, Info> Build()
    {
      return (IDictionary<string, Info>) this._versionInfos.ToDictionary<KeyValuePair<string, InfoBuilder>, string, Info>((Func<KeyValuePair<string, InfoBuilder>, string>) (entry => entry.Key), (Func<KeyValuePair<string, InfoBuilder>, Info>) (entry => entry.Value.Build()));
    }
  }
}
