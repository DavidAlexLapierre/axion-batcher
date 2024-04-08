using OpenTK.Graphics.OpenGL4;

class TextureBuilder : ITextureBuilder {
    public int CreateTexture() {
        int id = GL.GenTexture();
        return id;
    }
}