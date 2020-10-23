using UnityEngine;

public static class SurfDecorator
{
    public static bool shaderColorFill = false;
    public static float MaxHeight = 10f;

    public static float WaterHeightPerc = 0.01f;
    public static float CoastHeightPerc = 0.1f;
    public static float GrassHeightPerc = 0.5f;
    public static float RockHeightPerc = 0.7f;

    public static Color32 WaterColor = new Color32(80, 133, 188, 255);
    public static Color32 CoastColor = new Color32(202, 143, 66, 255);
    public static Color32 GrassColor = new Color32(19, 109, 21, 255);
    public static Color32 RockColor = new Color32(80, 80, 80, 255);
    public static Color32 SnowColor = new Color32(224, 255, 255, 255);

    private static Material terraMaterial;

    public static void InitMaterial()
    {
        Shader terraShader = Shader.Find("Custom/Terra");
        terraMaterial = new Material(terraShader);
        float isShading = shaderColorFill ? 1 : 0;
        terraMaterial.SetFloat("_IsShading", isShading);

        terraMaterial.SetFloat("_WaterHeight", WaterHeightPerc * MaxHeight);
        terraMaterial.SetFloat("_CoastHeight", CoastHeightPerc * MaxHeight);
        terraMaterial.SetFloat("_GrassHeight", GrassHeightPerc * MaxHeight);
        terraMaterial.SetFloat("_RockHeight", RockHeightPerc * MaxHeight);        
    }

    public static Color32 GetTriColor(float averageTriHeight)
    {
        if (averageTriHeight < WaterHeightPerc * MaxHeight)
            return WaterColor;
        if (averageTriHeight < CoastHeightPerc * MaxHeight)
            return CoastColor;
        if (averageTriHeight < GrassHeightPerc * MaxHeight)
            return GrassColor;
        if (averageTriHeight < RockHeightPerc * MaxHeight)
            return RockColor;

        return SnowColor;
    }

    public static Material GetMaterial()
    {        
        return terraMaterial;
    }
}
