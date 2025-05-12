using System;
using System.Collections;
using System.Collections.Generic;
using MeetAndTalk.Localization;
using UnityEngine;
using UnityEngine.Events;

namespace MeetAndTalk
{
	public class DialogueManager : DialogueGetData
	{
		[HideInInspector]
		public static DialogueManager Instance;

		public LocalizationManager localizationManager;

		[HideInInspector]
		public DialogueUIManager dialogueUIManager;

		public AudioSource audioSource;

		public UnityEvent StartDialogueEvent;

		public UnityEvent EndDialogueEvent;

		private BaseNodeData currentDialogueNodeData;

		private BaseNodeData lastDialogueNodeData;

		private TimerChoiceNodeData _nodeTimerInvoke;

		private DialogueNodeData _nodeDialogueInvoke;

		private DialogueChoiceNodeData _nodeChoiceInvoke;

		public bool isTalking;

		private float Timer;

		private void Awake()
		{
			Instance = this;
			dialogueUIManager = DialogueUIManager.Instance;
			audioSource = GetComponent<AudioSource>();
		}

		private void Start()
		{
			StartDialogueEvent.AddListener(delegate
			{
				LocomotionControl.Instance.currentState = LocomotionControl.State.Talk;
			});
			EndDialogueEvent.AddListener(delegate
			{
				LocomotionControl.Instance.currentState = LocomotionControl.State.Move;
			});
		}

		private void Update()
		{
			Timer -= Time.deltaTime;
			if (Timer > 0f)
			{
				dialogueUIManager.TimerSlider.value = Timer;
			}
		}

		public void SetupDialogue(DialogueContainerSO dialogue)
		{
			dialogueContainer = dialogue;
		}

		public void StartDialogue(DialogueContainerSO dialogue)
		{
			isTalking = true;
			dialogueUIManager = DialogueUIManager.Instance;
			dialogueContainer = dialogue;
			if (dialogueContainer.StartNodeDatas.Count == 1)
			{
				CheckNodeType(GetNextNode(dialogueContainer.StartNodeDatas[0]));
			}
			else
			{
				CheckNodeType(GetNextNode(dialogueContainer.StartNodeDatas[UnityEngine.Random.Range(0, dialogueContainer.StartNodeDatas.Count)]));
			}
			dialogueUIManager.dialogueCanvas.SetActive(value: true);
			StartDialogueEvent.Invoke();
		}

		public void StartDialogue(string ID)
		{
			StartDialogue();
		}

		public void StartDialogue()
		{
			dialogueUIManager = DialogueUIManager.Instance;
			if (dialogueContainer.StartNodeDatas.Count == 1)
			{
				CheckNodeType(GetNextNode(dialogueContainer.StartNodeDatas[0]));
			}
			else
			{
				CheckNodeType(GetNextNode(dialogueContainer.StartNodeDatas[UnityEngine.Random.Range(0, dialogueContainer.StartNodeDatas.Count)]));
			}
			dialogueUIManager.dialogueCanvas.SetActive(value: true);
			StartDialogueEvent.Invoke();
		}

		public void CheckNodeType(BaseNodeData _baseNodeData)
		{
			if (!(_baseNodeData is StartNodeData nodeData))
			{
				if (!(_baseNodeData is DialogueNodeData nodeData2))
				{
					if (!(_baseNodeData is DialogueChoiceNodeData nodeData3))
					{
						if (_baseNodeData is EndNodeData nodeData4)
						{
							RunNode(nodeData4);
						}
					}
					else
					{
						RunNode(nodeData3);
					}
				}
				else
				{
					RunNode(nodeData2);
				}
			}
			else
			{
				RunNode(nodeData);
			}
		}

		private void RunNode(StartNodeData _nodeData)
		{
			CheckNodeType(GetNextNode(dialogueContainer.StartNodeDatas[0]));
		}

		private void RunNode(DialogueNodeData _nodeData)
		{
			lastDialogueNodeData = currentDialogueNodeData;
			currentDialogueNodeData = _nodeData;
			if (dialogueUIManager.showSeparateName && dialogueUIManager.nameTextBox != null && _nodeData.Character != null)
			{
				dialogueUIManager.ResetText("");
				dialogueUIManager.nameTextBox.text = "<color=" + _nodeData.Character.HexColor() + ">" + _nodeData.Character.characterName.Find((LanguageGeneric<string> text) => text.languageEnum == localizationManager.SelectedLang()).LanguageGenericType + "</color>";
			}
			else if (dialogueUIManager.showSeparateName && dialogueUIManager.nameTextBox != null && _nodeData.Character != null)
			{
				dialogueUIManager.ResetText("");
			}
			else if (_nodeData.Character != null)
			{
				dialogueUIManager.ResetText("<color=" + _nodeData.Character.HexColor() + ">" + _nodeData.Character.characterName.Find((LanguageGeneric<string> text) => text.languageEnum == localizationManager.SelectedLang()).LanguageGenericType + ": </color>");
			}
			else
			{
				dialogueUIManager.ResetText("");
			}
			dialogueUIManager.fullText = _nodeData.TextType.Find((LanguageGeneric<string> text) => text.languageEnum == localizationManager.SelectedLang()).LanguageGenericType ?? "";
			dialogueUIManager.SkipButton.SetActive(value: true);
			MakeButtons(new List<DialogueNodePort>());
			if (_nodeData.AudioClips.Find((LanguageGeneric<AudioClip> clip) => clip.languageEnum == localizationManager.SelectedLang()).LanguageGenericType != null)
			{
				audioSource.PlayOneShot(_nodeData.AudioClips.Find((LanguageGeneric<AudioClip> clip) => clip.languageEnum == localizationManager.SelectedLang()).LanguageGenericType);
			}
			_nodeDialogueInvoke = _nodeData;
			if (_nodeData.Duration != 0f)
			{
				StartCoroutine(tmp());
			}
			IEnumerator tmp()
			{
				yield return new WaitForSeconds(_nodeData.Duration);
				DialogueNode_NextNode();
			}
		}

