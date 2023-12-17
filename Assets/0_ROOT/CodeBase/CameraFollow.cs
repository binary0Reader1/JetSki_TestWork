using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    private Vector3 m_offset;
    private Vector3 m_originPos;
    private const float FOLLOW_SPEED = 8.5f;

    private void Start()
    {
        m_originPos = transform.position;
        m_offset = m_originPos - m_target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = m_target.transform.position + m_offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * FOLLOW_SPEED);
    }
}
