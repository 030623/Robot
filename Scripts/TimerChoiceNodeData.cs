using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeetAndTalk
{
	[Serializable]
	public class TimerChoiceNodeData : BaseNodeData
	{
		public List<DialogueNodePort> DialogueNodePorts;

		public List<LanguageGeneric<AudioClip>> AudioClips;

		public DialogueCharacterSO Character;

		public AvatarPosition AvatarPos;

		public AvatarType AvatarType;

		public List<LanguageGeneric<string>> TextType;

		public float Duration;

		public float time;
	}
}
