using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Physics;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class BallMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        float2 curInput = new float2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Entities.ForEach((ref PhysicsVelocity vel, in SpeedData speedData) =>
        {
            float2 newVel = vel.Linear.xz;

            newVel += curInput * speedData.speed * deltaTime;

            vel.Linear.xz = newVel;
        }).Run();

        return default;
    }
}
