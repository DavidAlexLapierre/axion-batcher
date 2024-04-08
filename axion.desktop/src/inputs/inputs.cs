using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Axion.Desktop;

public class Inputs {
    internal static Keyboard keyboard;

    static Inputs() {
        keyboard = new Keyboard();
    }

    /// <summary>
    /// Checks whether a key has been pressed or not
    /// </summary>
    /// <param name="key">Key to look for</param>
    /// <returns></returns>
    public static bool KeyPressed(Keys key) {
        var retVal = keyboard.CheckKeyPressed(key);
        keyboard.Update();
        return retVal;
    }

    /// <summary>
    /// Checks whether a key is being held down or not
    /// </summary>
    /// <param name="key">Key to look for</param>
    /// <returns></returns>
    public static bool KeyDown(Keys key) {
        var retVal = keyboard.CheckKeyDown(key);
        keyboard.Update();
        return retVal;
    }

    /// <summary>
    /// CHecks whether a key has been released
    /// </summary>
    /// <param name="key">Key to look for</param>
    /// <returns></returns>
    public static bool KeyUp(Keys key) {
        var retVal = keyboard.CheckKeyUp(key);
        keyboard.Update();
        return retVal;
    }
}