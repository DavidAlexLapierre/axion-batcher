using Axion.Content;
using Axion.Data;
using Axion.Graphics;
using OpenTK.Mathematics;

namespace Axion.Components;

public class Sprite : Component {
    /// <summary>
    /// List of the different frames of the sprite
    /// </summary>
    public List<Rectangle> Frames { get; private set; }
    /// <summary>
    /// Texture used by the sprite
    /// </summary>
    public Texture Texture { get; private set; }
    /// <summary>
    /// Origin of the sprite
    /// </summary>
    public Vector2 Origin { get; private set; }
    /// <summary>
    /// Animation speed. Set to 0 by default
    /// </summary>
    public float AnimationSpeed { get; private set; }
    /// <summary>
    /// Image index of the sprite
    /// </summary>
    public int ImageIndex { get; private set; }
    /// <summary>
    /// Color to give to the sprite. White by default
    /// </summary>
    public Color Color { get; private set; }
    /// <summary>
    /// Shader to use
    /// </summary>
    public string Shader { get; private set; }
    /// <summary>
    /// Width of the sprite
    /// </summary>
    public int Width => Frames is not null ? Frames[0].Width : 0;
    /// <summary>
    /// Height of the sprite
    /// </summary>
    public int Height => Frames is not null ? Frames[0].Height : 0;

    /// <summary>
    /// Default constructor of a sprite
    /// </summary>
    /// <param name="frames">Animation frames of the sprite</param>
    /// <param name="texture">Texture to use</param>
    /// <param name="origin">Origin within the sprite</param>
    /// <param name="animationSpeed">Speed of the animation. Set to 0 by default</param>
    internal Sprite(List<FrameData> frames, Texture texture, Vector2 origin, float animationSpeed) {
        Frames = GetFrames(frames);
        Texture = texture;
        Origin = origin;
        AnimationSpeed = animationSpeed;
        ImageIndex = 0;
        Color = new Color();
        Shader = string.Empty;
    }

    /// <summary>
    /// Get the list of frames used by the sprite
    /// </summary>
    /// <returns>List of frames represented as a rectangle</returns>
    List<Rectangle> GetFrames(List<FrameData> frameData) {
        var frames = new List<Rectangle>();
        if (frameData is not null) {
            foreach (var frame in frameData) {
                frames.Add(new Rectangle(frame.X, frame.Y, frame.Width, frame.Height));
            }
        }

        return frames;
    }

    public override void Draw() {
        Axn.DrawSprite(Texture, Parent.Position, Origin, Frames[ImageIndex], Color, Shader);
    }

    /// <summary>
    /// Clone a sprite. This is used when loading the sprite and trying to create a copy
    /// </summary>
    /// <returns></returns>
    internal Sprite Clone() {
        var rand = new Random();
        var newColor = new Color((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
        var sprite = new Sprite(null, Texture, Origin, AnimationSpeed) {
            Frames = Frames,
            Color = newColor,
            Shader = Shader
        };
        return sprite;
    }
}