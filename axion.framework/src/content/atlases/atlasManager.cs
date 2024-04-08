using Axion.Components;
using OpenTK.Mathematics;

namespace Axion.Content;

class AtlasManager {
    /// <summary>
    /// Dictionary of all the sprites that were loaded
    /// </summary>
    Dictionary<string, Sprite> sprites;
    /// <summary>
    /// Dictionary of all the atlases and sprite data
    /// </summary>
    Dictionary<Guid, Atlas> atlases;
    /// <summary>
    /// ID of the current atlas used to load the data onto
    /// </summary>
    Guid currentAtlas;

    /// <summary>
    /// Reference to the texture builder
    /// </summary>
    ITextureBuilder textureBuilder;

    public AtlasManager(ITextureBuilder builder) {
        textureBuilder = builder;
        sprites = new Dictionary<string, Sprite>();
        atlases = new Dictionary<Guid, Atlas>();
    }

    /// <summary>
    /// Load a sprite into the atlas
    /// </summary>
    /// <param name="data">JSON data of the sprite</param>
    public void Load(JsonSpriteData data) {
        if (sprites.ContainsKey(data.Name)) return;
        
        if (currentAtlas == Guid.Empty) {
            currentAtlas = CreateAtlas();
        }

        // If there's room, store in the existing atlas
        List<FrameData> updatedList = null;
        while (updatedList is null) {
            updatedList = atlases[currentAtlas].Store(data);
            if (updatedList is null) {
                currentAtlas = CreateAtlas();
            }
        }

        // Once we have the list of frames, create the data and map it to the atlas
        var atlasData = new Sprite(updatedList, atlases[currentAtlas].Texture, new Vector2(data.Origin.X, data.Origin.Y), data.AnimationSpeed);
        sprites.Add(data.Name, atlasData);
    }

    /// <summary>
    /// Create a new atlas
    /// </summary>
    /// <returns></returns>
    Guid CreateAtlas() {
        var atlas = new Atlas(textureBuilder);
        atlases.Add(atlas.Id, atlas);
        return atlas.Id;
    }

    /// <summary>
    /// Get the sprite data of a sprite
    /// </summary>
    /// <param name="name">Name of the sprite to retrieve</param>
    /// <returns></returns>
    internal Sprite GetSpriteData(string name) {
        if (sprites.ContainsKey(name)) {
            return sprites[name].Clone();
        }

        return null;
    }
    
}