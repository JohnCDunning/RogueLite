using Unity.Entities;
using Unity.Mathematics;

namespace Roguelite.Player.Components
{
    public struct PlayerComponent : IComponentData
    {
        public float3 Position;
        public float3 Velocity;
    }
}
