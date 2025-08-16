using System;
using UnityEngine;
using XNode;

public class StateNode : Node
{
	// Token: 0x060010ED RID: 4333 RVA: 0x00056FB4 File Offset: 0x000551B4
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

	// Token: 0x060010EE RID: 4334 RVA: 0x0000E70C File Offset: 0x0000C90C
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
