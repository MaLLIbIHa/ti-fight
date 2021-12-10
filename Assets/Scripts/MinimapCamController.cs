using UnityEngine;

public class MinimapCamController : MonoBehaviour
{
	[SerializeField] private Transform plTransform;


    void Update()
    {
		Vector3 pos = new Vector3(plTransform.position.x, plTransform.position.y + 100.0f, plTransform.position.z);
		transform.position = pos;

		transform.rotation = Quaternion.Euler(90f, plTransform.rotation.eulerAngles.y, 0.0f);
	}
}
