namespace Axion.Graphics;

public class Mesh {
    public string Shader { get; protected set; } = string.Empty;
    public Vertex[] Vertices { get; protected set; }
}