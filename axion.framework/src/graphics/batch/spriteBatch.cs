using Axion;
using Axion.Components;
using Axion.Data;
using Axion.Graphics;
using OpenTK.Mathematics;

class SpriteBatch {
    /// <summary>
    /// Different batchers that are used by shaders
    /// </summary>
    Dictionary<string, Batcher> batchers;

    /// <summary>
    /// Default constructor of a spritebatch
    /// </summary>
    public SpriteBatch() {
        batchers = new Dictionary<string, Batcher>();
    }

    public void Init() {
        batchers.Add(string.Empty, new Batcher(Axn.Get<Shader>("default")));
    }

    public void Begin() {
        // This is where we set up the rendering, rasterization, etc
    }

    /// <summary>
    /// Add a SpriteMesh to the batcher
    /// </summary>
    /// <param name="mesh">Mesh to render</param>
    public void Draw(Texture texture, Vector3 position, Vector2 origin, Rectangle frame, Color color, string shader) {
        var _shader = Axn.Get<Shader>(shader);
        // This is where we add the items to the different batches
        if (!batchers.ContainsKey(shader)) { batchers.Add(shader, new Batcher(_shader)); }
        batchers[shader].Batch(texture, position, origin, frame, color);
    }

    public void End(Matrix4 worldMat) {
        // This is where we send the data in batch to the graphics card
        foreach (var batcher in batchers.Values) {
            batcher.RenderBatch(worldMat);
        }
    }
}