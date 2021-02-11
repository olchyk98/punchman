using UnityEngine;

public class Shake : MonoBehaviour
{
    public Vector3 maximumTranslationShake = Vector3.one * 0.5f;
    public float frequency = 25;
    private float myTrauma;
    private float mySeed;

    /// <summary>
    /// Sets the seed for perlin noise
    /// </summary>
    private void Awake()
    {
        mySeed = Random.value;
    }

    /// <summary>
    /// Runs the camera shake when trauma is above 0.
    /// </summary>
    void Update()
    {
        CameraShake(0.5f);
    }

    private void CameraShake(float aRecoverySpeed)
    {
        if (myTrauma == 0)
            return;

        var shakeAmount = maximumTranslationShake.x * (myTrauma * myTrauma) / 100;
        var x = Mathf.PerlinNoise(mySeed, Time.time * frequency) * 2 - 1;
        var y = Mathf.PerlinNoise(mySeed + 1, Time.time * frequency) * 2 - 1;

        transform.localPosition = new Vector2(
                x * shakeAmount,
                y * shakeAmount);

        myTrauma = Mathf.Clamp(myTrauma - aRecoverySpeed * Time.deltaTime, 0, 10);
    }

    /// <summary>
    /// Adds trauma and makes sure it gets clamped in a way that makes sense.
    /// </summary>
    /// <param name="aTrauma">The amount of trauma you'd like to add</param>
    public void AddTrauma(float aTrauma)
    {
        myTrauma = Mathf.Clamp(myTrauma + aTrauma, 0, 10);
    }
}
