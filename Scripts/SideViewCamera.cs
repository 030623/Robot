using UnityEngine;

public class SideViewCamera : MonoBehaviour
{
	public Transform playerTransform;

	[SerializeField]
	private Vector3 offset;

	private Camera cam;

	public float speed = 6f;

	private float followRange;

	private void Start()
	{
		cam = GetComponent<Camera>();
		base.transform.position = playerTransform.position + offset;
		followRange = offset.z;
	}

	private void LateUpdate()
	{
		if (PlayerInteraction.instance.enabled && LocomotionControl.Instance.enabled)
		{
			float axis = Input.GetAxis("Horizontal");
			base.transform.Translate(Vector3.right * axis * Time.deltaTime * speed);
			Vector3 position = base.transform.position;
			if (Mathf.Abs(base.transform.position.z - playerTransform.position.z) > followRange)
			{
				position.z = ((base.transform.position.z > playerTransform.position.z) ? (playerTransform.position.z + followRange) : (playerTransform.position.z - followRange));
				base.transform.position = position;
			}
			float z = position.z;
			position = playerTransform.position + offset;
			position.z = z;
			base.transform.position = position;
		}
	}

	public void ChangeSide(float value)
	{
		cam.orthographicSize -= value;
	}
}
