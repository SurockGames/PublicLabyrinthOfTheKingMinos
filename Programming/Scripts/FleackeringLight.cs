using UnityEngine;

public class FleackeringLight : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private float repeatRate;
    [SerializeField] private float minIntencity;

    private Light lightSource;
    private float initialIntencity;

    void Start()
    {
        lightSource = GetComponent<Light>();
        initialIntencity = lightSource.intensity;

        InvokeRepeating(nameof(SetRandomIntencity), repeatRate, repeatRate);
    }

    private void SetRandomIntencity()
    {
        float randOffset = Random.Range(-offset / 2, offset / 2);

        if (initialIntencity + randOffset < 0)
            lightSource.intensity = minIntencity;
        else
            lightSource.intensity = Mathf.Lerp(lightSource.intensity, initialIntencity + randOffset, repeatRate);
    }
}
