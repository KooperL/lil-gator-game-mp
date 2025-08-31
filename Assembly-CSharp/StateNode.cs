using System;
using UnityEngine;
using XNode;

public class StateNode : Node
{
	// Token: 0x06000DD0 RID: 3536 RVA: 0x00043010 File Offset: 0x00041210
	public void MoveNext()
	{
		if ((this.graph as StateGraph).current != this)
		{
			Debug.LogWarning("Node isn't active");
			return;
		}
		NodePort outputPort = base.GetOutputPort("exit");
		if (!outputPort.IsConnected)
		{
			Debug.LogWarning("Node isn't connected");
			return;
		}
		(outputPort.Connection.node as StateNode).OnEnter();
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x00043074 File Offset: 0x00041274
	public void OnEnter()
	{
		(this.graph as StateGraph).current = this;
	}

	[Node.InputAttribute(Node.ShowBackingValue.Unconnected, Node.ConnectionType.Multiple, Node.TypeConstraint.None, false)]
	public StateNode.Empty enter;

	[Node.OutputAttribute(Node.ShowBackingValue.Never, Node.ConnectionType.Multiple, Node.TypeConstraint.None, false)]
	public StateNode.Empty exit;

	[Serializable]
	public class Empty
	{
	}
}
