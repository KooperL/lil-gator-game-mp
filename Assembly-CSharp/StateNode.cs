using System;
using UnityEngine;
using XNode;

// Token: 0x02000287 RID: 647
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

	// Token: 0x0400123C RID: 4668
	[Node.InputAttribute(Node.ShowBackingValue.Unconnected, Node.ConnectionType.Multiple, Node.TypeConstraint.None, false)]
	public StateNode.Empty enter;

	// Token: 0x0400123D RID: 4669
	[Node.OutputAttribute(Node.ShowBackingValue.Never, Node.ConnectionType.Multiple, Node.TypeConstraint.None, false)]
	public StateNode.Empty exit;

	// Token: 0x02000436 RID: 1078
	[Serializable]
	public class Empty
	{
	}
}
