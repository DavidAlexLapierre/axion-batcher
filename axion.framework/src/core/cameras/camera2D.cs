using OpenTK.Mathematics;

namespace Axion.Cameras;

public class Camera2D : Camera {
    /// <summary>
    /// View width of the camera
    /// </summary>
    public int ViewWidth { get; private set; }
    /// <summary>
    /// View height of the camera
    /// </summary>
    public int ViewHeight { get; private set; }
    Entity target;
    Vector2 offset;

    /// <summary>
    /// X coordinate of the camera
    /// </summary>
    public int ViewX {
        get {
            var tarPos = target is not null ? target.Position : Vector3.Zero;
            return (int)(-tarPos.X - X - offset.X);
        }
    }

    /// <summary>
    /// Y coordinate of the camera
    /// </summary>
    public int ViewY {
        get {
            var tarPos = target is not null ? target.Position : Vector3.Zero;
            return (int)(-tarPos.Y - Y - offset.Y);
        }
    }

    /// <summary>
    /// Get the world matrix
    /// </summary>
    public override Matrix4 ViewMat {
        get {
            var tarPos = target is not null ? target.Position : Vector3.Zero;
            return Matrix4.LookAt(Position, tarPos, Vector3.UnitY);
        }
    }

    public override Matrix4 ProjectionMat {
        get {
            return Matrix4.CreateOrthographicOffCenter(ViewX, ViewX + ViewWidth, ViewY + ViewHeight, ViewY, nearView, viewDistance);
        }
    }

    /// <summary>
    /// Constructor for the 2D Camera
    /// </summary>
    /// <param name="viewWidth">View width</param>
    /// <param name="viewHeight">View height</param>
    public Camera2D(int viewWidth, int viewHeight) : base(int.MaxValue) {
        SetPosition(0, 0, 10);
        ViewWidth = viewWidth;
        ViewHeight = viewHeight;
        var scaleX = Axn.GetWindowSize().X / (float)viewWidth;
        var scaleY = Axn.GetWindowSize().Y / (float)viewHeight;
        SetScale(scaleX, scaleY);
    }

    /// <summary>
    /// Set the target of the camera to focus on
    /// </summary>
    /// <param name="target">Target entity</param>
    public void SetTarget(Entity target) => this.target = target;
    /// <summary>
    /// Set the offset of the camera. This is useful if you want the camera
    /// to focus an entity without having the entity straight in the middle.
    /// </summary>
    /// <param name="x">X offset</param>
    /// <param name="y">Y offset</param>
    public void SetOffset(float xOffset, float yOffset) => offset = new Vector2(xOffset, yOffset);
}