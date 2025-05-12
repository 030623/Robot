using UnityEngine;

namespace MeetAndTalk
{
	public class DialogueGetData : MonoBehaviour
	{
		[HideInInspector]
		public DialogueContainerSO dialogueContainer;

		protected BaseNodeData GetNodeByGuid(string _targetNodeGuid)
		{
			return dialogueContainer.AllNodes.Find((BaseNodeData node) => node.NodeGuid == _targetNodeGuid);
		}

		protected BaseNodeData GetNodeByNodePort(DialogueNodePort _nodePort)
		{
			return dialogueContainer.AllNodes.Find((BaseNodeData node) => node.NodeGuid == _nodePort.InputGuid);
		}

		protected BaseNodeData GetNextNode(BaseNodeData _baseNodeData)
		{
			NodeLinkData nodeLinkData = dialogueContainer.NodeLinkDatas.Find((NodeLinkData edge) => edge.BaseNodeGuid == _baseNodeData.NodeGuid);
			return GetNodeByGuid(nodeLinkData.TargetNodeGuid);
		}
	}
}
