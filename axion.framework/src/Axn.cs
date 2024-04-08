using Axion.Cameras;
using Axion.Data;
using Axion.Content;
using OpenTK.Mathematics;
using Axion.Graphics;

namespace Axion;

/// <summary>
/// Axion API
/// </summary>
public class Axn {

    #region Game

    /// <summary>
    /// Reference to the game object that was created
    /// </summary>
    internal static Game Game;

    /// <summary>
    /// Event handler for the game
    /// </summary>
    public static EventManager Events { get { return Game.EventManager; }}

    /// <summary>
    /// Initialize the the game.
    /// </summary>
    /// <param name="gameTitle">Title of the game</param>
    public static void CreateGame(IGameProvider provider) {
        Game = provider.CreateInstance();
        Game.Init(provider);
    }

    /// <summary>
    /// Get a reference to the game object
    /// </summary>
    /// <typeparam name="T">Type of game to handle</typeparam>
    /// <returns></returns>
    internal static T GetGame<T>() where T : Game { return Game as T; }

    /// <summary>
    /// Start the game loop
    /// </summary>
    public static void RunGame() {
        if (Game is not null) {
            Game.Run();
        } else {
            Console.WriteLine("Game has not been initialized. Cannot run the game");
        }
    }

    #endregion
    
    #region Resources

    /// <summary>
    /// Load an asset to memory
    /// </summary>
    /// <typeparam name="T">Type of asset to load</typeparam>
    /// <param name="assetName">Name of the asset</param>
    public static void Load<T>(string assetPath) => Game.Resources.Load<T>(assetPath);
    /// <summary>
    /// Load an asset directory
    /// </summary>
    /// <typeparam name="T">Type of asset to load</typeparam>
    /// <param name="asset">Asset to load</param>
    public static void Load<T>(T asset) => Game.Resources.Load<T>(asset);
    /// <summary>
    /// Retrieve an asset from the content
    /// </summary>
    /// <typeparam name="T">Type of asset to retrieve</typeparam>
    /// <param name="asset">Name of the asset to retrieve</param>
    /// <returns></returns>
    public static T Get<T>(string asset) where T : class => Game.Resources.Get<T>(asset);
    /// <summary>
    /// Register a new data loader
    /// </summary>
    /// <typeparam name="T">Type of data to load</typeparam>
    /// <param name="loader">The loader class handling the load and get method</param>
    public static void RegisterLoader<T>(LoaderBase loader) => Game.Resources.RegisterLoader<T>(loader);

    #endregion

    #region SceneManager

    /// <summary>
    /// Get th4e current scene used by the SceneManager
    /// </summary>
    /// <returns>Type of the current scene</returns>
    internal static Type GetCurrentScene() => Game.SceneManager.GetCurrentScene();

    /// <summary>
    /// Register a scene for the gmae
    /// </summary>
    /// <typeparam name="T">Type of scene to register. Only one scene per type can be registered</typeparam>
    /// <param name="scene">Scene to register</param>
    public static void RegisterScene<T>(T scene) where T : Scene => Game.SceneManager.RegisterScene(scene);

    /// <summary>
    /// Set the scene to use by the scene manager
    /// </summary>
    /// <typeparam name="T">Type of scene to use</typeparam>
    public static void SetScene<T>() where T : Scene => Game.SceneManager.SetScene<T>();

    /// <summary>
    /// Set the scene to use by the scene manager
    /// </summary>
    /// <param name="scene">Type of scene to use</param>
    public static void SetScene(Type scene) => Game.SceneManager.SetScene(scene);

    /// <summary>
    /// Create an entity in the current scene
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="entity">Entity to add</param>
    public static void CreateEntity(int x, int y, Entity entity) {
        entity.SetPosition(x, y);
        Game.SceneManager.CreateEntity(entity);
    }

    /// <summary>
    /// Destroy an entity within the current scene
    /// </summary>
    /// <param name="id">ID of the entity to destroy</param>
    public static void DisposeEntity(Guid id) => Game.SceneManager.DisposeEntity(id);

    /// <summary>
    /// Add a controller to the current scene. Only one controller of each type per scene
    /// </summary>/p
    /// <param name="controller">Controller to add<aram>
    public static void AddController(Controller controller) => Game.SceneManager.AddController(controller);

    /// <summary>
    /// Remove a controller from the current scene
    /// </summary>
    /// <typeparam name="T">Controller to remove</typeparam>
    public static void RemoveController(Guid id) => Game.SceneManager.DisposeController(id);

    /// <summary>
    /// Set the camera for the current scene
    /// </summary>
    /// <param name="camera">Camera to use</param>
    public static void SetCamera(Camera camera) => Game.SceneManager.Setcamera(camera);

    #endregion

    #region DisplayManager

    /// <summary>
    /// Set whether the window can be resized or now
    /// </summary>
    /// <param name="canResize"></param>
    public static void SetWindowResize(bool canResize) => Game.DisplayManager.SetWindowResize(canResize);

    /// <summary>
    /// Toggle fullscreen mode for the game
    /// </summary>
    public static void ToggleFullScreen() => Game.DisplayManager.ToggleFullscreen();

    public static Vector2 GetWindowSize() => Game.DisplayManager.GetWindowSize();

    #endregion

    #region Renderer

    public static void DrawSprite(Texture texture, Vector3 position, Vector2 origin, Rectangle frame, Color color, string shader) => Game.Renderer.DrawSprite(texture, position, origin, frame, color, shader);

    #endregion

}