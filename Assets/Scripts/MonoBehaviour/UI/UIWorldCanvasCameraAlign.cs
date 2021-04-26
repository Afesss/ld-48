using UnityEngine;

public class UIWorldCanvasCameraAlign : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private void Start()
    {
        if (cam != null)
            transform.rotation = cam.transform.rotation;
    }

}