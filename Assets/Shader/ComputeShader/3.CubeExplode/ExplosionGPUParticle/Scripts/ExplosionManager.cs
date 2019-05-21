using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public static ExplosionManager Instance;
    public bool explode = false;
    public float ShootSpeed = 10;
    public float maxZ = -4.35f;
    private Transform bullet;
    private bool shoot = false;
    private CameraShakeEffect shake;
    private void Awake()
    {
        Instance = this;
        shake = GetComponent<CameraShakeEffect>();
    }

    void Start()
    {
        bullet = GameObject.Find("Bullet").transform;
        bullet.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot = true;
            bullet.gameObject.SetActive(true);
        }

        if (shoot)
        {
            bullet.position += Vector3.forward * ShootSpeed * Time.deltaTime;
            if(bullet.position.z >= maxZ)
            {
                shoot = false;
                OnBulletColliding();
            }
        }
    }

    public void OnBulletColliding()
    {
        bullet.gameObject.SetActive(false);
        
        explode = true;

        shake.enabled = true;
    }
}
