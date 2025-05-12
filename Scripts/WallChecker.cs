using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
	private List<Collider> colliders = new List<Collider>();

	private void OnTriggerEnter(Collider other)
	{
		if (!(other.tag == "Stair") && (!other.isTrigger || !(other.tag != "Block")) && (other.gameObject.layer == 3 || other.tag == "Wall" || other.tag == "Block"))
		{
			colliders.Add(other);
			LocomotionControl.Instance.isHitWall = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.tag == "Wall")
		{
			colliders.Remove(other);
			if (colliders.Count < 1)
			{
				LocomotionControl.Instance.isHitWall = false;
			}
		}
	}
}
