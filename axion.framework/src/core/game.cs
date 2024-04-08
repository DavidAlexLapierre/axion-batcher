using Axion.Content;
using Axion.Graphics;
using OpenTK.Mathematics;

namespace Axion;

abstract class Game : IDisposable {
    /// <summary>
    /// Manager responsible for handling the window and its settings
    /// </summary>
    public abstract IDisplayManager DisplayManager { get; protected set; }
    /// <summary>
    /// Scene manager used by the game
    /// </summary>
    public SceneManager SceneManager { get; private set; }
    /// <summary>
    /// Event manager for the game
    /// </summary>
    public EventManager EventManager { get; private set; }
    /// <summary>
    /// Resource manager for the game
    /// </summary>
    public ResourceManager Resources { get; private set; }
    /// <summary>
    /// Reference to the graphics card and its rendering api
    /// </summary>
    public GraphicsDevice GraphicsDevice { get; private set; }
    /// <summary>
    /// Renderer for the game
    /// </summary>
    public Renderer Renderer { get; private set; }

    /// <summary>
    /// Initialize the game's core components
    /// </summary>
    public virtual void Init(IGameProvider provider) {
        SceneManager = new SceneManager();
        EventManager = new EventManager();
        Resources = new ResourceManager(provider.CreateTextureBuilder());
        GraphicsDevice = provider.CreateGraphicsDevice();
        Renderer = new Renderer();
        GraphicsDevice.Init();
        Renderer.Init();
    }

    /// <summary>
    /// Run the game loop
    /// </summary>
    public abstract void Run();

    /// <summary>
    /// Update loop for the game
    /// </summary>
    /// <param name="deltaT">Tiem between each frame in seconds</param>
    protected void Update(double deltaT) {
        SceneManager.Update(deltaT);
    }

    /// <summary>
    /// Draw loop for the game
    /// </summary>
    protected void Draw() {
        Renderer.Begin();
        SceneManager.Draw();
        Renderer.End(SceneManager.GetWorldMatrix());
    }

    public virtual void Dispose() {
    }
}