		private void RunNode(DialogueChoiceNodeData _nodeData)
		{
			lastDialogueNodeData = currentDialogueNodeData;
			currentDialogueNodeData = _nodeData;
			if (dialogueUIManager.showSeparateName && dialogueUIManager.nameTextBox != null && _nodeData.Character != null)
			{
				dialogueUIManager.ResetText("");
				dialogueUIManager.nameTextBox.text = "<color=" + _nodeData.Character.HexColor() + ">" + _nodeData.Character.characterName.Find((LanguageGeneric<string> text) => text.languageEnum == localizationManager.SelectedLang()).LanguageGenericType + "</color>";
			}
			else if (dialogueUIManager.showSeparateName && dialogueUIManager.nameTextBox != null && _nodeData.Character != null)
			{
				dialogueUIManager.ResetText("");
			}
			else if (_nodeData.Character != null)
			{
				dialogueUIManager.ResetText("<color=" + _nodeData.Character.HexColor() + ">" + _nodeData.Character.characterName.Find((LanguageGeneric<string> text) => text.languageEnum == localizationManager.SelectedLang()).LanguageGenericType + ": </color>");
			}
			else
			{
				dialogueUIManager.ResetText("");
			}
			dialogueUIManager.fullText = _nodeData.TextType.Find((LanguageGeneric<string> text) => text.languageEnum == localizationManager.SelectedLang()).LanguageGenericType ?? "";
			dialogueUIManager.SkipButton.SetActive(value: true);
			MakeButtons(new List<DialogueNodePort>());
			_nodeChoiceInvoke = _nodeData;
			StartCoroutine(tmp());
			if (_nodeData.AudioClips.Find((LanguageGeneric<AudioClip> clip) => clip.languageEnum == localizationManager.SelectedLang()).LanguageGenericType != null)
			{
				audioSource.PlayOneShot(_nodeData.AudioClips.Find((LanguageGeneric<AudioClip> clip) => clip.languageEnum == localizationManager.SelectedLang()).LanguageGenericType);
			}
			IEnumerator tmp()
			{
				yield return new WaitForSeconds(_nodeData.Duration);
				ChoiceNode_GenerateChoice();
			}
		}

		private void RunNode(EndNodeData _nodeData)
		{
			switch (_nodeData.EndNodeType)
			{
			case EndNodeType.End:
				dialogueUIManager.dialogueCanvas.SetActive(value: false);
				EndDialogueEvent.Invoke();
				break;
			case EndNodeType.Repeat:
				CheckNodeType(GetNodeByGuid(currentDialogueNodeData.NodeGuid));
				break;
			case EndNodeType.GoBack:
				CheckNodeType(GetNodeByGuid(lastDialogueNodeData.NodeGuid));
				break;
			case EndNodeType.ReturnToStart:
				CheckNodeType(GetNextNode(dialogueContainer.StartNodeDatas[UnityEngine.Random.Range(0, dialogueContainer.StartNodeDatas.Count)]));
				break;
			}
		}

		private void MakeButtons(List<DialogueNodePort> _nodePorts)
		{
			List<string> list = new List<string>();
			List<UnityAction> list2 = new List<UnityAction>();
			foreach (DialogueNodePort nodePort in _nodePorts)
			{
				list.Add(nodePort.TextLanguage.Find((LanguageGeneric<string> text) => text.languageEnum == localizationManager.SelectedLang()).LanguageGenericType);
				UnityAction a = null;
				a = (UnityAction)Delegate.Combine(a, (UnityAction)delegate
				{
					CheckNodeType(GetNodeByGuid(nodePort.InputGuid));
				});
				list2.Add(a);
			}
			dialogueUIManager.SetButtons(list, list2, showTimer: false);
		}

		private void DialogueNode_NextNode()
		{
			CheckNodeType(GetNextNode(_nodeDialogueInvoke));
		}

		private void ChoiceNode_GenerateChoice()
		{
			MakeButtons(_nodeChoiceInvoke.DialogueNodePorts);
			dialogueUIManager.SkipButton.SetActive(value: false);
		}

		public void SkipDialogue()
		{
			StopAllCoroutines();
			BaseNodeData baseNodeData = currentDialogueNodeData;
			if (!(baseNodeData is DialogueNodeData))
			{
				if (baseNodeData is DialogueChoiceNodeData)
				{
					ChoiceNode_GenerateChoice();
				}
			}
			else
			{
				DialogueNode_NextNode();
			}
		}

		public void ForceEndDialog()
		{
			dialogueUIManager.dialogueCanvas.SetActive(value: false);
			EndDialogueEvent.Invoke();
		}
	}
}
