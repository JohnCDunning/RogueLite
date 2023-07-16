using Roguelite.Player.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Roguelite.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAuthoring : MonoBehaviour
    {
        private EntityManager _entityManager;
        private Entity _entity;
        private void Awake()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _entity = _entityManager.CreateEntity(typeof(PlayerComponent));
        }

        private void FixedUpdate()
        {
            _entityManager.SetComponentData(_entity, new PlayerComponent()
            {
                Position = transform.position, Velocity = float3.zero
            });
        }
    }
}
