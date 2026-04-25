using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light lightSource;

    public float minIntensity = 0.2f;
    public float maxIntensity = 0.6f;

    public float minWaitTime = 0.5f;
    public float maxWaitTime = 1.5f;

    public float blackoutChance = 0.08f;
    public float blackoutTime = 0.12f;

    private float timer;
    private float nextFlickerTime;
    private float originalIntensity;

    void Start()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        originalIntensity = lightSource.intensity;
        nextFlickerTime = Random.Range(minWaitTime, maxWaitTime);
    }

    void Update()
    {
        if (lightSource == null) return;

        timer += Time.deltaTime;

        if (timer >= nextFlickerTime)
        {
            if (Random.value < blackoutChance)
            {
                lightSource.intensity = 0f;
                Invoke(nameof(RestoreLight), blackoutTime);
            }
            else
            {
                lightSource.intensity = Random.Range(minIntensity, maxIntensity);
            }

            timer = 0f;
            nextFlickerTime = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    void RestoreLight()
    {
        lightSource.intensity = Random.Range(minIntensity, maxIntensity);
    }
}