using UnityEngine;

public class ExplorerMotor : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotSpeed = 5f;

    public float minX = -360F;
    public float maxX = 360F;

    public float minY = -60F;
    public float maxY = 60F;

    float rotY = 0F;
    void FixedUpdate()
    {
        Vector3 forward = Vector3.Cross(transform.right, Vector3.up).normalized;
        transform.position += (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * forward) * speed * Time.fixedDeltaTime;

        if (Input.GetMouseButton(1))
        {            
            float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotSpeed;

            rotY += Input.GetAxis("Mouse Y") * rotSpeed;
            rotY = Mathf.Clamp(rotY, minY, maxY);

            transform.localEulerAngles = new Vector3(-rotY, rotX, 0);
        }
    }
}
