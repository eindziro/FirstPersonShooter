using UnityEngine;

namespace FirstPersonShooter
{
    [System.Serializable]
    public sealed class Vision
    {
        public float ActiveDis = 15;
        public float ActiveAng = 40;

        public bool Detect(Transform player, Transform target)
        {
            return CheckDistance(player, target) && CheckAngle(player, target) && !CheckForBlocks(player, target);
        }

        private bool CheckForBlocks(Transform player, Transform target)
        {
            if (!Physics.Linecast(player.position, target.position, out var hit)) return true;
            return hit.transform != target;
        }

        private bool CheckAngle(Transform player, Transform target)
        {
            var angle = Vector3.Angle(target.position - player.position, player.forward);
            return angle <= ActiveAng;
        }

        private bool CheckDistance(Transform player, Transform target)
        {
            return (player.position - target.position).sqrMagnitude <= ActiveDis * ActiveDis;
        }
        
    }
}