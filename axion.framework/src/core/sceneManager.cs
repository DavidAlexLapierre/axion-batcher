using Axion.Cameras;
using OpenTK.Mathematics;

namespace Axion;

class SceneManager {
    /// <summary>
    /// Current scene used by the framework
    /// </summary>
    Type currentScene;
    /// <summary>
    /// Map of the different scenes supported by the game
    /// </summary>
    Dictionary<Type, Scene> scenes;

    public SceneManager() {
        scenes = new Dictionary<Type, Scene>();
    }

    /// <summary>
    /// Update the current scene
    /// </summary>
    /// <param name="deltaT">Time between frames in seconds</param>
    public void Update(double deltaT) {
        if (currentScene is not null) {
            scenes[currentScene].Update(deltaT);
        }
    }

    /// <summary>
    /// Draw the current scene
    /// </summary>
    public void Draw() {
        if (currentScene is not null) {
            scenes[currentScene].Draw();
        }
    }

    /// <summary>
    /// Get the world matrix used by the current scene
    /// </summary>
    /// <returns></returns>
    public Matrix4 GetWorldMatrix() {
        if (currentScene is not null) {
            var camera = scenes[currentScene].Camera;
            return camera is not null ? camera.ViewMat * camera.ProjectionMat : Matrix4.Identity;
        }

        return Matrix4.Identity;
    }

    /// <summary>
    /// Get the current scene used by the scene manager
    /// </summary>
    /// <returns>Current scene type</returns>
    internal Type GetCurrentScene() { return currentScene; }

    /// <summary>
    /// Set the camera for the current scene
    /// </summary>
    /// <param name="camera">Camera to use</param>
    public void Setcamera(Camera camera) {
        if (currentScene is not null) {
            scenes[currentScene].SetCamera(camera);
        }
    }


    /// <summary>
    /// Register a scene for the scene registry
    /// </summary>
    /// <typeparam name="T">Type of the scene</typeparam>
    /// <param name="scene">Scene to add</param>
    public void RegisterScene<T>(T scene) where T : Scene {
        if (!scenes.ContainsKey(typeof(T))) {
            scenes.Add(typeof(T), scene);
            if (currentScene is null) { SetScene<T>(); }
        }
    }

    /// <summary>
    /// Set the current scene used by the framework
    /// </summary>
    /// <typeparam name="T">Scene to switch to</typeparam>
    public void SetScene<T>() where T : Scene {
        if (scenes.ContainsKey(typeof(T))) {
            if (currentScene is not null) scenes[currentScene].Dispose();
            currentScene = typeof(T);
            scenes[currentScene].Init();
        }
    }

    /// <summary>
    /// Set the current scene used by the framework
    /// </summary>
    /// <param name="scene">Scene to switch to</param>
    public void SetScene(Type scene) {
        if (scenes.ContainsKey(scene)) {
            if (currentScene is not null) scenes[currentScene].Dispose();
            currentScene = scene;
            scenes[currentScene].Init();
        }
    }

    /// <summary>
    /// Create an entity within the current scene
    /// </summary>
    /// <param name="entity">Entity to add</param>
    public void CreateEntity(Entity entity) { if (currentScene is not null) scenes[currentScene].AddEntity(entity); }

    /// <summary>
    /// Remove an entity from the current scene
    /// </summary>
    /// <param name="id"></param>
    public void DisposeEntity(Guid id) { if (currentScene is not null) scenes[currentScene].DisposeEntity(id); }

    /// <summary>
    /// Add a controller to the current scene. Only one controller of each type per scene
    /// </summary>/p
    /// <param name="controller">Controller to add<aram>
    public void AddController(Controller controller) { if (currentScene is not null) scenes[currentScene].AddController(controller); }

    /// <summary>
    /// Remove a controller from the current scene
    /// </summary>
    /// <typeparam name="T">Controller to remove</typeparam>
    public void DisposeController(Guid id) { if (currentScene is not null) scenes[currentScene].DisposeController(id); }
}