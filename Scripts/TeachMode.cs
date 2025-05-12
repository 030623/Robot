using TMPro;
using UnityEngine;

public class TeachMode : MonoBehaviour
{
	public TextMeshProUGUI UITipText;

	public string[] tips;

	private int tipIndex;

	private bool isDelaying;

	private void Start()
	{
		UITipText.text = tips[tipIndex];
	}

	private void Update()
	{
		if (!isDelaying && tipIndex == 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
		{
			tipIndex++;
			isDelaying = true;
			Invoke("DelayTip", 2f);
		}
		else if (!isDelaying && tipIndex == 1 && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K)))
		{
			tipIndex++;
			Object.Destroy(base.gameObject);
		}
	}

	private void DelayTip()
	{
		isDelaying = false;
		UITipText.text = tips[tipIndex];
	}
}
