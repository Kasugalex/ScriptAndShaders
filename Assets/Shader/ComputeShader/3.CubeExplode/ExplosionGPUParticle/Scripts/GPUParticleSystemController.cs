using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Kasug
{

    [System.Serializable]
    public class GPUParticleUpdaterGroup
    {
        public string name;
        public List<GPUParticleUpdater> updaters;

        public void Activete()
        {
            updaters.ForEach(updater => 
            {
                updater.gameObject.SetActive(true);
            });
        }

        public void Deactivete()
        {
            updaters.ForEach(updater =>
            {
                updater.gameObject.SetActive(false);
            });
        }
    }

    public class GPUParticleSystemController : MonoBehaviour
    {
        [SerializeField]
        GPUParticleSystem system;

        public List<GPUParticleUpdaterGroup> groups;

        private GPUParticleUpdaterGroup currentGroup;
        private void Start()
        {
            if(groups.Count > 0)
            {
                currentGroup = groups.First();
                currentGroup.Activete();
            }
        }

    }
}