using Axion;
using Axion.Data;
using Axion.Graphics;
using OpenTK.Mathematics;

class Batcher {
    const int NB_INDEX_PER_ITEM = 6;
    const int NB_VERTEX_PER_ITEM = 4;
    const int SIZE_GROWTH = 64;
    Texture texture;

    public Shader Shader { get; private set; }
    Vertex[] batchItems;
    short[] indices;
    int batchItemCount;
    int indexCount => batchItemCount / NB_VERTEX_PER_ITEM * NB_INDEX_PER_ITEM;

    internal Batcher(Shader shader) {
        Shader = shader;
        batchItemCount = 0;
        indices = new short[SIZE_GROWTH * NB_INDEX_PER_ITEM];
        ResizeIndices(SIZE_GROWTH);
        batchItems = new Vertex[SIZE_GROWTH * NB_VERTEX_PER_ITEM];
        for (int i = 0; i < SIZE_GROWTH; i++) {
            batchItems[i] = new Vertex();
        }
    }

    public unsafe void Batch(Texture texture, Vector3 position, Vector2 origin, Rectangle frame, Color color) {
        this.texture = texture;
        if (batchItemCount >= batchItems.Length) { ResizeBatchItemList(); }
        fixed (Vertex* batchPtr = batchItems) {
            Set(batchPtr + batchItemCount, texture, position, origin, frame, color);
            batchItemCount += 4;
        }
    }

    unsafe void Set(Vertex* Vertices, Texture texture, Vector3 position, Vector2 origin, Rectangle frame, Color color) {
        float xx, yy;
        // Top Left
        xx = position.X - origin.X;
        yy = position.Y - origin.Y;

        Vertices[0].Position = new Vector3(xx, yy, 0);
        Vertices[0].Color = color;


        // Bottom Left
        xx = position.X - origin.X;
        yy = position.Y - origin.Y + frame.Height;

        Vertices[1].Position = new Vector3(xx, yy, 0);
        Vertices[1].Color = color;

        // Bottom Right
        xx = position.X - origin.X + frame.Width;
        yy = position.Y - origin.Y + frame.Height;

        Vertices[2].Position = new Vector3(xx, yy, 0);
        Vertices[2].Color = color;

        // Top Right
        xx = position.X - origin.X + frame.Width;
        yy = position.Y - origin.Y;

        Vertices[3].Position = new Vector3(xx, yy, 0);
        Vertices[3].Color = color;
    }

    public unsafe void RenderBatch(Matrix4 worldMat) {
        Shader.SetUniform("worldMat", worldMat);
        Shader.SetUniform("textureSampler", texture); // NEED TO SAMPLE THE TEXTURE
        Axn.Game.GraphicsDevice.Flush(batchItems, batchItemCount, indices, indexCount, Shader);
        ResetBatch();
    }

    /// <summary>
    /// Reset the batch to the initial state
    /// </summary>
    void ResetBatch() {
        batchItemCount = 0;
    }

    unsafe void ResizeBatchItemList() {
        var oldSize = batchItems.Length;
        var newSize = oldSize + oldSize / 2;
        newSize = (newSize + (SIZE_GROWTH - 1)) & ~(SIZE_GROWTH-1);
        Array.Resize(ref batchItems, newSize);

        // Update the sprite meshes
        for (int i = oldSize; i < newSize; i++) {
            batchItems[i] = new Vertex();
        }

        ResizeIndices(newSize / NB_VERTEX_PER_ITEM);
    }

    unsafe void ResizeIndices(int newSize) {
        // Update indices
        int start = 0;
        short[] newIndices = new short[NB_INDEX_PER_ITEM * newSize];
        fixed(short* indexFixedPtr = newIndices) {
            var indexPtr = indexFixedPtr + (start * NB_INDEX_PER_ITEM);
            var spriteIndices = new int[]{0, 1, 3, 3, 1, 2};
            for (var i = start; i < newSize; i++, indexPtr += 6) {

                *(indexPtr + 0) = (short)(i * NB_VERTEX_PER_ITEM + spriteIndices[0]);
                *(indexPtr + 1) = (short)(i * NB_VERTEX_PER_ITEM + spriteIndices[1]);
                *(indexPtr + 2) = (short)(i * NB_VERTEX_PER_ITEM + spriteIndices[2]);
                *(indexPtr + 3) = (short)(i * NB_VERTEX_PER_ITEM + spriteIndices[3]);
                *(indexPtr + 4) = (short)(i * NB_VERTEX_PER_ITEM + spriteIndices[4]);
                *(indexPtr + 5) = (short)(i * NB_VERTEX_PER_ITEM + spriteIndices[5]);
            }
        }

        indices = newIndices;
    }
}