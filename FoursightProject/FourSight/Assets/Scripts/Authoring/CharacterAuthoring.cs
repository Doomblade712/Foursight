using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using System.Linq.Expressions;
using System.Xml.Serialization;
using Unity.Transforms;
using Unity.Physics;
using Unity.Burst;
using Unity.Rendering;


namespace FoursightProductions
{
    public struct InitializeCharacterFlag : IComponentData, IEnableableComponent { }
    public struct CharacterMoveDirection : IComponentData
    {
        public float2 Value;
    }

    public struct CharacterMoveSpeed : IComponentData
    {
        public float Value;
    }

    [MaterialProperty("_FacingDirection")]
    public struct FacingDirectionOverride : IComponentData
    {
        public float Value;
    }



    public class CharacterAuthoring : MonoBehaviour
    {
        public float MoveSpeed;
        private class Baker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<InitializeCharacterFlag>(entity);
                AddComponent<CharacterMoveDirection>(entity);
                AddComponent(entity, new CharacterMoveSpeed
                {
                    Value = authoring.MoveSpeed
                });
                AddComponent(entity, new FacingDirectionOverride
                {
                    Value = 1
                });
            }
        }
    }

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CharacterInitializationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (mass, shouldInitialize) in SystemAPI.Query<RefRW<PhysicsMass>, EnabledRefRW<InitializeCharacterFlag>>())
            {
                mass.ValueRW.InverseInertia = float3.zero;
                shouldInitialize.ValueRW = false;
            }
        }
    }

    public partial struct CharacterMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (velocity, facingDirection, direction, speed) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRW<FacingDirectionOverride>,CharacterMoveDirection, CharacterMoveSpeed>())
            {
                var moveStep2d = direction.Value * speed.Value;
                velocity.ValueRW.Linear = new float3(moveStep2d, 0f);

                if(math.abs(moveStep2d.x) > 0.15f)
                {
                    facingDirection.ValueRW.Value = math.sign(moveStep2d.x);
                }
            }
        }
    }


}
