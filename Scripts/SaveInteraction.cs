using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class SaveInteraction : MonoBehaviour
{
	public Canvas interactionUIPrefab;

	private Canvas canvas;

	private Button saveButton;

	public Transform interactionUIAnchor;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!(other.tag == "Player"))
		{
			return;
		}
		interactionUIAnchor.gameObject.SetActive(value: true);
		if (saveButton == null)
		{
			canvas = Object.Instantiate(interactionUIPrefab, interactionUIAnchor);
			saveButton = canvas.GetComponentInChildren<Button>();
			canvas.worldCamera = GamingCameraManager.Instance.currentCamera;
			saveButton.onClick.AddListener(delegate
			{
				GameManager.instance.SavePosition(LocomotionControl.Instance.transform.position);
				MainCanvasController.instance.ShowTip("Save Success", 2f);
			});
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			interactionUIAnchor.gameObject.SetActive(value: false);
		}
	}
}
