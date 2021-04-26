using UnityEngine;

public class RocketView : MonoBehaviour
{
    [SerializeField]
    private RocketBehaviour rocket;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Build()
    {
        gameObject.SetActive(true);
    }

    public void Launch()
    {
        rocket.RocketLauncher();
    }
}