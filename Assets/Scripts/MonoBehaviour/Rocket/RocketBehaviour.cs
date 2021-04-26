using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class RocketBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flyClip;
    [SerializeField] private AudioClip explosionRocketClip;
    [SerializeField] private Animation platformAnim;
    [SerializeField] private Animation rocketAnim;
    [SerializeField] private Transform rocket;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem dirtyRain;
    [SerializeField] private float flySpeed;
    [SerializeField] private float flyDistanceToExplosion;
    [SerializeField] private AnimationCurve curve;

    private float startYPosition;
    private SignalBus signalBus;
    #endregion
    [Inject]
    private void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }
    #region Methods
    private void Awake()
    {
        explosion.Stop();
        dirtyRain.Stop();
        audioSource.Stop();
        audioSource.playOnAwake = false;
    }

    public void RocketCanFly()
    {
        StartCoroutine(RocetStart());
    }
    public void RocketLauncher()
    {
        startYPosition = rocket.position.y;
        audioSource.clip = flyClip;
        audioSource.Play();
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
                audioSource.Stop();
                audioSource.clip = explosionRocketClip;
                audioSource.Play();
                explosion.Play();
                dirtyRain.Play();
                Destroy(rocket.gameObject);

                signalBus.Fire(new RocketSignal() { gameOverVersion = UIGameOver.GameOverVersion.Rocket });
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
