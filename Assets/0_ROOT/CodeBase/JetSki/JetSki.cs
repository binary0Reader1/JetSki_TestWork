using System;
using Environment;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace JetSki
{
    public class JetSki : MonoBehaviour
    {
        public event Action OnWaterExit;
        public event Action OnWaterEnter;
        [field: SerializeField] public Rigidbody Rb { get; private set; }
        [field: SerializeField] public ConfigurableJoint ConfigurableJoint { get; private set; }
        [field: SerializeField] public float TargetMoveSpeed { get; private set; } = 30.0f;

        [SerializeField] private float m_targetRotationSpeed = 5f;
        [SerializeField] private float m_rotationLimitInDegrees = 60.0f;

        private IGameInputService m_gameInputService;

        private Vector3 m_originPosition;

        private bool m_onWater;

        private Vector3 m_originalRotationEulerAngles;
        private Vector3 m_cashedCurrentRotation;
        private float m_currenRotationSpeed;

        private float m_currentMoveSpeed;

        [Inject]
        public void Construct(IGameInputService gameInputService) =>
            m_gameInputService = gameInputService;


        private void Start()
        {
            m_originalRotationEulerAngles = transform.rotation.eulerAngles;
            m_cashedCurrentRotation = m_originalRotationEulerAngles;

            m_currentMoveSpeed = TargetMoveSpeed;
            m_currenRotationSpeed = m_targetRotationSpeed;

            m_originPosition = transform.position;
        }

        private void FixedUpdate()
        {
            float direction = m_gameInputService.XInputDirection;
            if (m_onWater)
            {
                m_currenRotationSpeed = Mathf.Lerp(m_currenRotationSpeed, m_targetRotationSpeed, Time.fixedDeltaTime);
                m_currentMoveSpeed = Mathf.Lerp(m_currentMoveSpeed, TargetMoveSpeed, Time.fixedDeltaTime * 2.0f);
            }
            else
            {
                m_currenRotationSpeed =
                    Mathf.Lerp(m_currenRotationSpeed, m_targetRotationSpeed / 4.0f, Time.fixedDeltaTime);
                m_currentMoveSpeed = Rb.velocity.magnitude;
            }

            // Calculate the target rotation based on the direction and rotation limit
            float targetYRotation = m_originalRotationEulerAngles.y + direction * m_rotationLimitInDegrees;
            float targetZRotation = m_originalRotationEulerAngles.z + -direction * m_rotationLimitInDegrees / 2;

            float currentXRotation = Mathf.LerpAngle(m_cashedCurrentRotation.x, m_originalRotationEulerAngles.x,
                Time.fixedDeltaTime * (m_currenRotationSpeed / 2));
            float currentYRotation = Mathf.LerpAngle(m_cashedCurrentRotation.y, targetYRotation,
                Time.fixedDeltaTime * m_currenRotationSpeed);
            float currentZRotation = Mathf.LerpAngle(m_cashedCurrentRotation.z, targetZRotation,
                Time.fixedDeltaTime * m_currenRotationSpeed * 2);

            m_cashedCurrentRotation = new Vector3(currentXRotation, currentYRotation, currentZRotation);

            Quaternion newRotation = Quaternion.Euler(currentXRotation, currentYRotation, currentZRotation);
            ConfigurableJoint.targetRotation = Quaternion.Inverse(newRotation);


            if (!m_onWater)
                return;

            Vector3 forwardDirection = transform.forward;

            Vector3 rawNewVelocity = forwardDirection * m_currentMoveSpeed;
            Vector3 rawToCurrentDeltas = (rawNewVelocity - Rb.velocity) * 100;
            rawToCurrentDeltas.y = (m_originPosition.y - transform.position.y) / (Rb.mass * TargetMoveSpeed);

            Rb.AddForce(rawNewVelocity);
            Rb.AddForce(rawToCurrentDeltas);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Water>() == null) return;

            m_onWater = true;
            OnWaterEnter?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Water>() == null) return;

            m_onWater = false;
            OnWaterExit?.Invoke();
        }
    }
}