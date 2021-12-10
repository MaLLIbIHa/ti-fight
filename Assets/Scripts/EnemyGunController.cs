using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
	private float m_radius = 100.0f;

	private float m_rateTime;

	public float shootingRate = 2.0f;

	[SerializeField] private GameObject m_missilePrefab;
	
	// Update is called once per frame
	void Update()
    {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		transform.LookAt(player.transform);
		if ((Mathf.Abs(player.transform.position.magnitude - transform.position.magnitude) <= m_radius) && Time.time > m_rateTime)
		{
			GameObject missile = Instantiate(m_missilePrefab, transform.position, transform.rotation) as GameObject;
			missile.GetComponent<MissileController>().SetTarget(player);

			m_rateTime = Time.time + shootingRate;
		}
    }

}
