using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class RocketBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] private Animation platformAnim;
    [SerializeField] private Animation rocketAnim;
    [SerializeField] private Transform rocket;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem dirtyRain;
    [SerializeField] private float flySpeed;
    [SerializeField] private float flyDistanceToExplosion;
    [SerializeField] private AnimationCurve curve;

    private float startYPosition;
    #endregion


    #region Methods
    private void Awake()
    {
        explosion.Stop();
        dirtyRain.Stop();
    }
    public void RocketCanFly()
    {
        StartCoroutine(RocetStart());
    }
    //TODO: Delete when added UI.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RocketLauncher();
        }
    }
    public void RocketLauncher()
    {
        startYPosition = rocket.position.y;
        StartCoroutine(WaitSecondToStartRocket());
    }

    private IEnumerator RocetStart()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            rocketAnim.Stop();
            float explodePosition = startYPosition + flyDistanceToExplosion;
            float timer = rocket.transform.position.y / explodePosition;

            rocket.transform.position += Vector3.up * flySpeed * curve.Evaluate(timer) * Time.fixedDeltaTime;

            if (rocket.transform.position.y > explodePosition)
            {
                StopAllCoroutines();
                explosion.Play();
                dirtyRain.Play();
                Destroy(rocket.gameObject);
            }
        }
    }
    private IEnumerator WaitSecondToStartRocket()
    {
        rocketAnim.Play();
        yield return new WaitForSeconds(1);
        platformAnim.Play();
    }
    #endregion
}
