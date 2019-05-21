// 相机震动效果
using UnityEngine;

public class CameraShakeEffect : MonoBehaviour
{

    /*public Vector3 shakeFactor = Vector3.one;

    public float shakeTime = 1.0f;
    public float recoverSpeed = 1.0f;
    private float currentTime = 0.0f;
    private float totalTime = 0.0f;

    private Vector3 oriPos;
    private Transform trans;

    private void Awake()
    {
        trans = transform;
    }

    void OnEnable()
    {
        oriPos = trans.position;
        Trigger();
    }

    public void Trigger()
    {
        totalTime = shakeTime;
        currentTime = shakeTime;
    }

    public void UpdateShake()
    {
        if (currentTime > 0.0f && totalTime > 0.0f)
        {
            float percent = currentTime / totalTime;

            Vector3 shakePos = Vector3.zero;
            shakePos.x = Random.Range(-Mathf.Abs(shakeFactor.x) * percent, Mathf.Abs(shakeFactor.x) * percent);
            shakePos.y = Random.Range(-Mathf.Abs(shakeFactor.y) * percent, Mathf.Abs(shakeFactor.y) * percent);
            shakePos.z = Random.Range(-Mathf.Abs(shakeFactor.z) * percent, Mathf.Abs(shakeFactor.z) * percent);

            trans.position += shakePos;

            currentTime -= Time.deltaTime;
        }
        else
        {
            trans.position = Vector3.Lerp(trans.position, oriPos, Time.deltaTime * recoverSpeed);
            if(trans.position.sqrMagnitude - oriPos.sqrMagnitude <= 0.1f)
            {
                currentTime = 0.0f;
                totalTime = 0.0f;
                this.enabled = false;
            }
        }
    }

    void LateUpdate()
    {
        UpdateShake();
    }

    */
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