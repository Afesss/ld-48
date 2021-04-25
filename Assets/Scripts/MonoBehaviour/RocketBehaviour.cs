using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class RocketBehaviour : MonoBehaviour
{
    [SerializeField] private Animation platformAnim;
    [SerializeField] private Animation rocketAnim;
    [SerializeField] private Transform rocket;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem dirtyRain;
    [SerializeField] private float flySpeed;
    [SerializeField] private float flyDistanceToExplosion;
    [SerializeField] private AnimationCurve curve;

    private float startYPosition;

    private Settings settings;
    private Money money;

    [Inject]
    private void Construct(Settings settings,Money money)
    {
        this.settings = settings;
        this.money = money;
    }
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
        if(money.moneyAmount < settings.rocketCost)
        {
            //TODO: Delete in production.
            Debug.Log("Недостаточно денег");
            return;
        }
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
    [Serializable]
    public struct Settings
    {
        [Tooltip("Стоимость запуска ракеты")]
        public int rocketCost;
    }
}
