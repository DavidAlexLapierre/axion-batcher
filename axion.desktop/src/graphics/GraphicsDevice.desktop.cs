using Axion.Desktop;
using Axion.Graphics;
using OpenTK.Graphics.OpenGL4;

class DesktopGraphicsDevice : GraphicsDevice {

    public Dictionary<string, int> Vaos { get; private set; }
    public Dictionary<string, int> Ibos { get; private set; }
    public Dictionary<int, VertexBuffer> Vbos { get; private set; }

    public DesktopGraphicsDevice() : base(new DesktopShaderBuilder()) {
        Vaos = new Dictionary<string, int>();
        Ibos = new Dictionary<string, int>();
        Vbos = new Dictionary<int, VertexBuffer>();
    }

    public override void Flush(Vertex[] vertices, int vertexCount, short[] indices, int indexCount, Shader shader) {
        var vao = LoadVAO(shader);
        
        // Initialize the VAO
        ShaderBuilder.Use(shader);
        GL.BindVertexArray(vao);

        // Prepare the data
        BindIndices(indices, indexCount, shader);        
        ShaderBuilder.EnableAttributes(Vertex.VertexDefinition);
        StoreAttributes(vertices, vertexCount);

        // Draw
        GL.DrawElements(PrimitiveType.Triangles, indexCount, DrawElementsType.UnsignedShort, 0);

        // Unbind
        ShaderBuilder.DisableAttributes(Vertex.VertexDefinition);
        GL.BindVertexArray(0);
        ShaderBuilder.Stop(shader.Name);
    }

    int LoadVAO(Shader shader) {
        if (!Vaos.ContainsKey(shader.Name)) {
            Vaos.Add(shader.Name, GL.GenVertexArray());
        }

        return Vaos[shader.Name];
    }

    void StoreAttributes(Vertex[] vertices, int vertexCount) {
        var definition = Vertex.VertexDefinition;
        for (int i = 0; i < definition.VertexData.Length; i++) {
            var buffer = Vbos.ContainsKey(i) ? Vbos[i] : new VertexBuffer(vertexCount);
            if (!Vbos.ContainsKey(i)) Vbos.Add(i, buffer);

            buffer.SetBufferData(i, vertices, vertexCount, definition);
        }
    }

    void BindIndices(short[] indices, int indexCount, Shader shader) {
        var ibo = LoadIBO(shader);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indexCount * sizeof(short), indices, BufferUsageHint.DynamicDraw);
    }

    int LoadIBO(Shader shader) {
        if (!Ibos.ContainsKey(shader.Name)) {
            Ibos.Add(shader.Name, GL.GenBuffer());
        }

        return Ibos[shader.Name];
    }


    public override void Dispose() {
        foreach (var vertexBuffer in Vbos.Values) {
            vertexBuffer.Dispose();
        }
        foreach (var vao in Vaos.Values) {
            GL.DeleteVertexArray(vao);
        }
    }
}