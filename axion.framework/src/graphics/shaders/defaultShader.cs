namespace Axion.Graphics;

class DefaultShader : Shader {
    public DefaultShader() : base("default") {
        VertexShaderSource = @"
            #version 330 core

            layout (location=0) in vec3 pos;
            layout (location=1) in vec4 color;

            uniform mat4 worldMat;

            out vec4 int_color;
            out vec2 int_texCoords;

            void main(void) {
                gl_Position = worldMat * vec4(pos, 1.0);
                int_color = color;
            }
        ";

        FragmentShaderSource = @"
            #version 330 core

            in vec4 int_color;
            in vec2 int_texCoords;

            out vec4 outColor;

            void main(void) {
                outColor = int_color;
            }
        ";
    }
}