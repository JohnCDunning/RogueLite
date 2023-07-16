using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Roguelite.AI.Components
{
    public class AISeekComponentAuthoring : MonoBehaviour
    {
        public float SpeedBase = 1f;
        public float Size = 1f;
    }
    public class AISeekComponentBaker : Baker<AISeekComponentAuthoring>
    {
        public override void Bake(AISeekComponentAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new AIComponent
            {
                Size = authoring.Size,
                Speed = authoring.SpeedBase - Random.value/4f
            });

            Transform authoringTransform = authoring.transform;
            
            LocalTransform transform = new()
            {
                Position = authoringTransform.position,
                Rotation = authoringTransform.rotation,
                Scale = authoringTransform.localScale.x
            };
            AddComponent(entity, transform);
        }
        
    }
}