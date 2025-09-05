using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;

public static class BoundPhysics
{
    [BurstCompile]
    private struct OverlapShpereJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Bounds> Bounds;
        [ReadOnly] public Vector3 Center;
        [ReadOnly] public float Radius;

        public NativeArray<int> Results;
        public NativeArray<float> Distance;

        public void Execute(int index)
        {
            Bounds bound = Bounds[index];
            Vector3 closeset = bound.ClosestPoint(Center);
            float distSq = Vector3.SqrMagnitude(Center - closeset);
            Results[index] = distSq <= Radius ? 1 : 0;
            Distance[index] = distSq;
        }
    }

    [BurstCompile]
    private struct OverlapBoundJob : IJobParallelFor
    {
        [ReadOnly] public Bounds Owner;
        [ReadOnly] public NativeArray<Bounds> Targets;

        public NativeArray<int> Results;
        public NativeArray<float> Distance;

        public void Execute(int index)
        {
            Bounds target = Targets[index];
            Results[index] = CollisionBounds(target) == true ? 1 : 0;
            Distance[index] = Vector3.SqrMagnitude(Owner.center - target.center);
        }

        public bool CollisionBounds(Bounds target)
        {
            return (Owner.min.x <= target.min.x) && (Owner.max.x >= target.min.x) &&
                   (Owner.min.y <= target.min.y) && (Owner.max.y >= target.min.y) &&
                   (Owner.min.z <= target.min.z) && (Owner.max.z >= target.min.z);
        }
    }

    public static List<BoundObject> OverlapBounds(BoundObject boundObject, LayerMask layerMask)
    {
        List<BoundObject> all = Managers.Object.Bounds;
        NativeArray<Bounds> targets = new NativeArray<Bounds>(all.Count, Allocator.TempJob);
        NativeArray<int> results = new NativeArray<int>(all.Count, Allocator.TempJob);
        NativeArray<float> distance = new NativeArray<float>(all.Count, Allocator.TempJob);

        OverlapBoundJob job = new OverlapBoundJob
        {
            Owner = boundObject.Bounds,
            Targets = targets,
            Results = results,
            Distance = distance,
        };

        JobHandle handle = job.Schedule(all.Count, 64);
        handle.Complete();

        List<BoundObject> objects = new List<BoundObject>();
        objects = Enumerable.Range(0, all.Count)
                            .Where(index => results[index] == 1)
                            .Where(index => all[index].LayerMask == layerMask)
                            .Select(index => all[index]).ToList();

        return objects;
    }
 
    public static List<BoundObject> OverlapSphere(Vector3 center, float radius, LayerMask layerMask)
    {
        List<BoundObject> all = Managers.Object.Bounds;
        NativeArray<Bounds> bounds = new NativeArray<Bounds>(all.Count, Allocator.TempJob);
        NativeArray<int> results = new NativeArray<int>(all.Count, Allocator.TempJob);
        NativeArray<float> distance = new NativeArray<float>(all.Count, Allocator.TempJob);

        OverlapShpereJob job = new OverlapShpereJob
        {
            Bounds = bounds,
            Distance = distance,
            Results = results,
            Center = center,
            Radius = radius * radius,
        };

        JobHandle handle = job.Schedule(all.Count, 64);
        handle.Complete();

        List<BoundObject> objects = new List<BoundObject>();
        objects = Enumerable.Range(0, all.Count)
                            .Where(index => results[index] == 1)
                            .Where(index => all[index].LayerMask == layerMask)
                            .Select(index => all[index]).ToList();

        return objects;
    }
}