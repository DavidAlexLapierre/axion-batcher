namespace Axion.Content;
class OriginData {
    public int X { get; set; }
    public int Y { get; set; }
}

class FrameData {
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

class JsonSpriteData {
    public string Name { get; set; }
    public float AnimationSpeed { get; set; }
    public string Texture { get; set; }
    public OriginData Origin { get; set; }
    public FrameData[] Frames { get; set; }
}