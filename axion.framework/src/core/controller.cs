namespace Axion;

public abstract class Controller : IDisposable {
    /// <summary>
    /// Id for the controller
    /// </summary>
    public Guid Id { get; private set; }
    public Controller() {
        Id = Guid.NewGuid();
    }

    /// <summary>
    /// Update call for the controller
    /// </summary>
    /// <param name="deltaT">Time between each frame</param>
    public abstract void Update(double deltaT);

    /// <summary>
    /// Initialization function of the controller
    /// </summary>
    public virtual void Init() {}

    /// <summary>
    /// Disposal fuinction of the controller
    /// </summary>
    public virtual void Dispose() {}
}