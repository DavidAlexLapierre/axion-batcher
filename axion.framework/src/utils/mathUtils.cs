namespace Axion.Utils;

public class MathUtils {
    /// <summary>
    /// Converts degrees to radians
    /// </summary>
    /// <param name="degrees">Angle in degrees</param>
    /// <returns>Angle in radians</returns>
    public static float degToRad(double degrees) {
        return (float)(degrees * (Math.PI / 180.0));
    }

    /// <summary>
    /// Converts radians to degrees
    /// </summary>
    /// <param name="rads">Angle in radians</param>
    /// <returnsAngle in degrees></returns>
    public static float radToDeg(double rads) {
        return (float)(rads * (180.0 / Math.PI));
    }
}