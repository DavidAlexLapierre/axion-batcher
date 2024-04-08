struct ShaderJson {
    public string Name { get; set; }
    public string FragmentShader { get; set; }
    public string VertexShader { get; set; }
    public ShaderAttributeJson[] Attributes { get; set; }
}