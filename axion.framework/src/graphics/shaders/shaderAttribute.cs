namespace Axion.Graphics;

public struct ShaderAttribute {
    public string Name { get; set; }
    public int Count { get; set; }
    public int Index { get; set; }

    public ShaderAttribute(int index, string name, int count) {
        Name = name;
        Index = index;
        Count = count;
    }
}