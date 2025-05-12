using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
	public Button[] buttons;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
		{
			return;
		}
		MainCanvasController.instance.onClear.RemoveAllListeners();
		MainCanvasController.instance.onFadeComplete.RemoveAllListeners();
		PlayerInteraction.instance.enabled = false;
		LocomotionControl.Instance.enabled = false;
		MainCanvasController.instance.ExcessiveScene(isNotClear: true);
		MainCanvasController.instance.onFadeComplete.AddListener(delegate
		{
			MainCanvasController.instance.TypeWriter("Thank you for playing, to be continued");
			Button[] array = buttons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(value: true);
			}
		});
	}
}
