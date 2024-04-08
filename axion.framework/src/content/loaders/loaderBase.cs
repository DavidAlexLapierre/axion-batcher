namespace Axion.Content;

public abstract class LoaderBase {
    /// <summary>
    /// Load an asset from the content and process its data
    /// </summary>
    /// <typeparam name="T">Type of data to convert to</typeparam>
    /// <param name="assetPath">Path of the asset in the content directory</param>
    public abstract void Load<T>(string assetPath);

    /// <summary>
    /// Load an asset directory
    /// </summary>
    /// <typeparam name="T">Type of asset to load</typeparam>
    /// <param name="asset">Asset to load</param>
    public virtual void Load<T>(T asset) {}

    /// <summary>
    /// Retrieve an asset from the loader
    /// </summary>
    /// <typeparam name="T">Type of asset to retrieve</typeparam>
    /// <param name="asset">ID of the asset</param>
    /// <returns></returns>
    public abstract T Get<T>(string asset) where T : class;
}