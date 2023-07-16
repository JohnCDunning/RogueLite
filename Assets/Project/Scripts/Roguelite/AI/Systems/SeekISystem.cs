using Roguelite.AI.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Roguelite.AI.Systems
{
    public partial struct SeekISystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            float dt = Time.deltaTime;
            foreach (RefRW<LocalTransform> transform in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<AISeekTagComponent>())
            {
                transform.ValueRW.Position += new float3(0, 1 * dt, 0);
            }
        }
    }
}
