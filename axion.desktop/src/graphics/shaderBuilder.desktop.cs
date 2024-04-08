using Axion.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Axion.Desktop;

class DesktopShaderBuilder : IShaderBuilder {
    Dictionary<string, int> programs;

    public DesktopShaderBuilder() {
        programs = new Dictionary<string, int>();
    }

    public void Use(Shader shader) {
        if (programs.ContainsKey(shader.Name)) {
            var program = programs[shader.Name];
            GL.UseProgram(program);
            BindUniforms(program, shader);
        }
    }

    public void Stop(string name) => GL.UseProgram(0);

    public void Load(Shader shader) {
        if (!programs.ContainsKey(shader.Name)) {
            var vertexShader = Compile(shader.VertexShaderSource, ShaderType.VertexShader);
            var fragmentShader = Compile(shader.FragmentShaderSource, ShaderType.FragmentShader);
            var program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);
            GL.ValidateProgram(program);

            BindUniforms(program, shader);

            programs.Add(shader.Name, program);
        } else {
            Console.WriteLine($"Shaders already exists {shader.Name}");
            throw new Exception();
        }
    }

    void BindUniforms(int program, Shader shader) {
        foreach (var (uniform, data) in shader.Uniforms) {
            var location = GetUniformLocation(program, uniform);
            if (data is Matrix4) LoadMat4Uniform(location, (Matrix4)data);
            if (data is Texture) LoadTextureUniform(location, (Texture)data);
        }
    }

    public void EnableAttributes(VertexDefinition definition) {
        for (int i = 0; i < definition.VertexData.Length; i++) {
            GL.EnableVertexAttribArray(i);
        }
    }

    public void DisableAttributes(VertexDefinition definition) {
        for (int i = 0; i < definition.VertexData.Length; i++) {
            GL.DisableVertexAttribArray(i);
        }
    }

    public int GetUniformLocation(int program, string uniform) {
        return GL.GetUniformLocation(program, uniform);
    }

    void LoadMat4Uniform(int location, Matrix4 mat) {
        GL.UniformMatrix4(location, false, ref mat);
    }

    void LoadTextureUniform(int location, Texture texture) {
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, texture.TextureId);
    }



    int Compile(string source, ShaderType shaderType) {
        var shader = GL.CreateShader(shaderType);
        GL.ShaderSource(shader, source);
        GL.CompileShader(shader);
        int success;
        GL.GetShader(shader, ShaderParameter.CompileStatus, out success);
        if (success != 1) {
            var log = GL.GetShaderInfoLog(shader);
            Console.WriteLine($"Shader compilation failed for {shaderType} \n {log}");
            GL.DeleteShader(shader);
            return -1;
        }

        return shader;
    }
}