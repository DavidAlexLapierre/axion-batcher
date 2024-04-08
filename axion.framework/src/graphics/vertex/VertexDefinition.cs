namespace Axion.Graphics;

class VertexDefinition {
    public VertexData[] VertexData;
    public int Stride;
    public VertexDefinition(VertexData[] vertexData) : this(GetVertexStride(vertexData), vertexData)  {

    }

    public VertexDefinition(int stride, VertexData[] vertexData) {
        Stride = stride;
        VertexData = vertexData;
    }

    private static int GetVertexStride(VertexData[] elements) {
        int max = 0;
        for (var i = 0; i < elements.Length; i++) {
            var start = elements[i].Offset + (short)elements[i].Format;
            if (max < start)
                max = start;
        }

        return max;
    }
}