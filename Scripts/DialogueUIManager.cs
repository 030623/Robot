using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MeetAndTalk
{
	public class DialogueUIManager : MonoBehaviour
	{
		public static DialogueUIManager Instance;

		[Header("Dialogue UI")]
		public bool showSeparateName;

		public TextMeshProUGUI nameTextBox;

		public TextMeshProUGUI textBox;

		[Space]
		public GameObject dialogueCanvas;

		public Slider TimerSlider;

		public GameObject SkipButton;

		public GameObject SpriteLeft;

		public GameObject SpriteRight;

		[Header("Dynamic Dialogue UI")]
		public GameObject ButtonPrefab;

		public GameObject ButtonContainer;

		[HideInInspector]
		public string prefixText;

		[HideInInspector]
		public string fullText;

		private string currentText = "";

		private int characterIndex;

		private float lastTypingTime;

		private List<Button> buttons = new List<Button>();

		private List<TextMeshProUGUI> buttonsTexts = new List<TextMeshProUGUI>();

		private void Awake()
		{
			Instance = this;
			SpriteLeft.SetActive(value: false);
			SpriteRight.SetActive(value: false);
		}

		private void Update()
		{
			textBox.text = prefixText + fullText;
		}

		public void ResetText(string prefix)
		{
			currentText = prefix;
			prefixText = prefix;
			characterIndex = 0;
		}

		public void SetButtons(List<string> _texts, List<UnityAction> _unityActions, bool showTimer)
		{
			foreach (Transform item in ButtonContainer.transform)
			{
				Object.Destroy(item.gameObject);
			}
			for (int i = 0; i < _texts.Count; i++)
			{
				GameObject obj = Object.Instantiate(ButtonPrefab, ButtonContainer.transform);
				obj.transform.Find("Text").GetComponent<TMP_Text>().text = _texts[i];
				obj.gameObject.SetActive(value: true);
				obj.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
				obj.GetComponent<Button>().onClick.AddListener(_unityActions[i]);
			}
			TimerSlider.gameObject.SetActive(showTimer);
		}
	}
}
