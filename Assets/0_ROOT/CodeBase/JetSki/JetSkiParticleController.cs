using Particles;
using UnityEngine;

namespace JetSki
{
    [RequireComponent(typeof(JetSki))]
    public class JetSkiParticleController : MonoBehaviour
    {
        [SerializeField] private JetSki m_jetSki;
        [SerializeField] private ParticleGroup m_foam;
        [SerializeField] private ParticleGroup m_splashes;
        [SerializeField] private float m_splashesSensibility = 0.3f;
        
        private bool m_splashAllowed = true;
        private bool m_inWater;

        private void Start()
        {
            m_jetSki.OnWaterEnter += () => m_inWater = true;
            m_jetSki.OnWaterExit += () => m_inWater = false;
        }

        private void LateUpdate()
        {
            if (!m_inWater)
            {
                m_foam.MultiplyEmissionRate(0.0f);
                return;
            }
            
            float speedCoefficient = m_jetSki.Rb.velocity.magnitude / m_jetSki.TargetMoveSpeed;
            float foamEmissionRate = Mathf.Clamp(speedCoefficient * speedCoefficient, 0, 1);
            m_foam.MultiplyEmissionRate(foamEmissionRate);

            if (m_jetSki.Rb.velocity.y < -m_splashesSensibility && m_splashAllowed)
            {
                m_splashes.MultiplyEmissionRate(Mathf.Abs(m_jetSki.Rb.velocity.y));
                m_splashes.Play();
                m_splashAllowed = false;
            }

            if (m_jetSki.Rb.velocity.y > m_splashesSensibility) 
                m_splashAllowed = true;
        }
    }
}
