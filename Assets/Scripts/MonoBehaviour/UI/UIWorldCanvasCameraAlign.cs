using UnityEngine;

public class UIWorldCanvasCameraAlign : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

}