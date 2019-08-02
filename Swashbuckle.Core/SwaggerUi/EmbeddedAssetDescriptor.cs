// Decompiled with JetBrains decompiler
// Type: Swashbuckle.SwaggerUi.EmbeddedAssetDescriptor
// Assembly: Swashbuckle.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cd1bb07a5ac7c7bc
// MVID: 1DF278A1-7B04-4008-A96D-B53CBD64DB3D
// Assembly location: C:\Users\Saurabh.Tripathi\Desktop\Swashbuckle.Core.dll

using System.Reflection;

namespace Swashbuckle.SwaggerUi
{
  public class EmbeddedAssetDescriptor
  {
    public EmbeddedAssetDescriptor(Assembly containingAssembly, string name, bool isTemplate)
    {
      this.Assembly = containingAssembly;
      this.Name = name;
      this.IsTemplate = isTemplate;
    }

    public Assembly Assembly { get; private set; }

    public string Name { get; private set; }

    public bool IsTemplate { get; private set; }
  }
}
