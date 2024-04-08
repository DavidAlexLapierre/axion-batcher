namespace Axion.Data;

public struct Rectangle {
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Rectangle() { }

    public Rectangle(int w, int h) {
        Width = w;
        Height = h;
    }

    public Rectangle(int x, int y, int w, int h) {
        X = x;
        Y = y;
        Width = w;
        Height = h;
    }
}