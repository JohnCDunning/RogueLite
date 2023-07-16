using Unity.Entities;
using Unity.Mathematics;

namespace Roguelite.AI.Components
{
    public struct AIComponent : IComponentData
    {
        public float Size;
        public float Speed;
    }
}
