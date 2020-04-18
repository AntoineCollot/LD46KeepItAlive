using UnityEngine;

public sealed class Curves
{
    //_____________________Mathfx_____________
    //http://wiki.unity3d.com/index.php?title=Mathfx

    //Ease in out
    public static float Hermite(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
    }

    public static Vector2 Hermite(Vector2 start, Vector2 end, float value)
    {
        return new Vector2(Hermite(start.x, end.x, value), Hermite(start.y, end.y, value));
    }

    public static Vector3 Hermite(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Hermite(start.x, end.x, value), Hermite(start.y, end.y, value), Hermite(start.z, end.z, value));
    }

    //Ease out
    public static float Sinerp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
    }

    public static Vector2 Sinerp(Vector2 start, Vector2 end, float value)
    {
        return new Vector2(Mathf.Lerp(start.x, end.x, Mathf.Sin(value * Mathf.PI * 0.5f)), Mathf.Lerp(start.y, end.y, Mathf.Sin(value * Mathf.PI * 0.5f)));
    }

    public static Vector3 Sinerp(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Mathf.Lerp(start.x, end.x, Mathf.Sin(value * Mathf.PI * 0.5f)), Mathf.Lerp(start.y, end.y, Mathf.Sin(value * Mathf.PI * 0.5f)), Mathf.Lerp(start.z, end.z, Mathf.Sin(value * Mathf.PI * 0.5f)));
    }
    //Ease in
    public static float Coserp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, 1.0f - Mathf.Cos(value * Mathf.PI * 0.5f));
    }

    public static Vector2 Coserp(Vector2 start, Vector2 end, float value)
    {
        return new Vector2(Coserp(start.x, end.x, value), Coserp(start.y, end.y, value));
    }

    public static Vector3 Coserp(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Coserp(start.x, end.x, value), Coserp(start.y, end.y, value), Coserp(start.z, end.z, value));
    }

    //Boing
    public static float Berp(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return start + (end - start) * value;
    }

    public static Vector2 Berp(Vector2 start, Vector2 end, float value)
    {
        return new Vector2(Berp(start.x, end.x, value), Berp(start.y, end.y, value));
    }

    public static Vector3 Berp(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Berp(start.x, end.x, value), Berp(start.y, end.y, value), Berp(start.z, end.z, value));
    }

    //Bounce
    public static float Bounce(float x)
    {
        return Mathf.Abs(Mathf.Sin(6.28f * (x + 1f) * (x + 1f)) * (1f - x));
    }

    public static Vector2 Bounce(Vector2 vec)
    {
        return new Vector2(Bounce(vec.x), Bounce(vec.y));
    }

    public static Vector3 Bounce(Vector3 vec)
    {
        return new Vector3(Bounce(vec.x), Bounce(vec.y), Bounce(vec.z));
    }


    //__________________Others____________________
    //http://gizma.com/easing/


    //Quadratic
    public static float QuadEaseIn(float start,float end, float t)
    {
        return (end-start) * t * t + start;
    }

    public static Vector2 QuadEaseIn(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(QuadEaseIn(vec.x, end.x, t), QuadEaseIn(vec.y, end.y, t));
    }

    public static Vector3 QuadEaseIn(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(QuadEaseIn(vec.x, end.x, t), QuadEaseIn(vec.y, end.y, t), QuadEaseIn(vec.z, end.z, t));
    }


    public static float QuadEaseOut(float start, float end, float t)
    {
        return -(end - start) * t * (t - 2) + start;
    }

    public static Vector2 QuadEaseOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(QuadEaseOut(vec.x, end.x, t), QuadEaseOut(vec.y, end.y, t));
    }

    public static Vector3 QuadEaseOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(QuadEaseOut(vec.x, end.x, t), QuadEaseOut(vec.y, end.y, t), QuadEaseOut(vec.z, end.z, t));
    }

    public static float QuadEaseInOut(float start, float end, float t)
    {
        t *= 2;
        if (t < 1)
            return (end - start) / 2 * t * t + start;
        t--;
        return -(end - start) / 2 * (t * (t - 2) - 1) + start;
    }

    public static Vector2 QuadEaseInOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(QuadEaseInOut(vec.x, end.x, t), QuadEaseInOut(vec.y, end.y, t));
    }

    public static Vector3 QuadEaseInOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(QuadEaseInOut(vec.x, end.x, t), QuadEaseInOut(vec.y, end.y, t), QuadEaseInOut(vec.z, end.z, t));
    }

    //Cubic
    public static float CubicEaseIn(float start, float end, float t)
    {
        return (end - start) * t * t * t + start;
    }

    public static Vector2 CubicEaseIn(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(CubicEaseIn(vec.x, end.x, t), CubicEaseIn(vec.y, end.y, t));
    }

    public static Vector3 CubicEaseIn(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(CubicEaseIn(vec.x, end.x, t), CubicEaseIn(vec.y, end.y, t), CubicEaseIn(vec.z, end.z, t));
    }

    public static float CubicEaseOut(float start, float end, float t)
    {
        t--;
        return (end - start) * (t * t * t + 1) + start;
    }

    public static Vector2 CubicEaseOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(CubicEaseOut(vec.x, end.x, t), CubicEaseOut(vec.y, end.y, t));
    }

    public static Vector3 CubicEaseOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(CubicEaseOut(vec.x, end.x, t), CubicEaseOut(vec.y, end.y, t), CubicEaseOut(vec.z, end.z, t));
    }

    public static float CubicEaseInOut(float start, float end, float t)
    {
        t *= 2;
        if (t < 1)
            return (end - start) / 2 * t * t * t + start;
        t -= 2;
        return (end - start) / 2 * (t * t * t + 2) + start;
    }

    public static Vector2 CubicEaseInOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(CubicEaseInOut(vec.x, end.x, t), CubicEaseInOut(vec.y, end.y, t));
    }

    public static Vector3 CubicEaseInOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(CubicEaseInOut(vec.x, end.x, t), CubicEaseInOut(vec.y, end.y, t), CubicEaseInOut(vec.z, end.z, t));
    }

    //Quartic
    public static float QuartEaseIn(float start, float end, float t)
    {
        return (end - start) * t * t * t * t + start;
    }

    public static Vector2 QuartEaseIn(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(QuartEaseIn(vec.x, end.x, t), QuartEaseIn(vec.y, end.y, t));
    }

    public static Vector3 QuartEaseIn(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(QuartEaseIn(vec.x, end.x, t), QuartEaseIn(vec.y, end.y, t), QuartEaseIn(vec.z, end.z, t));
    }

    public static float QuartEaseOut(float start, float end, float t)
    {
        t--;
        return -(end - start) * (t * t * t * t - 1) + start;
    }

    public static Vector2 QuartEaseOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(QuartEaseOut(vec.x, end.x, t), QuartEaseOut(vec.y, end.y, t));
    }

    public static Vector3 QuartEaseOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(QuartEaseOut(vec.x, end.x, t), QuartEaseOut(vec.y, end.y, t), QuartEaseOut(vec.z, end.z, t));
    }

    public static float QuartEaseInOut(float start, float end, float t)
    {
        t *= 2;
        if (t < 1)
            return (end - start) / 2 * t * t * t * t + start;
        t -= 2;
        return -(end - start) / 2 * (t * t * t * t - 2) + start;
    }

    public static Vector2 QuartEaseInOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(QuartEaseInOut(vec.x, end.x, t), QuartEaseInOut(vec.y, end.y, t));
    }

    public static Vector3 QuartEaseInOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(QuartEaseInOut(vec.x, end.x, t), QuartEaseInOut(vec.y, end.y, t), QuartEaseInOut(vec.z, end.z, t));
    }

    //Quintic
    public static float QuintEaseIn(float start, float end, float t)
    {
        return (end - start) * t * t * t * t * t + start;
    }

    public static Vector2 QuintEaseIn(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(QuintEaseIn(vec.x, end.x, t), QuintEaseIn(vec.y, end.y, t));
    }

    public static Vector3 QuintEaseIn(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(QuintEaseIn(vec.x, end.x, t), QuintEaseIn(vec.y, end.y, t), QuintEaseIn(vec.z, end.z, t));
    }

    public static float QuintEaseOut(float start, float end, float t)
    {
        t--;
        return (end - start) * (t * t * t * t * t + 1) + start;
    }

    public static Vector2 QuintEaseOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(QuintEaseOut(vec.x, end.x, t), QuintEaseOut(vec.y, end.y, t));
    }

    public static Vector3 QuintEaseOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(QuintEaseOut(vec.x, end.x, t), QuintEaseOut(vec.y, end.y, t), QuintEaseOut(vec.z, end.z, t));
    }

    public static float QuintEaseInOut(float start, float end, float t)
    {
        t *= 2;
        if (t < 1)
            return (end - start) / 2 * t * t * t * t * t + start;
        t -= 2;
        return (end - start) / 2 * (t * t * t * t * t + 2) + start;
    }

    public static Vector2 QuintEaseInOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(QuintEaseInOut(vec.x, end.x, t), QuintEaseInOut(vec.y, end.y, t));
    }

    public static Vector3 QuintEaseInOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(QuintEaseInOut(vec.x, end.x, t), QuintEaseInOut(vec.y, end.y, t), QuintEaseInOut(vec.z, end.z, t));
    }

    //Sinusoidal
    public static float SinEaseIn(float start, float end, float t)
    {
        return -(end - start) * Mathf.Cos(t * (Mathf.PI / 2)) + end + start;
    }

    public static Vector2 SinEaseIn(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(QuadEaseIn(vec.x, end.x, t), QuadEaseIn(vec.y, end.y, t));
    }

    public static Vector3 SinEaseIn(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(SinEaseIn(vec.x, end.x, t), SinEaseIn(vec.y, end.y, t), SinEaseIn(vec.z, end.z, t));
    }

    public static float SinEaseOut(float start, float end, float t)
    {
        return (end - start) * Mathf.Sin(t * (Mathf.PI / 2)) + start;
    }

    public static Vector2 SinEaseOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(SinEaseOut(vec.x, end.x, t), SinEaseOut(vec.y, end.y, t));
    }

    public static Vector3 SinEaseOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(SinEaseOut(vec.x, end.x, t), SinEaseOut(vec.y, end.y, t), SinEaseOut(vec.z, end.z, t));
    }

    public static float SinEaseInOut(float start, float end, float t)
    {
        return -(end - start) / 2 * (Mathf.Cos(Mathf.PI * t) - 1) + start;
    }

    public static Vector2 SinEaseInOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(SinEaseInOut(vec.x, end.x, t), SinEaseInOut(vec.y, end.y, t));
    }

    public static Vector3 SinEaseInOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(SinEaseInOut(vec.x, end.x, t), SinEaseInOut(vec.y, end.y, t), SinEaseInOut(vec.z, end.z, t));
    }

    //Exponential 
    public static float ExpEaseIn(float start, float end, float t)
    {
        return (end - start) * Mathf.Pow(2, 10 * (t - 1)) + start;
    }

    public static Vector2 ExpEaseIn(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(ExpEaseIn(vec.x, end.x, t), ExpEaseIn(vec.y, end.y, t));
    }

    public static Vector3 ExpEaseIn(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(ExpEaseIn(vec.x, end.x, t), ExpEaseIn(vec.y, end.y, t), ExpEaseIn(vec.z, end.z, t));
    }

    public static float ExpEaseOut(float start, float end, float t)
    {
        return (end - start) * (-Mathf.Pow(2, -10 * t) + 1) + start;
    }

    public static Vector2 ExpEaseOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(ExpEaseOut(vec.x, end.x, t), ExpEaseOut(vec.y, end.y, t));
    }

    public static Vector3 ExpEaseOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(ExpEaseOut(vec.x, end.x, t), ExpEaseOut(vec.y, end.y, t), ExpEaseOut(vec.z, end.z, t));
    }

    public static float ExpEaseInOut(float start, float end, float t)
    {
        t *= 2;
        if (t < 1)
            return (end - start) / 2 * Mathf.Pow(2, 10 * (t - 1)) + start;
        t--;
        return (end - start) / 2 * (-Mathf.Pow(2, -10 * t) + 2) + start;
    }

    public static Vector2 ExpEaseInOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(ExpEaseInOut(vec.x, end.x, t), ExpEaseInOut(vec.y, end.y, t));
    }

    public static Vector3 ExpEaseInOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(ExpEaseInOut(vec.x, end.x, t), ExpEaseInOut(vec.y, end.y, t), ExpEaseInOut(vec.z, end.z, t));
    }

    //Circular 
    public static float CircEaseIn(float start, float end, float t)
    {
        return -(end - start) * (Mathf.Sqrt(1 - t * t) - 1) + start;
    }

    public static Vector2 CircEaseIn(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(CircEaseIn(vec.x, end.x, t), CircEaseIn(vec.y, end.y, t));
    }

    public static Vector3 CircEaseIn(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(CircEaseIn(vec.x, end.x, t), CircEaseIn(vec.y, end.y, t), CircEaseIn(vec.z, end.z, t));
    }

    public static float CircEaseOut(float start, float end, float t)
    {
        t--;
        return (end - start) * Mathf.Sqrt(1 - t * t) + start;
    }

    public static Vector2 CircEaseOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(CircEaseOut(vec.x, end.x, t), CircEaseOut(vec.y, end.y, t));
    }

    public static Vector3 CircEaseOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(CircEaseOut(vec.x, end.x, t), CircEaseOut(vec.y, end.y, t), CircEaseOut(vec.z, end.z, t));
    }

    public static float CircEaseInOut(float start, float end, float t)
    {
        t *= 2;
        if (t < 1)
            return -(end - start) / 2 * (Mathf.Sqrt(1 - t * t) - 1) + start;
        t -= 2;
        return (end - start) / 2 * (Mathf.Sqrt(1 - t * t) + 1) + start;
    }

    public static Vector2 CircEaseInOut(Vector2 vec, Vector2 end, float t)
    {
        return new Vector2(CircEaseInOut(vec.x, end.x, t), CircEaseInOut(vec.y, end.y, t));
    }

    public static Vector3 CircEaseInOut(Vector3 vec, Vector3 end, float t)
    {
        return new Vector3(CircEaseInOut(vec.x, end.x, t), CircEaseInOut(vec.y, end.y, t), CircEaseInOut(vec.z, end.z, t));
    }

}