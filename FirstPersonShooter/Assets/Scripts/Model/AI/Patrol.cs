using UnityEngine;
using UnityEngine.AI;

namespace FirstPersonShooter
{
    public static class Patrol
    {
        public static Vector3 GenericRandomGenericPoint(Transform agent)
        {
            Vector3 result;

            int dis = Random.Range(5, 50);
            Vector3 randomPoint = Random.insideUnitSphere * dis;

            NavMesh.SamplePosition(agent.position + randomPoint, out var hit, dis, NavMesh.AllAreas);

            result = hit.position;
            return result;
        }
    }
}