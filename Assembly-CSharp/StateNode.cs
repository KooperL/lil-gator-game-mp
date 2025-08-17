using System;
using UnityEngine;
using XNode;

public class StateNode : Node
{
	// Token: 0x060010ED RID: 4333 RVA: 0x00057148 File Offset: 0x00055348
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

	// Token: 0x060010EE RID: 4334 RVA: 0x0000E721 File Offset: 0x0000C921
	public void OnEnter()
	{
		(this.graph as StateGraph).current = this;
	}

	[Node.InputAttribute(1, 0, 0, false)]
	public StateNode.Empty enter;

	[Node.OutputAttribute(0, 0, 0, false)]
	public StateNode.Empty exit;

	[Serializable]
	public class Empty
	{
	}
}
