using Axion.Data;
using Axion.Graphics;
using OpenTK.Mathematics;

namespace Axion.Content;

/// <summary>
/// Class representing a texture atlas. A texture atlas is a 2048x2048 texture
/// combining the different textures used by the game.
/// </summary>
class Atlas {
    const int CELL_DIM = 2;
    const int MAX_SIZE = 2048;
    public Texture Texture { get; private set; }
    public Guid Id { get; private set; }
    bool[,] insertions;
    Vector2 lastInsertion;

    public Atlas(ITextureBuilder builder) {
        Id = Guid.NewGuid();
        Texture = new Texture(MAX_SIZE, MAX_SIZE, builder);
        insertions = new bool[MAX_SIZE / CELL_DIM, MAX_SIZE / CELL_DIM];
        lastInsertion = new Vector2();
    }

    /// <summary>
    /// Tries to store sprite data within the atlas. If there's not enough room
    /// it will return null. If it was able to store the data,
    /// it will return a list of the sprite texture coordinates within the atlas
    /// </summary>
    /// <param name="data">Sprite data that was loaded</param>
    /// <returns>List of the texture coordinates on the atlas or null</returns>
    public List<FrameData> Store(JsonSpriteData data) {
        List<FrameData> frameList = data.Frames.ToList();

        var newFrames = TryPlaceFrames(frameList);

        // Means we failed to place everything in the texture
        if (newFrames.Count != frameList.Count) { return null; }

        try {
            // Store the texture data in the atlas
            var spriteTexture = Axn.Get<Texture>(data.Texture);
            Color[] newData = null;
            // get the initial sprite data
            foreach (var frame in frameList) {
                var source = new Rectangle(frame.X, frame.Y, frame.Width, frame.Height);
                newData = spriteTexture.GetData(source);
            }
            if (newData is not null) {
                // Add data to the texture
                for(int i = 0; i < newFrames.Count; i++) {
                    var destination = new Rectangle(newFrames[i].X, newFrames[i].Y, newFrames[i].Width, newFrames[i].Height);
                    Texture.SetData(destination, newData);
                }
            }
        } catch (Exception e) {
            // TODO: Implement proper logging structure
            Console.WriteLine(string.Format("Failed to load texture {0} from sprite {1}", data.Texture ,data.Name));
            Console.WriteLine(e);

            return null;
        }

        return newFrames;
    }

    /// <summary>
    /// Given a frame, tries to place it within the atlas.
    /// </summary>
    /// <param name="frames">List of frames that it needs to store</param>
    /// <returns>List of the texture coordinates on the atlas or null</returns>
    List<FrameData> TryPlaceFrames(List<FrameData> frames) {
        List<FrameData> newFrames = new List<FrameData>();
        bool[,] mapClone = (bool[,])insertions.Clone();
        foreach (var frame in frames) {
            var found = false;
            for (int x = (int)lastInsertion.X; x < insertions.GetLength(0); x++) {
                for (int y = (int)lastInsertion.Y; y < insertions.GetLength(1); y++) {
                    if (!Overlaps(frame, x, y, mapClone)) {
                        newFrames.Add(frame);
                        mapClone = ModifyMap(frame, x, y, mapClone);
                        lastInsertion = new Vector2(x, y);
                        found = true;
                    }
                    if (found) break;
                }
                if (found) break;
            }
        }

        // Modify the insertion map
        if (newFrames.Count == frames.Count) insertions = mapClone;

        return newFrames;
    }

    /// <summary>
    /// Modified the insertion map to let the atlas know it already has data stored
    /// </summary>
    /// <param name="frame">Frame to store</param>
    /// <param name="x">X coordiante</param>
    /// <param name="y">Y coordiante</param>
    /// <param name="insertionMap">Map containing the current insertions</param>
    /// <returns></returns>
    bool[,] ModifyMap(FrameData frame, int x, int y, bool[,] insertionMap) {
        var rectW = frame.Width / CELL_DIM;
        var rectH = frame.Height / CELL_DIM;

        for (int i = x; i < rectW; i++) {
            for (int j = y; j < rectH; j++) {
                insertionMap[i, j] = true;
            }
        }

        return insertionMap;
    }

    /// <summary>
    /// Verified whether two rectangle overlap
    /// </summary>
    /// <param name="rectangle">Rectangle to loop for</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="existingList">Existing list</param>
    /// <returns></returns>
    bool Overlaps(FrameData rectangle, int x, int y, bool[,] insertionMap) {
        var rectW = rectangle.Width / CELL_DIM;
        var rectH = rectangle.Height / CELL_DIM;

        if (rectW >= insertionMap.GetLength(0)) return true;
        if (rectH >= insertionMap.GetLength(1)) return true;

        for (int i = x; i < rectW; i++) {
            for (int j = y; j < rectH; j++) {
                if (insertionMap[i,j]) {
                    return true;
                }
            }
        }

        return false;
    }
}