using UnityEngine;
public class FBM
{
    public int octaves;
    public int seed;
    public float lacunarity, persistence, scale;
    public Vector2 offset;
    Vector2[] octaveOffsets;
    public FBM (int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        this.seed = seed;
        this.scale = scale;
        this.octaves = octaves;
        this.persistence = persistence;        
        this.lacunarity = lacunarity;
        this.offset = offset;

        octaveOffsets = new Vector2[octaves];
        Init();
    }

    public void Init()
    {
        System.Random prng = new System.Random(seed);
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }
    }

    public float GetValue(float x, float y)
    {
        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;

        for (int i = 0; i < octaves; i++)
        {
            float sampleX = x / scale * frequency + octaveOffsets[i].x;
            float sampleY = y / scale * frequency + octaveOffsets[i].y;

            float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
            noiseHeight += perlinValue * amplitude;

            amplitude *= persistence;
            frequency *= lacunarity;
        }

        return noiseHeight;
    }
}
