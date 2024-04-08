using OpenTK.Mathematics;

namespace Axion.Graphics;

public class Shader {
    internal string Name { get; private set; }
    internal Dictionary<string, object> Uniforms { get; private set; }
    internal string FragmentShaderSource { get; set; }
    internal string VertexShaderSource { get; set; }

    internal Shader(string name) {
        Name = name;
        Uniforms = new Dictionary<string, object>();
    }

    internal Shader(string name, string fragmentSource, string vertexSource) {
        Name = name;
        VertexShaderSource = vertexSource;
        FragmentShaderSource = fragmentSource;
        Uniforms = new Dictionary<string, object>();
    }

    internal void SetUniform(string uniform, Matrix4 mat) {
        if (!Uniforms.ContainsKey(uniform)) {
            Uniforms.Add(uniform, mat);
        } else {
            Uniforms[uniform] = mat;
        }
    }

    internal void SetUniform(string uniform, Texture texture) {
        if (!Uniforms.ContainsKey(uniform)) {
            Uniforms.Add(uniform, texture);
        } else {
            Uniforms[uniform] = texture;
        }
    }
}