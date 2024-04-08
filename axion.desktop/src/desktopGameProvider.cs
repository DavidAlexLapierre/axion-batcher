using Axion.Graphics;

namespace Axion.Desktop;

public class DesktopGameProvider : IGameProvider {
    /// <summary>
    /// Create the instance of a desktop game
    /// </summary>
    /// <returns></returns>
    Game IGameProvider.CreateInstance() { return new DesktopGame(); }
    /// <summary>
    /// Create the instance of the graphics device
    /// </summary>
    /// <returns></returns>
    GraphicsDevice IGameProvider.CreateGraphicsDevice() { return new DesktopGraphicsDevice(); }

    /// <summary>
    /// Create a texture builder fo the opengl version of axion
    /// </summary>
    /// <returns></returns>
    ITextureBuilder IGameProvider.CreateTextureBuilder() { return new TextureBuilder(); }
}