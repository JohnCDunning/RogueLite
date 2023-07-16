using Roguelite.AI.Components;
using Roguelite.Player.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Roguelite.AI.Systems
{
    public partial struct AIBehaviourISystem : ISystem
    {
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
                             in SystemAPI.Query<RefRW<LocalTransform>, RefRO<AIComponent>>().WithAll<AIComponent>())
                    {
                        float3 collisionRes = CalculateSeparationVector(transform.ValueRO.Position,
                            aiSeek.ValueRO.Size,
                            otherTransform.ValueRO.Position,
                            otherAI.ValueRO.Size);
                       
                        Debug.Log(collisionRes);
                        collisionRes = new float3(collisionRes.x, 0f, collisionRes.z);
                        if (math.length(collisionRes) > 0)
                        {
                            transform.ValueRW.Position -= (collisionRes / 2f);
                            otherTransform.ValueRW.Position += (collisionRes / 2f);
                        }
                    }
                }
            }
        }

        
        public static float3 CalculateSeparationVector(float3 sphere1Position, float sphere1Radius, float3 sphere2Position, float sphere2Radius)
        {
            float3 separationVector = sphere2Position - sphere1Position;
            float distance = math.length(separationVector);

            float sumOfRadii = sphere1Radius/2f + sphere2Radius/2f;

            if (distance <= sumOfRadii)
            {
                float3 separationDirection = math.normalize(separationVector);
                float separationDistance = sumOfRadii - distance;
                float3 separation = separationDirection * separationDistance;

                return separation;
            }

            return float3.zero;
        }
    }
}
