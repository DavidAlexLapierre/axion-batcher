using Axion.Data;
using StbImageSharp;

namespace Axion.Graphics;

public partial class Texture {
    const int DATA_PER_PIXEL = 4;
    public byte[] Data { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    internal int TextureId { get; private set; }

    internal Texture(int width, int height, ITextureBuilder textureBuilder) {
        Data = new byte[width * height * DATA_PER_PIXEL];
        Width = width;
        Height = height;
        TextureId = textureBuilder.CreateTexture();
    }

    internal Texture(ImageResult image) {
        Data = image.Data;
        Width = image.Width;
        Height = image.Height;
    }

    /// <summary>
    /// Set data within the texture
    /// </summary>
    /// <param name="destination">Destination region which modifies the texture</param>
    /// <param name="data">Data to set</param>
    public void SetData(Rectangle destination, Color[] data) {
        // Ensure the rectangle is within bounds
        if (destination.X < 0 || destination.Y < 0 || destination.X + destination.Width > Width || destination.Y + destination.Height > Height) {
            Console.WriteLine("The destination rectangle is out of bound of the texture");
        } else {
            // Copy the new data into the original array at the specified rectangle
            for (int row = 0; row < destination.Height; row++) {
                for (int col = 0; col < destination.Width; col++) {
                    int originalIndex = ((destination.Y + row) * Width + destination.X + col) * DATA_PER_PIXEL;
                    int newIndex = row * destination.Width + col; // Corrected indexing
                    Data[originalIndex]     =   (byte)data[newIndex].R;
                    Data[originalIndex + 1] =   (byte)data[newIndex].G;
                    Data[originalIndex + 2] =   (byte)data[newIndex].B;
                    Data[originalIndex + 3] =   (byte)data[newIndex].A;
                }
            }
        }
    }


    /// <summary>
    /// Get data from within the texture based on a source rectangle.
    /// </summary>
    /// <param name="source">Region to get data from</param>
    /// <returns>Data from the specified region</returns>
    public Color[] GetData(Rectangle source) {
        // Ensure the source rectangle is within bounds
        if (source.X < 0 || source.Y < 0 || source.X + source.Width > Width || source.Y + source.Height > Height) {
            Console.WriteLine("The source rectangle is out of bounds of the texture");
            return null;
        } else {
            Color[] result = new Color[source.Width * source.Height]; // 4 bytes per pixel
            for (int row = source.Y; row < source.Height; row++) {
                for (int col = source.X; col < source.Width; col++) {
                    int originalIndex = ((source.Y + row) * Width + source.X + col) * 4; // 4 bytes per pixel
                    int resultIndex = row * source.Width + col; // 4 bytes per pixel

                    Color color = new Color(
                        Data[originalIndex],
                        Data[originalIndex + 1],
                        Data[originalIndex + 2],
                        Data[originalIndex + 3]
                    );
                    result[resultIndex] = color;
                }
            }

            return result;
        }
    }
}