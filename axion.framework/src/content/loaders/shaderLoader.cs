using System.Text.Json;
using Axion.Graphics;

namespace Axion.Content;

class ShaderLoader : LoaderBase {
    Dictionary<string, Shader> shaders;

    public ShaderLoader() {
        shaders = new Dictionary<string, Shader>();
    }

    public override T Get<T>(string asset) {
        if (shaders.ContainsKey(asset)) {
            return shaders[asset] as T;
        }

        return default;
    }

    public override void Load<T>(string assetPath) {
        string json;
        
        using (StreamReader reader = File.OpenText(assetPath)) {
            json = reader.ReadToEnd();
        }

        var content = JsonSerializer.Deserialize<ShaderJson>(json);

        if (!shaders.ContainsKey(content.Name)) {
            string fragmentSource;
            string vertexSource;

            // Read the fragment shader
            using (StreamReader reader = File.OpenText(content.FragmentShader)) {
                fragmentSource = reader.ReadToEnd();
            }

            // Read the vertex shadeer
            using (StreamReader reader = File.OpenText(content.VertexShader)) {
                vertexSource = reader.ReadToEnd();
            }

            var shader = new Shader(content.Name, fragmentSource, vertexSource);
            shaders.Add(content.Name, shader);
            Axn.Game.GraphicsDevice.ShaderBuilder.Load(shader);
        }
    }

    public override void Load<T>(T asset) {
        var shader = asset as Shader;
        if (!shaders.ContainsKey(shader.Name)) {
            shaders.Add(shader.Name, shader);
            Axn.Game.GraphicsDevice.ShaderBuilder.Load(shader);
        }
    }
}