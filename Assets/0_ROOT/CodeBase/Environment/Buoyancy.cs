using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(BoxCollider))]
    public class Buoyancy : MonoBehaviour
    {
        [SerializeField] private BoxCollider m_waterArea;
        [SerializeField] private float m_buoyancyForce = 100.0f;
        [SerializeField] private float m_depthMultiplier = 3.0f;
        private float m_topSurfaceYPos;
        private float m_yPos;

        private readonly Dictionary<Collider, Rigidbody> m_dependedRigidbodiesWithColliders = new();

        private void Awake()
        {
            Vector3 position = transform.position;
            m_yPos = position.y + m_waterArea.center.y;
        
            m_topSurfaceYPos = m_yPos + m_waterArea.size.y / 2.0f;
            transform.position = new Vector3(position.x, m_topSurfaceYPos, position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody attachedRigidbody = other.attachedRigidbody;
            if (attachedRigidbody != null) 
                m_dependedRigidbodiesWithColliders.TryAdd(other, attachedRigidbody);
        }
        
        private void OnTriggerStay(Collider other)
        {
            if(!m_dependedRigidbodiesWithColliders.ContainsKey(other))
                return;
            
            Rigidbody rb = m_dependedRigidbodiesWithColliders[other];
            if (rb != null)
            {
                float otherY = rb.transform.position.y;
            
                float yDelta = 0.0f + otherY;
            
                if(yDelta > 0)
                    return;

                float heightPushCoefficient = Mathf.Abs(yDelta) * m_depthMultiplier;
            
                // Apply an upward force to simulate buoyancy
                rb.AddForce(Vector3.up * m_buoyancyForce * heightPushCoefficient,
                    ForceMode.Force);
            }
        }
    }
}