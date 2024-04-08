using OpenTK.Mathematics;

namespace Axion.Cameras;

public abstract class Camera : Transform, IDisposable {
    /// <summary>
    /// Closest point that the camera will render
    /// </summary>
    protected float nearView;
    /// <summary>
    /// Furthest distance that the camera will render
    /// </summary>
    protected float viewDistance;
    /// <summary>
    /// View matrix
    /// </summary>
    public abstract Matrix4 ViewMat { get; }
    /// <summary>
    /// Projection matrix
    /// </summary>
    public abstract Matrix4 ProjectionMat { get; }

    public Camera(float _viewDistance) {
        nearView = 0.1f;
        viewDistance = _viewDistance;
        Axn.Events.AddEventListener(Events.WindowResize, HandleWindowResize);
    }

    /// <summary>
    /// Camera update function
    /// </summary>
    /// <param name="deltaT">Timebetween each frames</param>
    public virtual void Update(double deltaT) {}

    public virtual void HandleWindowResize(object sender, EventArgs args) {}

    public void Dispose() {
        Axn.Events.RemoveEventListener(Events.WindowResize, HandleWindowResize);
    }
}