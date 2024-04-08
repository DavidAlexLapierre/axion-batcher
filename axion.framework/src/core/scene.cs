using Axion.Cameras;

namespace Axion;

/// <summary>
/// Scene object used by Axion
/// </summary>
public abstract class Scene : IDisposable {
    /// <summary>
    /// Camera used by the scene
    /// </summary>
    internal Camera Camera { get; private set; }
    /// <summary>
    /// Map of entities currently listed in the scene
    /// </summary>
    Dictionary<Guid, Entity> entities;
    /// <summary>
    /// Different game controllers used by the scene. Only one controller type can be registered at a time
    /// </summary>
    Dictionary<Guid, Controller> controllers;
    /// <summary>
    /// List of entities that will be created at the end of the frame
    /// </summary>
    List<Entity> toAddEntities;
    /// <summary>
    /// List of entities that will be deleted at the end of the frame
    /// </summary>
    List<Guid> toDisposeEntities;
    /// <summary>
    /// List of controllers to add
    /// </summary>
    List<Controller> toAddControllers;
    /// <summary>
    /// List of controllers to remove
    /// </summary>
    List<Guid> toDisposeControllers;

    public Scene() {
        entities = new Dictionary<Guid, Entity>();
        controllers = new Dictionary<Guid, Controller>();
        toAddEntities = new List<Entity>();
        toDisposeEntities = new List<Guid>();
        toAddControllers = new List<Controller>();
        toDisposeControllers = new List<Guid>();
    }

    /// <summary>
    /// Called at the initialization of the scene. It is used to set up
    /// its entities and logic.
    /// </summary>
    public abstract void Init();
    /// <summary>
    /// Dispose function for the scene. This function is called when the current
    /// scene not being used anymore.
    /// </summary>
    public virtual void Dispose() {
        if (Camera is not null) Camera.Dispose();
    }

    /// <summary>
    /// Update function of a scene. Controllers are updated first, then entities
    /// </summary>
    /// <param name="deltaT">Time between each frame in seconds</param>
    internal void Update(double deltaT) {
        foreach (var controller in controllers.Values) {
            controller.Update(deltaT);
        }
        foreach (var entity in entities.Values) {
            entity.Update(deltaT);
        }
        Merge();
    }

    /// <summary>
    /// Draw function for a scene
    /// </summary>
    internal void Draw() {
        foreach (var entity in entities.Values) {
            entity.Draw();
        }
    }

    /// <summary>
    /// Set the camera used by the scene
    /// </summary>
    /// <param name="camera">Camera that will be used</param>
    internal void SetCamera(Camera camera) => Camera = camera;
    /// <summary>
    /// Add an entity to the list of entities to be created
    /// </summary>
    /// <param name="entity">Entity to add</param>
    internal void AddEntity(Entity entity) => toAddEntities.Add(entity);
    /// <summary>
    /// Add an entity to the list of entities to delete
    /// </summary>
    /// <param name="id"></param>
    internal void DisposeEntity(Guid id) => toDisposeEntities.Add(id);

    /// <summary>
    /// Add a controller
    /// </summary>
    /// <param name="controller">Controller object to add</param>
    internal void AddController(Controller controller) => toAddControllers.Add(controller);

    /// <summary>
    /// Remove a controller
    /// </summary>
    /// <typeparam name="T">Controller to remove</typeparam>
    internal void DisposeController(Guid id) => toDisposeControllers.Add(id);

    /// <summary>
    /// Merged the entities that were added to the creation and deletion list
    /// </summary>
    void Merge() {
        // Add entities
        foreach (var entity in toAddEntities) {
            if (!entities.ContainsKey(entity.Id)) {
                entities.Add(entity.Id, entity);
                entities[entity.Id].Init();
            }
        }
        // Remove entities
        foreach (var id in toDisposeEntities) {
            if (entities.ContainsKey(id)) {
                entities[id].Dispose();
                entities.Remove(id);
            }
        }
        // Add controllers
        foreach (var controller in toAddControllers) {
            if (!controllers.ContainsKey(controller.Id)) {
                controllers.Add(controller.Id, controller);
                controller.Init();
            }
        }
        // Remove controllers
        foreach (var id in toDisposeControllers) {
            if (controllers.ContainsKey(id)) {
                controllers[id].Dispose();
                controllers.Remove(id);
            }
        }
        toDisposeEntities.Clear();
        toAddEntities.Clear();
        toDisposeControllers.Clear();
        toAddControllers.Clear();
    }
}