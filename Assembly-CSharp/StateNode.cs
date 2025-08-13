using System;
using UnityEngine;
using XNode;

// Token: 0x0200035F RID: 863
public class StateNode : Node
{
	// Token: 0x06001092 RID: 4242 RVA: 0x00055224 File Offset: 0x00053424
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

	// Token: 0x06001093 RID: 4243 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
	public void OnEnter()
	{
		(this.graph as StateGraph).current = this;
	}

	// Token: 0x0400159C RID: 5532
	[Node.InputAttribute(1, 0, 0, false)]
	public StateNode.Empty enter;

	// Token: 0x0400159D RID: 5533
	[Node.OutputAttribute(0, 0, 0, false)]
	public StateNode.Empty exit;

	// Token: 0x02000360 RID: 864
	[Serializable]
	public class Empty
	{
	}
}
