using Axion.Data;
using OpenTK.Mathematics;

namespace Axion.Graphics;

/// <summary>
/// Class defining a vertex for the rendering engine
/// </summary>
public struct Vertex {
    /// <summary>
    /// Position of the vertex
    /// </summary>
    public Vector3 Position { get; set; }
    /// <summary>
    /// Color of the vertex
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// Texture definition for the vertex
    /// </summary>
    internal static readonly VertexDefinition VertexDefinition;

    public Vertex() {
        Position = Vector3.Zero;
        Color = new Color();
    }

    /// <summary>
    /// Constructor of a vertex
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    public Vertex(float x, float y, float z) {
        Position = new Vector3(x, y, z);
        Color = new Color();
    }

    /// <summary>
    /// Constructor of a vertex
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    public Vertex(int x, int y, int z, Color color) {
        Position = new Vector3(x, y, z);
        Color = color;
    }

    /// <summary>
    /// Static constructor for a vertex containing a
    /// - position
    /// - color
    /// - texture
    /// </summary>
    static Vertex() {
        var data = new VertexData[] {
            new VertexData(0, VertexFormat.Vector3, VertexType.Position),
            new VertexData(12, VertexFormat.Color, VertexType.Color)
        };
        VertexDefinition = new VertexDefinition(data);
    }
}