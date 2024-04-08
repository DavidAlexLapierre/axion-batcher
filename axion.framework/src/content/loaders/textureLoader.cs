using Axion.Content;
using Axion.Graphics;
using StbImageSharp;

class TextureLoader : LoaderBase {
    /// <summary>
    /// Not sure if we need to keep this? Maybe its not worth caching the textures
    /// since they get duplicated and loaded in the atlas. It's a tradeoff between
    /// maybe reloading the same texture twice or keeping the same texture twice in
    /// memory.
    /// </summary>
    Dictionary<string, Texture> textures;

    public TextureLoader() : base() {
        textures = new Dictionary<string, Texture>();
    }

    public override T Get<T>(string asset) {
        if (textures.ContainsKey(asset)) {
            return textures[asset] as T;
        } else {
            try {
                Load<T>(asset);
                return textures[asset] as T;
            } catch (Exception) {
                return default;
            }
        }
    }

    public override void Load<T>(string assetPath) {
        if (!textures.ContainsKey(assetPath)) {
            try {
                using (var stream = File.OpenRead(assetPath)) {
                    var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                    var texture = new Texture(image);
                    textures.Add(assetPath, texture);
                }
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}