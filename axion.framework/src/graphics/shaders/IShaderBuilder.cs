namespace Axion.Graphics;

interface IShaderBuilder {
    public void Use(Shader shader);
    public void Stop(string name);
    public void Load(Shader shader);
    public void EnableAttributes(VertexDefinition vertexDefinition);
    public void DisableAttributes(VertexDefinition vertexDefinition);
}