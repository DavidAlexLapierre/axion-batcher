namespace Axion.Graphics;

abstract class GraphicsDevice : IDisposable {
    public IShaderBuilder ShaderBuilder { get; private set; }

    public GraphicsDevice(IShaderBuilder shaderBuilder) {
        ShaderBuilder = shaderBuilder;
    }

    public void Init() {
        Axn.Load<Shader>(new DefaultShader());
    }

    public abstract void Flush(Vertex[] vertices, int vertexCount, short[] indices, int indexCount, Shader shader);

    public abstract void Dispose();
}