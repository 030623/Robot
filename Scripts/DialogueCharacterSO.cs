using System;
using System.Collections.Generic;
using MeetAndTalk.Localization;
using UnityEngine;

namespace MeetAndTalk
{
	[CreateAssetMenu(menuName = "Dialogue/New Dialogue Character")]
	public class DialogueCharacterSO : ScriptableObject
	{
		public List<LanguageGeneric<string>> characterName;

		public Color textColor = new Color(0.8f, 0.8f, 0.8f, 1f);

		public string HexColor()
		{
			return "#" + ColorUtility.ToHtmlStringRGB(textColor);
		}

		private void OnValidate()
		{
			if (characterName.Count == Enum.GetNames(typeof(LocalizationEnum)).Length)
			{
				return;
			}
			if (characterName.Count < Enum.GetNames(typeof(LocalizationEnum)).Length)
			{
				for (int i = characterName.Count; i < Enum.GetNames(typeof(LocalizationEnum)).Length; i++)
				{
					characterName.Add(new LanguageGeneric<string>());
					characterName[i].languageEnum = (LocalizationEnum)i;
					characterName[i].LanguageGenericType = "";
				}
			}
			if (characterName.Count > Enum.GetNames(typeof(LocalizationEnum)).Length)
			{
				for (int j = 0; j < characterName.Count - Enum.GetNames(typeof(LocalizationEnum)).Length; j++)
				{
					characterName.Remove(characterName[characterName.Count - 1]);
				}
			}
		}

		public string GetName()
		{
			LocalizationManager _manager = (LocalizationManager)Resources.Load("Languages");
			if (_manager != null)
			{
				return characterName.Find((LanguageGeneric<string> text) => text.languageEnum == _manager.SelectedLang()).LanguageGenericType;
			}
			return "Can't find Localization Manager in scene";
		}
	}
}
