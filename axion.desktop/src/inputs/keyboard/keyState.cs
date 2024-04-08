using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Axion.Desktop;

class KeyState {
    public Dictionary<Keys, bool> downStates { get; private set; }
    public Dictionary<Keys, bool> upStates { get; private set; }

    /// <summary>
    /// Default constructor which sets all the values to false
    /// </summary>
    public KeyState() {
        downStates = new Dictionary<Keys, bool>();
        upStates = new Dictionary<Keys, bool>();

        foreach (var key in Enum.GetValues(typeof(Keys)).Cast<Keys>()) {
            if (!downStates.ContainsKey(key)) downStates.Add(key, false);
            if (!upStates.ContainsKey(key)) upStates.Add(key, false);
        }
    }

    /// <summary>
    /// Used to clone a state
    /// </summary>
    /// <param name="clone">Value to clone</param>
    public KeyState(KeyState clone) {
        downStates = new Dictionary<Keys, bool>(clone.downStates);
        upStates = new Dictionary<Keys, bool>(clone.upStates);
    }

    /// <summary>
    /// Get the current down state for a given key
    /// </summary>
    /// <param name="key">Key to look for</param>
    /// <returns>Down state of the key</returns>
    public bool GetDownState(Keys key) { return downStates[key]; }

    /// <summary>
    /// Get the current up state for a given key
    /// </summary>
    /// <param name="key">Key to look for</param>
    /// <returns>Up state of the key</returns>
    public bool GetUpState(Keys key) { return upStates[key]; }

    /// <summary>
    /// Set the current down state for a given key
    /// </summary>
    /// <param name="key">Key to look for</param>
    public void SetDownState(Keys key, bool enabled) => downStates[key] = enabled;

    /// <summary>
    /// Set the current up state for a given key
    /// </summary>
    /// <param name="key">Key to look for</param>
    public void SetUpState(Keys key, bool enabled) => upStates[key] = enabled;


}