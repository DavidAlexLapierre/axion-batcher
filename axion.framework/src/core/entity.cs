using OpenTK.Mathematics;

namespace Axion;

public abstract class Entity : Transform, IDisposable {
    /// <summary>
    /// List of the different children of this entity
    /// </summary>
    public List<Entity> Children { get; protected set; }
    /// <summary>
    /// Parent of the entity. This value can be null
    /// </summary>
    protected Entity Parent { get; private set; }
    /// <summary>
    /// Dictionary of the different components supported by the entity
    /// </summary>
    Dictionary<Type, Component> components;
    /// <summary>
    /// List of components to remove after the update frame
    /// </summary>
    List<Type> componentsToRemove;

    public readonly Guid Id;
    public Entity() {
        Id = Guid.NewGuid();
        components = new Dictionary<Type, Component>();
        componentsToRemove = new List<Type>();
        Children = new List<Entity>();
    }

    /// <summary>
    /// Add a child to this entity. Works in a similar fashion
    /// as Axn.CreateEntity(int x, int y, Entity child) but instead,
    /// it adds a parent/child relation to the entity.
    /// </summary>
    /// <param name="child">Child entity</param>
    public void AddChild(int x, int y, Entity child) {
        Children.Add(child);
        child.SetParent(this);
        Axn.CreateEntity(x, y, child);
    }

    /// <summary>
    /// Remove a child from the entity
    /// </summary>
    /// <param name="id"></param>
    public void RemoveChild(Guid id) {
        var child = Children.Find(c => c.Id == id);
        if (child is not null) {
            Children.Remove(child);
            Axn.DisposeEntity(id);
        }
    }

    /// <summary>
    /// Set the parent of the entity
    /// </summary>
    /// <param name="parent">Parent entity</param>
    internal void SetParent(Entity parent) => Parent = parent;

    /// <summary>
    /// Add a component to the entity
    /// </summary>
    /// <typeparam name="T">Type of the component</typeparam>
    /// <param name="component">Component to add</param>
    public void AddComponent<T>(T component) where T : Component {
        var key = typeof(T);
        if (!components.ContainsKey(key)) {
            component.SetParent(this);
            components.Add(key, component);
        }
    }

    /// <summary>
    /// Get a component
    /// </summary>
    /// <typeparam name="T">Type of the component to retrieve</typeparam>
    /// <returns>Component or null</returns>
    public T GetComponent<T>() where T : Component {
        var key = typeof(T);
        if (components.ContainsKey(key)) {
            return components[key] as T;
        }

        return null;
    }

    /// <summary>
    /// Remove a component from the entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RemoveComponent<T>() where T : Component {
        var key = typeof(T);
        if (components.ContainsKey(key)) {
            componentsToRemove.Add(key);
        }
    }

    /// <summary>
    /// Initialize the entity and its components. this function is called after the
    /// constructor.
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// Update the different components
    /// </summary>
    /// <param name="deltaT">Time between each frame in seconds</param>
    public virtual void Update(double deltaT) {
        foreach (var component in components.Values) {
            component.Update(deltaT);
        }
        RemoveComponents();
    }

    /// <summary>
    /// Draw the different components
    /// </summary>
    public virtual void Draw() {
        foreach (var component in components.Values) {
            component.Draw();
        }
    }

    /// <summary>
    /// Remove the components from the list of components after
    /// the update call
    /// </summary>
    private void RemoveComponents() {
        foreach (var key in componentsToRemove) {
            if (components.ContainsKey(key)) {
                components.Remove(key);
            }
        }
        componentsToRemove.Clear();
    }

    /// <summary>
    /// Kills the entity
    /// </summary>
    public virtual void Dispose() {
        foreach (var child in Children) {
            Axn.DisposeEntity(child.Id);
        }
    }
}