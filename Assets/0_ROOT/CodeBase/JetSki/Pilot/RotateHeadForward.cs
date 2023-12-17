using UnityEngine;

namespace JetSki.Pilot
{
    public class RotateHeadForward : MonoBehaviour
    {
        private Vector3 m_direction = Vector3.forward;
        private ConfigurableJoint m_configurableJoint;

        private Quaternion m_originRotation;
        
        private void Awake()
        {
            m_direction = transform.forward;
            m_configurableJoint = GetComponent<ConfigurableJoint>();
            
            m_originRotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            Quaternion newRotation = transform.rotation * Quaternion.Inverse(m_originRotation);
            m_configurableJoint.targetRotation = newRotation;
        }
    }
}
