using System.Runtime.InteropServices;
using Axion.Graphics;
using OpenTK.Graphics.OpenGL4;

unsafe class VertexBuffer : IDisposable {
    public int Vbo { get; private set; }

    public VertexBuffer(int vertexCount) {
        if (Vbo == 0) {
            Vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, Vbo);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                new IntPtr(Marshal.SizeOf(typeof(Vertex)) * vertexCount),
                IntPtr.Zero,
                BufferUsageHint.DynamicDraw
            );
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }

    public unsafe void SetBufferData(int attributeNumber, Vertex[] vertices, int itemCount, VertexDefinition definition) {
        GL.BindBuffer(BufferTarget.ArrayBuffer, Vbo);

        GCHandle handle = GCHandle.Alloc(vertices, GCHandleType.Pinned);

        try {
            
            IntPtr vertexDataPtr = handle.AddrOfPinnedObject();
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, itemCount * Marshal.SizeOf(typeof(Vertex)), vertexDataPtr);

            var vertexData = definition.VertexData[attributeNumber];
            GL.VertexAttribPointer(attributeNumber, (int)vertexData.Format, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(Vertex)), (IntPtr)vertexData.Offset);
        } finally {
            handle.Free();
        }

        // Unbind the buffer
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public void Dispose() {
        GL.DeleteBuffer(Vbo);
    }
}