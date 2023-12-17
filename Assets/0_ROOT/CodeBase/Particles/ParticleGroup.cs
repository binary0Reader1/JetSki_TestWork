using UnityEngine;

namespace Particles
{
    public class ParticleGroup : MonoBehaviour
    {
        [SerializeField] protected ParticleSystem[] Particles;
        
        private ParticleSystem.EmissionModule[] m_emissionModules;
        private float[] m_originalRates;
        
        private void Awake()
        {
            m_emissionModules = new ParticleSystem.EmissionModule[Particles.Length];
            m_originalRates = new float[Particles.Length];
            
            for (int i = 0; i < Particles.Length; i++)
            {
                m_emissionModules[i] = Particles[i].emission;
                m_originalRates[i] = m_emissionModules[i].rateOverTime.constant;
            }
        }
        
        public void MultiplyEmissionRate(float rateMultiplier)
        {
            for (int i = 0; i < m_emissionModules.Length; i++)
                m_emissionModules[i].rateOverTime = m_originalRates[i] * rateMultiplier;
        }

        public void Play()
        {
            for (int i = 0; i < Particles.Length; i++) 
                Particles[i].Play();
        }
    }
}