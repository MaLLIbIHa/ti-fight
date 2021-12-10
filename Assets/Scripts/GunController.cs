using UnityEngine;

public class GunController : MonoBehaviour
{
	public float shootRate;
	private float m_shootRateTime;

	[SerializeField] private GameObject plGm;
	[SerializeField] private GameObject m_shotPrefab;

	private float range = 10000.0f;

	private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
		transform.rotation = plGm.transform.rotation;

        if (Input.GetKey(KeyCode.Space))
		{
			if (Time.time > m_shootRateTime)
			{
				ShootRay();
				m_shootRateTime = Time.time + shootRate;
			}
		}
    }

	void ShootRay()
	{
		Ray ray = new Ray(transform.position, transform.forward);
		GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
		if (Physics.Raycast(ray, out hit, range))
		{
			laser.GetComponent<ShotBehavior>().UpdateTarget(hit.transform.gameObject);
			laser.GetComponent<ShotBehavior>().UpdateTargetPos(hit.point);
		}
		else
		{
			laser.GetComponent<ShotBehavior>().UpdateTargetPos(ray.GetPoint(range));
			GameObject.Destroy(laser, 2f);
		}
	}
}
