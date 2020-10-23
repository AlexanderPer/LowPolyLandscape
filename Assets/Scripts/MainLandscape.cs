using UnityEngine;

public class MainLandscape : MonoBehaviour
{
    public bool shaderColorFill = false;
    public int size = 150;
    public float maxHeight = 10f;
    public float fieldMaxHeight = 0.15f;

    public float waterHeightPerc = 0.01f;
    public float coastHeightPerc = 0.1f;
    public float grassHeightPerc = 0.5f;
    public float rockHeightPerc = 0.7f;

    public float mainScaleFBM = 25f;
    public Vector2 offsetMainFBM = new Vector2(171, 13);

    public float fieldScaleFBM = 25f;
    public Vector2 offsetFieldFBM = new Vector2(101, 113);

    private int numPatches = 4;
    private TerrainPatch[] patchArr;
    void Start()
    {
        int patchSize = size / 2;
        float halfSize = patchSize / 2.0f;

        Surfacer surfacer = Surfacer.GetInstance();
        surfacer.InitSurface(size, maxHeight, fieldMaxHeight);
        surfacer.InitAllFBM(mainScaleFBM, fieldScaleFBM, offsetMainFBM, offsetFieldFBM);

        SurfDecorator.shaderColorFill = shaderColorFill;
        SurfDecorator.MaxHeight = maxHeight;
        SurfDecorator.WaterHeightPerc = waterHeightPerc;
        SurfDecorator.CoastHeightPerc = coastHeightPerc;
        SurfDecorator.GrassHeightPerc = grassHeightPerc;
        SurfDecorator.RockHeightPerc = rockHeightPerc;
        SurfDecorator.InitMaterial();

        patchArr = new TerrainPatch[numPatches];
        patchArr[0] = new TerrainPatch(gameObject.transform, -halfSize, -halfSize, patchSize);
        patchArr[1] = new TerrainPatch(gameObject.transform, -halfSize, halfSize, patchSize);
        patchArr[2] = new TerrainPatch(gameObject.transform, halfSize, -halfSize, patchSize);
        patchArr[3] = new TerrainPatch(gameObject.transform, halfSize, halfSize, patchSize); 
    }    
}
