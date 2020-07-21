using System;
using UnityEngine;

namespace FirstPersonShooter
{
    public sealed class DestroyPoint : MonoBehaviour
    {
        public event Action<GameObject> OnFinishChanged = delegate(GameObject o) { };

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Bot>())
            {
               OnFinishChanged.Invoke(gameObject);
            }
        }
    }
}