using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target;
	public float distance;
	public float heightOffset;

	private float m_turnSpeed = 0.025f;

	private float m_timer;
	private float m_animTime = 2.8f;

    // Update is called once per frame
    void FixedUpdate()
    {
		if (m_timer > m_animTime) m_turnSpeed = 0.15f; else m_timer += Time.deltaTime;

		//Camera update
		transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, m_turnSpeed);
		transform.position = transform.rotation * new Vector3(0f, heightOffset, -distance) + target.position;
    }
}
