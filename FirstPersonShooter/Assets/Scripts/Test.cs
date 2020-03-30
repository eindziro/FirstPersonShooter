using UnityEngine;

namespace FirstPersonShooter
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            FindObjectOfType<FlashLightModel>().Layer = 2;
        }
    }
}