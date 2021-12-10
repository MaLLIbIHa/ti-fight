using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRb;
	public GameObject flares; 
	public ParticleSystem[] thrustPrt;
	public GameObject explodeOnDeath;
	public Camera cam;

	internal int health;
	private int maxHealth = 5;

	private float JetThrust = 10000.0f;

	public float yawSpeed = 25f;
	public float pitchSpeed = 40f;
	public float rollSpeed = 45f;

	private float yaw;
	private float pitch;
	private float roll;
	private float thrust;

	private float speedFactor;

	internal bool haveFlares = true;
	internal bool IsFlaresActive = false;
	private float flaresTime = 5f;
	private float flareCooldownTime = 7f;
	private float flareCooldown;
	private float flareActiveTime;

	internal float speed;
	internal Vector3 crosshairPos;

	private const float aeroEffect = 0.1f;

	// Start is called before the first frame update
	void Start()
    {
		playerRb = GetComponent<Rigidbody>();
		thrust = 100f;
		health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
		//Update variables
		if (yaw > 0.0f) yaw = Mathf.Clamp(yaw - 0.007f, 0.0f, 1.0f);
		else yaw = Mathf.Clamp(yaw + 0.007f, -1.0f, 0.0f);
		if (roll > 0.0f) roll = Mathf.Clamp(roll - 0.007f, 0.0f, 1.0f);
		else roll = Mathf.Clamp(roll + 0.007f, -1.0f, 0.0f);
		if (pitch > 0.0f) pitch = Mathf.Clamp(pitch - 0.007f, 0.0f, 1.0f);
		else pitch = Mathf.Clamp(pitch + 0.007f, -1.0f, 0.0f);

		//Yaw
		if (Input.GetKey(KeyCode.RightArrow)) yaw = Mathf.Clamp(yaw + 0.01f, 0.0f, 1.0f);
		if (Input.GetKey(KeyCode.LeftArrow)) yaw = Mathf.Clamp(yaw - 0.01f, -1.0f, 0.0f);
		//Roll
		if (Input.GetKey(KeyCode.A)) roll = Mathf.Clamp(roll + 0.05f, 0.0f, 1.0f);
		if (Input.GetKey(KeyCode.D)) roll = Mathf.Clamp(roll - 0.05f, -1.0f, 0.0f);
		//Pitch
		if (Input.GetKey(KeyCode.UpArrow)) pitch = Mathf.Clamp(pitch + 0.03f, 0.0f, 1.0f);
		if (Input.GetKey(KeyCode.DownArrow)) pitch = Mathf.Clamp(pitch - 0.03f, -1.0f, 0.0f);
		//Thrust
		if (Input.GetKey(KeyCode.W)) thrust += 20.0f;
		else if (Input.GetKey(KeyCode.S)) thrust += -20.0f;
		else thrust = 0.0f;

		thrust = Mathf.Clamp(thrust, -30.0f, 60.0f);

		if (Time.time > flareCooldown) haveFlares = true;
		if (Input.GetKeyDown(KeyCode.X) && (haveFlares)) { 
			IsFlaresActive = true;
			haveFlares = false;
			var flare = Instantiate(flares, transform.position, transform.rotation);
			Destroy(flare, 2f);
			flareCooldown = Time.time + flareCooldownTime;
			flareActiveTime = Time.time + flaresTime; 
		}
		if (Time.time > flareActiveTime) IsFlaresActive = false;

		//Modify life time of particles
		for (int i = 0; i < 3; i++)
		{
			var thrustPrtmain = thrustPrt[i].main;
			thrustPrtmain.startLifetimeMultiplier = thrust / 100.0f;	
		}
	}

	void FixedUpdate()
	{
		//Rotation update
		transform.RotateAround(transform.position, transform.up, yaw * Time.deltaTime * yawSpeed * speedFactor); // Yaw
		transform.RotateAround(transform.position, transform.right, pitch * Time.deltaTime * pitchSpeed * speedFactor); // Pitch
		transform.RotateAround(transform.position, transform.forward, roll * Time.deltaTime * rollSpeed * speedFactor); // Roll

		//Update speed and velocity
		var localVelocity = transform.InverseTransformDirection(playerRb.velocity);
		var localSpeed = Mathf.Max(0f, localVelocity.z);

		speed = localSpeed;

		speedFactor = Mathf.Clamp(localSpeed / 100.0f, 0.1f, 1.0f);

		var aerofactor = Vector3.Dot(transform.forward, playerRb.velocity.normalized);
		aerofactor *= aerofactor;
		playerRb.velocity = Vector3.Lerp(playerRb.velocity, transform.forward * localSpeed, aerofactor * localSpeed * aeroEffect * Time.fixedDeltaTime);

		//Add force
		if (thrust < 0.0f && playerRb.velocity.magnitude < 5.0f)
			thrust = 0.0f;
		playerRb.AddForce(transform.forward * thrust * JetThrust);
	}

	private void LateUpdate()
	{
		crosshairPos = cam.WorldToScreenPoint(transform.position + (transform.forward * 500f));
	}

	public void ChangeHealth(int amount)
	{
		health += amount;
		Mathf.Clamp(health, 0, maxHealth);
		if (health <= 0)
		{
			var explode = Instantiate(explodeOnDeath, transform.position, transform.rotation);
			Destroy(gameObject);
			Destroy(explode, 2f);
		}
	}
}
