using System;
using MeetAndTalk.GlobalValue;

namespace MeetAndTalk
{
	[Serializable]
	public class IfNodeData : BaseNodeData
	{
		public string ValueName;

		public GlobalValueIFOperations Operations;

		public string OperationValue;

		public string TrueGUID;

		public string FalseGUID;
	}
}
