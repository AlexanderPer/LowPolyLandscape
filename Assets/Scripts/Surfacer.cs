using UnityEngine;

public class Surfacer
{
    public float maxHeight = 10f;
    public float fieldMaxHeight = 0.15f;
    private FBM mainfBM;
    private FBM fieldfBM;
    private static Surfacer surfacerInstance;
    private float size = 10;
    public static Surfacer GetInstance()
    {
        if (surfacerInstance == null)
            surfacerInstance = new Surfacer();

        return surfacerInstance;
    }

    public void InitSurface(float size, float maxHeight, float fieldMaxHeight)
    {
        this.size = size;
        this.maxHeight = maxHeight;
        this.fieldMaxHeight = fieldMaxHeight;
    }
    public void InitMainFBM(int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        mainfBM = new FBM(seed, scale, octaves, persistence, lacunarity, offset);
    }
    public void InitFieldFBM(int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        fieldfBM = new FBM(seed, scale, octaves, persistence, lacunarity, offset);
    }

    public void InitAllFBM(float mainScale, float fieldScale, Vector2 mainOffset, Vector2 fieldOffset)
    {
        mainfBM.scale = mainScale;
        mainfBM.offset = mainOffset;
        fieldfBM.scale = fieldScale;
        fieldfBM.offset = fieldOffset;
        mainfBM.Init();
        fieldfBM.Init();
    }
    private Surfacer()
    {
        InitMainFBM(0, 25f, 4, .5f, 2f, new Vector2(71, 13));
        InitFieldFBM(21, 25f, 2, .5f, 2f, new Vector2(101, 113));
    }
    
    public float GetHeight(float x, float z)
    {
        float fieldHeight = 0.8f * fieldfBM.GetValue(x, z) + 0.2f;
        fieldHeight = Mathf.Clamp(fieldHeight, -1, fieldMaxHeight);
        float mainHeight = 0.8f * mainfBM.GetValue(x, z) + 0.2f;
        float resHeight = Mathf.Max(fieldHeight, mainHeight) + EdgeSlope(x, z);
        resHeight = resHeight < 0.0f ? 0.0f : resHeight;
        
        return resHeight * maxHeight;
    }

    private float EdgeSlope(float x, float z)
    {
        float xNorm = 2 * x / size;
        float zNorm = 2 * z / size;

        float value = Mathf.Sqrt(xNorm*xNorm + zNorm*zNorm);
        return -Mathf.Pow(value, 8);
    }    
}
