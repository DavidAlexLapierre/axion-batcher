using Axion.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Axion.Graphics;

/// <summary>
/// Manager class responsible fvor handling the game window
/// </summary>
class DisplayManager : IDisplayManager {

    /// <summary>
    /// Get the window object of the game
    /// </summary>
    GameWindow Window => Axn.GetGame<DesktopGame>().Window;
    
    /// <summary>
    /// Default constructor of the display manager
    /// </summary>
    public DisplayManager() {
        Window.WindowBorder = WindowBorder.Resizable;
        Window.Resize += HandleWindowResize;
    }

    /// <summary>
    /// Toggle between fullscreen and windowed
    /// </summary>
    public void ToggleFullscreen() {
        Window.WindowState = Window.IsFullscreen ?
            WindowState.Normal :
            WindowState.Fullscreen;
    }

    /// <summary>
    /// Helper function to allow users to resize the game window
    /// </summary>
    /// <param name="canResize">Whether or not users can resize the window</param>
    public void SetWindowResize(bool canResize) => Window.WindowBorder = canResize ? WindowBorder.Resizable : WindowBorder.Fixed;

    /// <summary>
    /// Handle the resizing of the window
    /// </summary>
    /// <param name="args"></param>
    void HandleWindowResize(ResizeEventArgs args) {
        GL.Viewport(0, 0, args.Width, args.Height);
    }

    public Vector2 GetWindowSize() { return new Vector2(Window.ClientSize[0], Window.ClientSize[1]); }
}