using UnityEngine;
using Random = UnityEngine.Random;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Kasug
{
    public class ExplodeUpdater : GPUParticleUpdater
    {

        [System.Serializable]
        struct Explosion
        {
            public Vector3 position;
            public float radius;
            public float intensity;
            public Explosion(Vector3 p, float r, float i)
            {
                position = p;
                radius = r;
                intensity = i;
            }
        };

        [SerializeField]
        List<Explosion> explosions;

        public float radius = 1f;
        public float size = 0.5f;
        public float intensity = 17.5f;

        public bool automatic = false;
        [Range(0.01f, 1f)]
        public float automaticDuration = 0.1f;

        ComputeBuffer explosionBuffer;

        protected override void Start()
        {
            base.Start();
        }

        IEnumerator Repeater()
        {
            yield return 0;

            while (true)
            {
                yield return new WaitForSeconds(automaticDuration);
                Explode();
            }
        }

        protected override void Update()
        {
            CheckInit();
            base.Update();

            if (Input.GetMouseButtonDown(0))
            {

                if (show)
                    StartCoroutine(Repeater());
                show = false;
            }
        }

        public override void Dispatch(GPUParticleSystem system)
        {
            CheckInit();

            int count = explosions.Count;
            if (count > 0)
            {
                explosionBuffer.SetData(explosions.ToArray());
                shader.SetBuffer(0, "_Explosions", explosionBuffer);
                shader.SetInt("_ExplosionsCount", count);
                base.Dispatch(system);

                explosions.Clear();
            }
        }

        public void Explode()
        {
            Explode(Random.insideUnitSphere * radius, size, intensity);
        }

        public void Explode(Vector3 p, float radius, float intensity)
        {
            var exp = new Explosion(p, radius, intensity);
            explosions.Add(exp);
        }

        void CheckInit()
        {
            if (explosionBuffer == null)
            {
                explosionBuffer = new ComputeBuffer(32, Marshal.SizeOf(typeof(Explosion)));
                explosionBuffer.SetData(explosions);
            }
        }

        void OnEnable()
        {
            if (automatic)
            {
                StartCoroutine(Repeater());
            }
        }

        void OnDisable()
        {
            if (explosionBuffer != null)
            {
                explosionBuffer.Release();
                explosionBuffer = null;
            }
        }

        bool show = true;
        private void OnGUI()
        {
            if (!show) return;
            GUIStyle style = new GUIStyle();
            style.fontSize = 40;
            style.normal.textColor = Color.white;
            GUI.TextArea(new Rect((Screen.width >> 1) - 130, 100, 500, 200), "按下左键爆炸", style);
        }

    }

}