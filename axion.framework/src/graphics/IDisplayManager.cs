using OpenTK.Mathematics;

namespace Axion.Graphics;

internal interface IDisplayManager {
    Vector2 GetWindowSize();
    void SetWindowResize(bool canResize);
    void ToggleFullscreen();
}