using UnityEngine;

public class MapIconsRotationController : MonoBehaviour
{
	[SerializeField] private Transform plTrns;

    // Update is called once per frame
    void Update()
    {
		transform.position = plTrns.position;
		transform.rotation = Quaternion.Euler(90f, plTrns.rotation.eulerAngles.y, 0f);
    }
}
