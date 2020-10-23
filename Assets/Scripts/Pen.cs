using UnityEngine;

public class Pen : MonoBehaviour
{
    public static float Time = float.MaxValue;
    public static float Width = 0.5f;
    public static Color LineColor = Color.black;

    private TrailRenderer trailer;      

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (Physics.Raycast(ray, out info))
        {
            GameObject trailerObject = new GameObject("Trailer");
            trailer = trailerObject.AddComponent<TrailRenderer>();
            trailer.transform.position = info.point;
            trailer.time = Time;
            trailer.startWidth = Width;
            trailer.endWidth = Width;
            trailer.material.color = LineColor;
        }
    }

    private void OnMouseDrag()
    {
        if (trailer)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit info;
            if (Physics.Raycast(ray, out info))
            {
                trailer.transform.position = info.point;
            }

        }
    }

    private void OnMouseUp()
    {
        trailer = null;
    }
}
