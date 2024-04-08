using Axion.Components;
using Axion.Content.Loaders;
using Axion.Graphics;

namespace Axion.Content;

public class ResourceManager {
    /// <summary>
    /// Map of the different Loaders
    /// </summary>
    Dictionary<Type, LoaderBase> loaders;

    /// <summary>
    /// Constructor for the resource manager
    /// </summary>
    internal ResourceManager(ITextureBuilder builder) {
        loaders = new Dictionary<Type, LoaderBase>() {
            { typeof(Texture), new TextureLoader() },
            { typeof(Sprite), new SpriteLoader(builder) },
            { typeof(Shader), new ShaderLoader() }
        };
    }
    
    /// <summary>
    /// Retrieve an asset from the content
    /// </summary>
    /// <typeparam name="T">Type of asset to retrieve</typeparam>
    /// <param name="asset">Name of the asset to retrieve</param>
    /// <returns></returns>
    public T Get<T>(string asset) where T : class {
        var key = typeof(T);
        if (loaders.ContainsKey(key)) {
            return loaders[key].Get<T>(asset);
        }

        return default;
    }

    /// <summary>
    /// Load an asset to memory
    /// </summary>
    /// <typeparam name="T">Type of asset to load</typeparam>
    /// <param name="assetName">Name of the asset</param>
    public void Load<T>(string assetName) {
        var key = typeof(T);
        if (loaders.ContainsKey(key)) {
            loaders[key].Load<T>(assetName);
        }
    }

    /// <summary>
    /// Load an asset directory
    /// </summary>
    /// <typeparam name="T">Type of asset to load</typeparam>
    /// <param name="asset">Asset to load</param>
    public void Load<T>(T asset) {
        var key = typeof(T);
        if (loaders.ContainsKey(key)) {
            loaders[key].Load(asset);
        }
    }

    /// <summary>
    /// Register a new data loader
    /// </summary>
    /// <typeparam name="T">Type of data to load</typeparam>
    /// <param name="loader">The loader class handling the load and get method</param>
    public void RegisterLoader<T>(LoaderBase loader) {
        var dataType = typeof(T);
        if (!loaders.ContainsKey(dataType)) {
            loaders.Add(dataType, loader);
        }
    }
}