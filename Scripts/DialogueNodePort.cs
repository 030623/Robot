using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace MeetAndTalk
{
	[Serializable]
	public class DialogueNodePort
	{
		public string InputGuid;

		public string OutputGuid;

		public TextField TextField;

		public List<LanguageGeneric<string>> TextLanguage = new List<LanguageGeneric<string>>();
	}
}
