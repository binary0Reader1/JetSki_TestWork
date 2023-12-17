using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Buoyancy))]
    public class Water : MonoBehaviour
    {
        public Buoyancy BuoyancyRef => m_buoyancy;
        [SerializeField] private Buoyancy m_buoyancy;
    }
}
