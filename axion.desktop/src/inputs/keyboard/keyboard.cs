
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Axion.Desktop;

internal class Keyboard {
    KeyState previousKeyState;
    KeyState keyState;
    GameWindow Window => Axn.GetGame<DesktopGame>().Window;

    public Keyboard() {
        previousKeyState = new KeyState();
        keyState = new KeyState();
        Window.KeyDown += HandleKeyDown;
        Window.KeyUp += HandleKeyUp;
    }

    /// <summary>
    /// Handler method whenever a key is being held down
    /// </summary>
    /// <param name="args">Keyboard args</param>
    internal void HandleKeyDown(KeyboardKeyEventArgs args) {
        EnableKeyDown(args.Key);
        DisableKeyUp(args.Key);
    }

    /// <summary>
    /// Handler method whenever a key is released
    /// </summary>
    /// <param name="args">Keyboard args</param>
    internal void HandleKeyUp(KeyboardKeyEventArgs args) {
        EnableKeyUp(args.Key);
        DisableKeyDown(args.Key);
    }

    /// <summary>
    /// Validates whether a key is being held down and has only been pressed once
    /// </summary>
    /// <param name="key">Key to look for</param>
    /// <returns>Whether the key has been pressed or not</returns>
    public bool CheckKeyPressed(Keys key) {
        var previous = previousKeyState.GetDownState(key);
        var current = keyState.GetDownState(key);
        if (!previous && current) {
            return current;
        }

        return false;
    }

    /// <summary>
    /// Validates whether a key is being held down
    /// </summary>
    /// <param name="key">Key to look for</param>
    /// <returns>Whether the key is being held down or not</returns>
    public bool CheckKeyDown(Keys key) { return keyState.GetDownState(key); }

    /// <summary>
    /// Validates whether a key has been released
    /// </summary>
    /// <param name="key">Key to look for</param>
    /// <returns>Whether the key has been released</returns>
    public bool CheckKeyUp(Keys key) { return keyState.GetUpState(key) && previousKeyState.GetDownState(key); }

    /// <summary>
    /// Updates the keyboard state
    /// </summary>
    public void Update() => previousKeyState = new KeyState(keyState);

    /// <summary>
    /// Set the keyboard down statefor a given key
    /// </summary>
    /// <param name="key">Key to change the state for</param>
    public void EnableKeyDown(Keys key) => keyState.SetDownState(key, true);
    /// <summary>
    /// Set the keyboard up state for a given key
    /// </summary>
    /// <param name="key">Key to change the state for</param>
    public void EnableKeyUp(Keys key) => keyState.SetUpState(key, true);
    /// <summary>
    /// Reset the keyboard down state for a given key
    /// </summary>
    /// <param name="key">Key to change the state for</param>
    public void DisableKeyDown(Keys key) => keyState.SetDownState(key, false);
    /// <summary>
    /// Reset the keyboard up state for a given key
    /// </summary>
    /// <param name="key">Key to change the state for</param>
    public void DisableKeyUp(Keys key) => keyState.SetUpState(key, false);
}