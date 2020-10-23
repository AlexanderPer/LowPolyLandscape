using UnityEngine;

public class TerrainPatch
{
    GameObject patchGO;
    MeshFilter meshFilter;

    float centerX;
    float centerZ;
    int size = 10;
    int triangleIndex;    

    public TerrainPatch(Transform parent, float centerX, float centerZ, int size)
    {
        patchGO = new GameObject("TerrainPatch");
        patchGO.transform.SetParent(parent);
        this.centerX = centerX;
        this.centerZ = centerZ;
        patchGO.transform.SetPositionAndRotation(new Vector3(centerX, 0, centerZ), Quaternion.identity);

        meshFilter = patchGO.AddComponent<MeshFilter>();
        MeshRenderer renderer = patchGO.AddComponent<MeshRenderer>();        
        renderer.material = SurfDecorator.GetMaterial();

        this.size = size;

        Regenerate();
        patchGO.AddComponent<MeshCollider>();
        patchGO.AddComponent<Pen>();
    }   

    public void Regenerate()
    {        
        float halfSize = size / 2f;
        float origin = -halfSize;

        int triNum = size * size * 6;
        Vector3[] verts = new Vector3[triNum];
        Color32[] colors = new Color32[triNum];
        int[] triangles = new int[triNum];

        int sideVertsNum = size + 1;
        int vertsNum = sideVertsNum * sideVertsNum;
        Vector3[] smoothVerts = new Vector3[vertsNum];

        Surfacer surfacer = Surfacer.GetInstance();
        int vertexIndex = 0;
        for (int z = 0; z < sideVertsNum; z++)
        {
            for (int x = 0; x < sideVertsNum; x++)
            {

                float height = surfacer.GetHeight(centerX + origin + x, centerZ + origin + z);             
                smoothVerts[vertexIndex] = new Vector3(origin + x, height, origin + z);

                if (x < size && z < size)
                {
                    AddTriangle(triangles, vertexIndex, vertexIndex + sideVertsNum, vertexIndex + sideVertsNum + 1);                    
                    AddTriangle(triangles, vertexIndex, vertexIndex + sideVertsNum + 1, vertexIndex + 1);
                }
                vertexIndex++;
            }
        }

        for (int i = 0; i < triangles.Length; i++)
        {
            verts[i] = smoothVerts[triangles[i]];
            triangles[i] = i;
        }

        for (int i = 0; i < verts.Length; i += 3)
        {
            float averageTriHeight = (verts[i].y + verts[i + 1].y + verts[i + 2].y) / 3f;
            Color triColor = SurfDecorator.GetTriColor(averageTriHeight);
            AddColor(colors, i, triColor);
        }

        InitMesh(verts, triangles, colors);
    }

    private void AddTriangle(int[] triangles, int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    private void AddColor(Color32[] colors, int index, Color32 color)
    {
        colors[index] = color;
        colors[index + 1] = color;
        colors[index + 2] = color;
    }

    private void InitMesh(Vector3[] verts, int[] triangles, Color32[] colors)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.colors32 = colors;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }
}
