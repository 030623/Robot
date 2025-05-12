using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
	public KeyCode skipKey;

	private Button _button;

	private void Start()
	{
		_button = GetComponent<Button>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(skipKey))
		{
			_button.onClick.Invoke();
		}
	}
}
