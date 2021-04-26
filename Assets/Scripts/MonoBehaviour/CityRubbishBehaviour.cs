using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class CityRubbishBehaviour : MonoBehaviour
{
    [SerializeField] private float maxHeight;

    private Vector3 startPosition;
    private Ecology ecology;
    [Inject]
    private void Construct(Ecology ecology)
    {
        this.ecology = ecology;
    }
    private void Awake()
    {
        startPosition = transform.position;
        ecology.OnEcologyChange += Ecology_OnEcologyChange;
    }
    private void OnDestroy()
    {
        ecology.OnEcologyChange -= Ecology_OnEcologyChange;
    }
    private void Ecology_OnEcologyChange(Ecology.Type type)
    {
        float ecologyRate = ecology.GetCurrenMaxPollutionRate();
        SetPosition(ecologyRate);
    }

    private void SetPosition(float lerpSpeed)
    {
        transform.position = Vector3.Slerp(startPosition, startPosition + Vector3.up * maxHeight,lerpSpeed);
    }
}
