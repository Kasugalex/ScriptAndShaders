// 相机震动效果
using UnityEngine;

public class CameraShakeEffect : MonoBehaviour
{
    public float shakeLevel = 3f;
    public float setShakeTime = 0.5f;   
    public float shakeFps = 60f;   
    public float slerpFactor = 4; //给一个2的幂次方就行
    private bool isshakeCamera = false;
    private float fps;
    private float shakeTime = 0.0f;
    private float frameTime = 0.0f;
    private float shakeDelta = 0.005f;
    private Camera selfCamera;

    void OnEnable()
    {
        isshakeCamera = true;
        selfCamera = gameObject.GetComponent<Camera>();
        shakeTime = setShakeTime;
        fps = shakeFps;
        frameTime = 0.03f;
        shakeDelta = 0.005f;
    }

    void OnDisable()
    {
        selfCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        isshakeCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isshakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    enabled = false;
                }
                else
                {
                    frameTime += Time.deltaTime;

                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        float factor = shakeTime / slerpFactor;
                        selfCamera.rect = new Rect(shakeDelta * (-1.0f + shakeLevel * Random.value * factor), shakeDelta * (-1.0f + shakeLevel * Random.value * factor), 1.0f, 1.0f);
                    }
                }
            }
        }
    }
}