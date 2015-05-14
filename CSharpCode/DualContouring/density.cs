using Code.Noise;
using UnityEngine;

public static class glm
{
    public static float Sphere(Vector3 worldPosition, Vector3 origin, float radius)
    {
        return Vector3.Magnitude(worldPosition - origin) - radius;
    }

    public static float Cuboid(Vector3 worldPosition, Vector3 origin, Vector3 halfDimensions)
    {
        Vector3 local_pos = worldPosition - origin;
        Vector3 pos = local_pos;

        Vector3 d = new Vector3(Mathf.Abs(pos.x), Mathf.Abs(pos.y), Mathf.Abs(pos.z)) - halfDimensions;
        float m = Mathf.Max(d.x, Mathf.Max(d.y, d.z));
        return Mathf.Min(m, Vector3.Magnitude(d.magnitude > 0 ? d : Vector3.zero));
    }

    public static float FractalNoise(int octaves, float frequency, float lacunarity, float persistence, Vector2 position)
    {
        float SCALE = 1.0f / 128.0f;
        Vector2 p = position * SCALE;
        float noise = 0.0f;

        float amplitude = 1.0f;
        p *= frequency;

        for (int i = 0; i < octaves; i++)
        {
            noise += Noise.Perlin(p.x, p.y) * amplitude;
            p *= lacunarity;
            amplitude *= persistence;
        }

        // move into [0, 1] range
        return 0.5f + (0.5f * noise);
    }


    public static float Density_Func(Vector3 worldPosition)
    {
        float MAX_HEIGHT = 20.0f;
        float noise = FractalNoise(4, 0.5343f, 2.2324f, 0.68324f, new Vector2(worldPosition.x, worldPosition.z));
        float terrain = worldPosition.y - (MAX_HEIGHT * noise);

        float cube = Cuboid(worldPosition, new Vector3(-4.0f, 10.0f, -4.0f), new Vector3(12.0f, 12.0f, 12.0f));
        float sphere = Sphere(worldPosition, new Vector3(15.0f, 2.5f, 1.0f), 16.0f);

        return Mathf.Max(-cube, Mathf.Min(sphere, terrain));
    }
}