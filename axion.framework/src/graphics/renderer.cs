using Axion.Components;
using Axion.Data;
using OpenTK.Mathematics;

namespace Axion.Graphics;

/// <summary>
/// Manager class responsible for handling the rendering of objects
/// </summary>
class Renderer {

    /// <summary>
    /// Renderer responsible for rendering sprites
    /// </summary>
    SpriteBatch spriteBatch;

    public Renderer() {
        spriteBatch = new SpriteBatch();
    }

    /// <summary>
    /// Initialize the Renderer
    /// </summary>
    public void Init() {
        spriteBatch.Init();
    }

    public void Begin() => spriteBatch.Begin();

    public void DrawSprite(Texture texture, Vector3 position, Vector2 origin, Rectangle frame, Color color, string shader = "") => spriteBatch.Draw(texture, position, origin, frame, color, shader);

    public void End(Matrix4 worldMat) => spriteBatch.End(worldMat);

}