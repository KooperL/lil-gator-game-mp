using System;
using UnityEngine;
using XNode;

public class StateNode : Node
{
	// Token: 0x060010ED RID: 4333 RVA: 0x00057124 File Offset: 0x00055324
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

	// Token: 0x060010EE RID: 4334 RVA: 0x0000E72B File Offset: 0x0000C92B
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
