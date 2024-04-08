using Axion.Graphics;

namespace Axion;

public interface IGameProvider {
    /// <summary>
    /// Create a platform specific instance of a game
    /// </summary>
    /// <returns>Game instance</returns>
    internal Game CreateInstance();
    /// <summary>
    /// Get the graphicsDevice used by the platform
    /// </summary>
    /// <returns></returns>
    internal GraphicsDevice CreateGraphicsDevice();

    /// <summary>
    /// Create the texture builder for the game
    /// </summary>
    /// <returns></returns>
    internal ITextureBuilder CreateTextureBuilder();
}