namespace Axion;

public abstract class Component {
    /// <summary>
    /// Parent entity of the component
    /// </summary>
    public Entity Parent { get; private set; }
    
    /// <summary>
    /// Update function of the component
    /// </summary>
    /// <param name="deltaT">Time between each frame in seconds</param>
    public virtual void Update(double deltaT) {}
    /// <summary>
    /// Draw function of the component
    /// </summary>
    public virtual void Draw() {}

    /// <summary>
    /// Used to set the parent of the component
    /// </summary>
    /// <param name="parent">Parent entity</param>
    internal void SetParent(Entity parent) => Parent = parent;
}