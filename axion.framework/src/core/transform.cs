using Axion.Utils;
using OpenTK.Mathematics;

namespace Axion;

public class Transform {
    /// <summary>
    /// X coordinate
    /// </summary>
    public float X => Position.X;
    /// <summary>
    /// Y coordinate
    /// </summary>
    public float Y => Position.Y;
    /// <summary>
    /// Z coordinate
    /// </summary>
    public float Z => Position.Z;
    /// <summary>
    /// Rotation on the X axis in degrees
    /// </summary>
    public float Pitch => MathUtils.radToDeg(Rotation.X);
    /// <summary>
    /// Rotation on the Y axis in degrees
    /// </summary>
    public float Yaw => MathUtils.radToDeg(Rotation.Y);
    /// <summary>
    /// Rotation on the Z axis in degrees
    /// </summary>
    public float Roll => MathUtils.radToDeg(Rotation.Z);

    /// <summary>
    /// Scale on the X axis
    /// </summary>
    public float ScaleX => Scale.X;
    /// <summary>
    /// Scale on the Y axis
    /// </summary>
    public float ScaleY => Scale.Y;
    /// <summary>
    /// Scale on the Z axis
    /// </summary>
    public float ScaleZ => Scale.Z;

    /// <summary>
    /// Position of the entity
    /// </summary>
    public Vector3 Position { get; protected set; }
    /// <summary>
    /// Rotation of the entity
    /// </summary>
    public Vector3 Rotation { get; protected set; }
    /// <summary>
    /// Scale of the entity
    /// </summary>
    public Vector3 Scale { get; protected set; }
    /// <summary>
    /// Depth value, used for 2D entities
    /// </summary>
    public int Depth;

    public Transform() {
        Position = Vector3.Zero;
        Rotation = Vector3.Zero;
        Scale = Vector3.One;
        Depth = 0;
    }

    /// <summary>
    /// Move on the X and Y axis
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    public void Move(float x, float y) {
        Position += new Vector3(x, y, 0);
    }

    /// <summary>
    /// Move on the X, Y and Z axis
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    public void Move(float x, float y, float z) {
        Position += new Vector3(x, y, z);
    }

    /// <summary>
    /// Set the position of the entity
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    public void SetPosition(float x, float y) {
        Position = new Vector3(x, y, Position.Z);
    }
    
    /// <summary>
    /// Set the position of the entity
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    public void SetPosition(int x, int y, int z) {
        Position = new Vector3(x, y, z);
    }

    /// <summary>
    /// Rotate in degrees along the X, Y and Z axis
    /// </summary>
    /// <param name="pitch">Rotation on the X axis in degrees</param>
    /// <param name="yaw">Rotation on the Y axis in degrees</param>
    /// <param name="roll">Rotation on the Z axis in degrees</param>
    public void Rotate(float pitch, float yaw, float roll) {
        Rotation += new Vector3(MathUtils.degToRad(pitch), MathUtils.degToRad(yaw), MathUtils.degToRad(roll));
    }

    /// <summary>
    /// Set the rotation in degrees along the X, Y and Z axis
    /// </summary>
    /// <param name="pitch">Rotation on the X axis in degrees</param>
    /// <param name="yaw">Rotation on the Y axis in degrees</param>
    /// <param name="roll">Rotation on the Z axis in degrees</param>
    public void SetRotation(float pitch, float yaw, float roll) {
        Rotation = new Vector3(MathUtils.degToRad(pitch), MathUtils.degToRad(yaw), MathUtils.degToRad(roll));
    }

    /// <summary>
    /// Rotate along the X axis
    /// </summary>
    /// <param name="pitch">Rotation on the X axis in degrees</param>
    public void RotatePitch(float pitch) {
        Rotation += new Vector3(MathUtils.degToRad(pitch), 0, 0);
    }

    /// <summary>
    /// Rotate along the Y axis
    /// </summary>
    /// <param name="yaw">Rotation on the Y axis in degrees</param>
    public void RotateYaw(float yaw) {
        Rotation += new Vector3(0, MathUtils.degToRad(yaw), 0);
    }

    /// <summary>
    /// Rotate along the Z axis
    /// </summary>
    /// <param name="roll">Rotation on the Z axis in degrees</param>
    public void RotateRoll(float roll) {
        Rotation += new Vector3(0, 0, MathUtils.degToRad(roll));
    }

    /// <summary>
    /// Increase the scale of the entity
    /// </summary>
    /// <param name="x">X value to increase by</param>
    /// <param name="y">Y value to increase by</param>
    public void IncreaseScale(float x, float y) {
        Scale += new Vector3(x, y, 0);
    }

    /// <summary>
    /// Increase the scale of the entity
    /// </summary>
    /// <param name="x">X value to increase by</param>
    /// <param name="y">Y value to increase by</param>
    /// <param name="z">Z value to increase by</param>
    public void IncreaseScale(float x, float y, float z) {
        Scale += new Vector3(x, y, z);
    }

    /// <summary>
    /// Set the scale of the entity
    /// </summary>
    /// <param name="x">X value</param>
    /// <param name="y">Y value</param>
    public void SetScale(float x, float y) {
        Scale = new Vector3(x, y, 1);
    }

    /// <summary>
    /// Set the scale of the entity
    /// </summary>
    /// <param name="x">X value</param>
    /// <param name="y">Y value</param>
    /// <param name="z">Z value</param>
    public void SetScale(float x, float y, float z) {
        Scale = new Vector3(x, y, z);
    }
}