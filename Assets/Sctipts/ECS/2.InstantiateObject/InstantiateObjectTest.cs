using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Rendering;
public class InstantiateObjectTest : MonoBehaviour
{

    public Material material;
    public Mesh mesh;
    public GameObject ballPrefab;
    public int spawnCount = 5000;

    void Start()
    {
        InstantiateAll();
    }
    void InstantiateAll(GameObject prefab = null)
    {
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();
        var entities = new NativeArray<Entity>(spawnCount, Allocator.Temp);

        if (prefab)
        {
            entityManager.Instantiate(prefab, entities);
        }
        else
        {
            var archeType = entityManager.CreateArchetype(typeof(GravityComponentData), typeof(Position), typeof(RenderMesh));
            entityManager.CreateEntity(archeType, entities);
        }

        var meshRenderer = new RenderMesh()
        {
            mesh = mesh,
            material = material
        };

        for (int i = 0; i < entities.Length; i++)
        {
            Vector3 pos = UnityEngine.Random.insideUnitSphere * 40;

            pos.y = GravityJobSystem.Top;
            var entity = entities[i];
            entityManager.SetComponentData(entity, new Position { Value = pos });
            entityManager.SetComponentData(entity, new GravityComponentData { mass = Random.Range(0.5f, 3f), delay = 0.02f * i });
            entityManager.SetSharedComponentData(entity, meshRenderer);
        }

        entities.Dispose();
    }
        

}
