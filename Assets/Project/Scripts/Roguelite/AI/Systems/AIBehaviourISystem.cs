using Roguelite.AI.Components;
using Roguelite.Player.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Roguelite.AI.Systems
{
    [BurstCompile]
    public partial struct AIBehaviourISystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float dt = Time.deltaTime;
            foreach (var player in SystemAPI.Query<RefRO<PlayerComponent>>().WithAll<PlayerComponent>())
            {
                foreach (var ( transform, aiSeek) 
                         in SystemAPI.Query<RefRW<LocalTransform>, RefRO<AIComponent>>().WithAll<AIComponent>())
                {
                    float3 moveDirection = player.ValueRO.Position - transform.ValueRO.Position;
                    float3 desiredVelocity = moveDirection * aiSeek.ValueRO.Speed * dt;

                    transform.ValueRW.Position += desiredVelocity;
                    
                    // Collision Test 
                    foreach (var (otherTransform, otherAI) 
                             in SystemAPI.Query<RefRO<LocalTransform>, RefRO<AIComponent>>().WithAll<AIComponent>())
                    {
                        if (CalculateSeparationVector(transform.ValueRO.Position,
                                aiSeek.ValueRO.Size,
                                otherTransform.ValueRO.Position,
                                otherAI.ValueRO.Size, out float3 collisionRes))
                        {
                            if (math.length(collisionRes) > 0f)
                                transform.ValueRW.Position -= (collisionRes * 0.5f);
                        }
                    }
                }
            }
        }

        
        public static bool CalculateSeparationVector(float3 sphere1Position, float sphere1Radius, float3 sphere2Position, float sphere2Radius, out float3 separation)
        {
            float3 separationVector = sphere2Position - sphere1Position;
            float distance = math.length(separationVector);

            float sumOfRadii = sphere1Radius/2f + sphere2Radius/2f;

            if (distance <= sumOfRadii)
            {
                float3 separationDirection = math.normalize(separationVector);
                float separationDistance = sumOfRadii - distance;
                separation = separationDirection * separationDistance;
                return true;
            }

            separation = float3.zero;
            return false;
        }
    }
}
