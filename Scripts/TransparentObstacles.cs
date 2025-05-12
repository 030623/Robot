using System.Collections.Generic;
using UnityEngine;

public class TransparentObstacles : MonoBehaviour
{
	public Transform player;

	public LayerMask obstacleLayer;

	public float heightThreshold = 1f;

	private List<Renderer> currentObstacles = new List<Renderer>();

	private void Update()
	{
		MakeObstaclesTransparent();
	}

	private void MakeObstaclesTransparent()
	{
		foreach (Renderer currentObstacle in currentObstacles)
		{
			SetMaterialTransparent(currentObstacle, transparent: false);
		}
		currentObstacles.Clear();
		Vector3 direction = player.position - base.transform.position;
		RaycastHit[] array = Physics.RaycastAll(base.transform.position, direction, direction.magnitude, obstacleLayer);
		for (int i = 0; i < array.Length; i++)
		{
			RaycastHit raycastHit = array[i];
			Renderer component = raycastHit.collider.GetComponent<Renderer>();
			if (component != null && Mathf.Abs(raycastHit.point.y - player.position.y) > heightThreshold)
			{
				SetMaterialTransparent(component, transparent: true);
				currentObstacles.Add(component);
			}
		}
	}

	private void SetMaterialTransparent(Renderer renderer, bool transparent)
	{
		Material[] materials = renderer.materials;
		foreach (Material material in materials)
		{
			if (transparent)
			{
				material.SetFloat("_Mode", 2f);
				Color color = material.color;
				color.a = 0.3f;
				material.color = color;
				material.SetInt("_SrcBlend", 5);
				material.SetInt("_DstBlend", 10);
				material.SetInt("_ZWrite", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.EnableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = 3000;
			}
			else
			{
				material.SetFloat("_Mode", 0f);
				Color color2 = material.color;
				color2.a = 1f;
				material.color = color2;
				material.SetInt("_SrcBlend", 1);
				material.SetInt("_DstBlend", 0);
				material.SetInt("_ZWrite", 1);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = -1;
			}
		}
	}
}
