using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Roguelite.AI.Components
{
    public class AISeekComponentAuthoring : MonoBehaviour
    {
    }
    public class AISeekComponentBaker : Baker<AISeekComponentAuthoring>
    {
        public override void Bake(AISeekComponentAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new AISeekTagComponent());

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