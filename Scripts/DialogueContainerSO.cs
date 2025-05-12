using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeetAndTalk
{
	[Serializable]
	[CreateAssetMenu(menuName = "Dialogue/New Dialogue")]
	public class DialogueContainerSO : ScriptableObject
	{
		public List<NodeLinkData> NodeLinkDatas = new List<NodeLinkData>();

		public List<DialogueChoiceNodeData> DialogueChoiceNodeDatas = new List<DialogueChoiceNodeData>();

		public List<DialogueNodeData> DialogueNodeDatas = new List<DialogueNodeData>();

		public List<EndNodeData> EndNodeDatas = new List<EndNodeData>();

		public List<StartNodeData> StartNodeDatas = new List<StartNodeData>();

		[Header("Pro Version Node")]
		public List<TimerChoiceNodeData> TimerChoiceNodeDatas = new List<TimerChoiceNodeData>();

		public List<EventNodeData> EventNodeDatas = new List<EventNodeData>();

		public List<RandomNodeData> RandomNodeDatas = new List<RandomNodeData>();

		public List<CommandNodeData> CommandNodeDatas = new List<CommandNodeData>();

		public List<IfNodeData> IFNodeDatas = new List<IfNodeData>();

		public List<BaseNodeData> AllNodes
		{
			get
			{
				List<BaseNodeData> list = new List<BaseNodeData>();
				list.AddRange(DialogueNodeDatas);
				list.AddRange(DialogueChoiceNodeDatas);
				list.AddRange(TimerChoiceNodeDatas);
				list.AddRange(EndNodeDatas);
				list.AddRange(EventNodeDatas);
				list.AddRange(StartNodeDatas);
				list.AddRange(RandomNodeDatas);
				list.AddRange(CommandNodeDatas);
				return list;
			}
		}
	}
}
