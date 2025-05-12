using System;
using MeetAndTalk.Localization;

namespace MeetAndTalk
{
	[Serializable]
	public class LanguageGeneric<T>
	{
		public LocalizationEnum languageEnum;

		public T LanguageGenericType;
	}
}
