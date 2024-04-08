using Axion.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Axion.Desktop;

/// <summary>
/// Class containing the logic for a desktop game. It handles the application
/// life cycle.
/// </summary>
class DesktopGame : Game {

#if DEBUG
    double elapsedTime;
    int frameCount;
    double averageFPS;
#endif

    /// <summary>
    /// Reference to the game Window
    /// </summary>
    public GameWindow Window { get; private set; }
    public override IDisplayManager DisplayManager { get; protected set; }

    /// <summary>
    /// Run the game loop
    /// </summary>
    public override void Run() => Window.Run();

    public override void Init(IGameProvider provider) {
        Window = new GameWindow(GameWindowSettings.Default, NativeWindowSettings.Default);
        DisplayManager = new DisplayManager();
        base.Init(provider);
        Window.UpdateFrame += OnBeforeUpdateFrame;
        Window.RenderFrame += OnBeforeDrawFrame;
    }



    /// <summary>
    /// Function called before the update frame
    /// </summary>
    /// <param name="args"></param>
    void OnBeforeUpdateFrame(FrameEventArgs args) {
        Update(args.Time);
#if DEBUG
        if (elapsedTime >= 0.5) {
            Window.Title = ((int)(averageFPS / (float)frameCount)).ToString();
            elapsedTime = 0;
            frameCount = 0;
            averageFPS = 0;
        }

        averageFPS += 1 / args.Time;
        ++frameCount;
        elapsedTime += args.Time;
#endif
    }

    /// <summary>
    /// Function called before the draw frame
    /// </summary>
    /// <param name="args"></param>
    void OnBeforeDrawFrame(FrameEventArgs args) {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        Draw();
        Window.SwapBuffers();
    }

    /// <summary>
    /// Called before the game closes
    /// </summary>
    public override void Dispose() {
        Window.Dispose();
    }
}