using UnityEngine;

public class ShotBehavior : MonoBehaviour {

	private Vector3 m_targetPos;
	private GameObject target;

	public GameObject explosionPrefab;

	// Update is called once per frame
	void Update () {

		if (transform.position == m_targetPos)
		{
			if (target.GetComponent<EnemyController>() != null)
			{
				Debug.Log(target.name);
				target.GetComponent<EnemyController>().ChangeHealth(-1);
			}
			explode();
			return;
		}

		transform.position = Vector3.MoveTowards(transform.position, m_targetPos, Time.deltaTime * 700f);
	}

	void explode()
	{
		GameObject explosion = GameObject.Instantiate(explosionPrefab, transform.position, transform.rotation);
		Destroy(gameObject);
		Destroy(explosion, 1f);
	}

	public void UpdateTargetPos(Vector3 pos)
	{
		m_targetPos = pos;
	}

	public void UpdateTarget(GameObject tar)
	{
		target = tar;
	}
}
