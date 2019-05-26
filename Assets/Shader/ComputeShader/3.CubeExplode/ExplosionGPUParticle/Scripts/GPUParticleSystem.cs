﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Runtime.InteropServices;

namespace Kasug
{
    [System.Serializable]
    public struct GPUParticle
    {
        public float mass;
        public float lifetime; // 0.0 ~ 1.0
        public Vector3 origin;
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 scale;
        public Vector3 vel;
        public Vector3 acc;
        public Color color;
        public int reactive;

        public GPUParticle(float m, Vector3 p, Quaternion r, Vector3 s, Vector3 v, Vector3 a, Color c)
        {
            mass = m;
            lifetime = 0.5f;
            origin = pos = p;
            rot = r;
            scale = s;
            vel = v;
            acc = a;
            color = c;
            reactive = 0;
        }
    };

    public class GPUParticleSystem : MonoBehaviour
    {

        public ComputeBuffer ParticleBuffer { get { return buffer; } }
        public MaterialPropertyBlock block
        {
            get
            {
                if (_block == null)
                {
                    _block = new MaterialPropertyBlock();
                }
                return _block;
            }
        }
        public List<GPUParticleUpdater> updaters;
        [SerializeField] Shader shader;
        [SerializeField] Color color = Color.white;
        [SerializeField] int vertexCount = 30000;
        [SerializeField] ComputeShader updateShader;
        [SerializeField] Material particleDisplayMat;
        [SerializeField, Range(0.1f, 1f)] float deceleration = 0.98f;

        Mesh mesh;
        GPUParticle[] particles;
        ComputeBuffer buffer;
        MaterialPropertyBlock _block;
        Transform _trans;

        const int _Thread = 8;

        protected void Start()
        {
            _trans = transform;
            var sideCount = Mathf.FloorToInt(Mathf.Pow(vertexCount, 1f / 3f));
            var count = sideCount * sideCount * sideCount;
            var dsideCount = sideCount * sideCount;
            particles = new GPUParticle[count];

            var scale = (1f / sideCount);
            var offset = -Vector3.one * 0.5f;

            for (int x = 0; x < sideCount; x++)
            {
                var xoffset = x * dsideCount;
                for (int y = 0; y < sideCount; y++)
                {
                    var yoffset = y * sideCount;
                    for (int z = 0; z < sideCount; z++)
                    {
                        var index = xoffset + yoffset + z;
                        var particle = new GPUParticle(Random.Range(0.5f, 1f), new Vector3(x, y, z) * scale + offset, Quaternion.identity, Vector3.one, Vector3.zero, Vector3.zero, Color.white);
                        particles[index] = particle;
                    }
                }
            }

            block.SetFloat("_Size", scale);

            mesh = Build(sideCount);
        }

        private int colorH = 0;
        void Update()
        {
            CheckInit();

            updaters.ForEach(updater =>
            {
                if (updater.gameObject.activeSelf)
                {
                    updater.Dispatch(this);
                }
            });

            float t = Time.timeSinceLevelLoad;
            updateShader.SetVector("_Time", new Vector4(t / 20f, t, t * 2f, t * 3f));
            updateShader.SetFloat("_DT", Time.deltaTime);
            updateShader.SetFloat("_Deceleration", deceleration);

            Dispatch("Update");

            block.SetBuffer("_Particles", buffer);

            colorH = colorH < 360 ? ++colorH : 0;

            color = HSVToRGB.HSV2RGB(colorH, 200, 200);

            block.SetColor("_Color", color);

            Graphics.DrawMesh(mesh, _trans.localToWorldMatrix, particleDisplayMat, 0, null, 0, block);
        }

        void Dispatch(string key)
        {
            int kernel = updateShader.FindKernel(key);
            if (kernel < 0)
            {
            }
            updateShader.SetBuffer(kernel, "_Particles", buffer);
            updateShader.Dispatch(kernel, buffer.count / _Thread + 1, 1, 1);
        }

        Mesh Build(int count)
        {
            Mesh particleMesh = new Mesh();
            particleMesh.name = count.ToString();

            var dcount = count * count;
            var tcount = dcount * count;

            var scale = (1f / count);
            var dscale = scale * scale;
            var offset = -Vector3.one * 0.5f;

            var vertices = new Vector3[tcount];
            var uvs = new Vector2[tcount];
            var indices = new int[tcount];
            /*
            for (int x = 0; x < count; x++)
            {
                var xoffset = x * dcount;
                for (int y = 0; y < count; y++)
                {
                    var yoffset = y * count;
                    for (int z = 0; z < count; z++)
                    {
                        var index = xoffset + yoffset + z;
                        vertices[index] = new Vector3(x, y, z) * scale + offset;
                        uvs[index] = new Vector2(x * scale + z * dscale, y * scale);
                        indices[index] = index;
                    }
                }
            }
            */

            for (int x = 0; x < count; x++)
            {
                var xoffset = x * dcount;
                for (int y = 0; y < count; y++)
                {
                    var yoffset = y * count;

                    var index = xoffset + yoffset ;
                    vertices[index] = new Vector3(x, y, 0) * scale + offset;
                    uvs[index] = new Vector2(x * scale, y * scale);
                    indices[index] = index;
                }
            }

            particleMesh.vertices = vertices;
            //particleMesh.uv = uvs;

            particleMesh.SetIndices(indices, MeshTopology.Points, 0);
            //particleMesh.RecalculateBounds();
            //var bounds = particleMesh.bounds;
            //bounds.size = bounds.size * 100f;
            //particleMesh.bounds = bounds;

            return particleMesh;
        }

        void CheckInit()
        {
            if (buffer == null)
            {
                buffer = new ComputeBuffer(particles.Length, Marshal.SizeOf(typeof(GPUParticle)));
                buffer.SetData(particles);
            }
        }

        void OnDisable()
        {
            if (buffer != null)
            {
                buffer.Release();
                buffer = null;
            }
        }

    }
}