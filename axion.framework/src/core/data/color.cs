namespace Axion.Data;

public struct Color {
    public float R { get; private set; }
    public float G { get; private set; }
    public float B { get; private set; }
    public float A { get; private set; }
    public Color() {
        R = 1;
        G = 1;
        B = 1;
        A = 1;
    }

    public Color(byte r, byte g, byte b) {
        R = r / 255f;
        G = g / 255f;
        B = b / 255f;
        A = 1;
    }

    public Color(byte r, byte g, byte b, byte a) {
        R = r / 255f;
        G = g / 255f;
        B = b / 255f;
        A = a / 255f;
    }
}