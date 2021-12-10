using UnityEngine;

public class MissileController : MonoBehaviour
{
	[SerializeField] private GameObject explosionPrt;

	private GameObject m_player;
	private Transform m_playerTr;
	private Rigidbody missileRb;

	public float speed = 20f;
	public float turnSpeed = 3f;

	public float maxLifeTime = 4f;
	private float lifeTime;

	private float randomFactor;

	void Start()
	{
		missileRb = GetComponent<Rigidbody>();
		lifeTime = 0f;
	}

	// Update is called once per frame
	void Update()
    {

        if (m_player != null)
		{
			m_playerTr = m_player.GetComponent<Transform>();
			missileRb.velocity = transform.forward * speed;

			if (m_player.GetComponent<PlayerController>().IsFlaresActive)
			{
				randomFactor = Random.Range(5f, 10f);
			}
			else randomFactor = 0f;

			var missileRot = Quaternion.LookRotation(m_playerTr.position - transform.position + new Vector3(randomFactor, randomFactor, randomFactor));
			missileRb.MoveRotation(Quaternion.RotateTowards(transform.rotation, missileRot, turnSpeed));

			lifeTime += Time.deltaTime;
			if (lifeTime >= maxLifeTime) Destroy(gameObject);
		}
    }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>() != null)
		{
			var explosion = Instantiate(explosionPrt, transform.position, transform.rotation);
			Destroy(gameObject);
			Destroy(explosion, 1f);
			m_player.GetComponent<PlayerController>().ChangeHealth(-1);
		}
	}

	public void SetTarget(GameObject pl)
	{
		m_player = pl;
	}
}
