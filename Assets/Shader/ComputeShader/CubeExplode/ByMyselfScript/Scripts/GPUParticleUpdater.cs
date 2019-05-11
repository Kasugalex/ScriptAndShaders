using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kasug
{
    public class GPUParticleUpdater : MonoBehaviour
    {
        protected int dispatchID = 0;

        [SerializeField]
        protected ComputeShader shader;

        protected const int Thread = 8;

        protected const string BufferKey = "_Particles";

        protected virtual void Start() { }

        protected virtual void Update() { }

        public virtual void Dispatch(GPUParticleSystem system)
        {
            Dispatch(dispatchID,system);
        }

        protected void Dispatch(int id,GPUParticleSystem system)
        {
            shader.SetBuffer(id, BufferKey, system.ParticleBuffer);
            shader.Dispatch(id, system.ParticleBuffer.count / Thread + 1, 1, 1);
        }
    }
}
