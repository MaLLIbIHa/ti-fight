using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public int health;
	private int maxHealth = 10;

	[SerializeField] private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
		health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
		{
			Destroy(gameObject);
			var explode = Instantiate(explosion, transform.position, transform.rotation);
			Destroy(explode, 1.5f);
		}
    }
	
	public void ChangeHealth(int amount)
	{
		health += amount;
		Mathf.Clamp(health, 0, maxHealth);
	}
}
