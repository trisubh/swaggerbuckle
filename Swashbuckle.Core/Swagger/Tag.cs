﻿// Decompiled with JetBrains decompiler
// Type: Swashbuckle.Swagger.Tag
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System.Collections.Generic;

namespace Swashbuckle.Swagger
{
  public class Tag
  {
    public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    public string name;
    public string description;
    public ExternalDocs externalDocs;
  }
}