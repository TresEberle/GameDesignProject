using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public static CameraShake instance { get; private set; }

    [SerializeField] private CinemachineBasicMultiChannelPerlin noice;

    private float startingIntensity;
    private float shakeTimer;
    private float shakeTimerTotal;


        private void Awake()
        {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else 
        {
            instance = this;        
        }
        }
 
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            noice.AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - shakeTimer / shakeTimerTotal);
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        noice.AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }
}