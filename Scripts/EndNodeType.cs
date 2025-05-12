using System;

namespace MeetAndTalk
{
	[Serializable]
	public enum EndNodeType
	{
		End = 0,
		Repeat = 1,
		GoBack = 2,
		ReturnToStart = 3
	}
}
