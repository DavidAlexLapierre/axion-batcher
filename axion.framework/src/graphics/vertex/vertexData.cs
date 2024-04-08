namespace Axion.Graphics;

class VertexData {
    public int Offset { get; private set; }
    public VertexFormat Format { get; private set; }
    public VertexType Type { get; private set; }

    public VertexData(int offset, VertexFormat format, VertexType type) {
        Offset = offset;
        Format = format;
        Type = type;
    }
}