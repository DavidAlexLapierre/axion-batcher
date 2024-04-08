using System.Text.Json;

namespace Axion.Content.Loaders;

class SpriteLoader : LoaderBase {
    AtlasManager atlasManager;
    internal SpriteLoader(ITextureBuilder builder) : base() {
        atlasManager = new AtlasManager(builder);
    }

    public override T Get<T>(string asset) {
        return atlasManager.GetSpriteData(asset) as T;
    }

    public override void Load<T>(string assetPath) {
        try {
            string json;
            
            using (StreamReader reader = File.OpenText(assetPath)) {
                json = reader.ReadToEnd();
            }

            var content = JsonSerializer.Deserialize<JsonSpriteData>(json);
            if (content.Origin is null) content.Origin = new OriginData{ X = 0, Y = 0 };
            atlasManager.Load(content);
            
        } catch (Exception) {
            Console.WriteLine("Failed to load asset at location " + assetPath);
        }
    }
}