using System;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class StateGraph : NodeGraph
{
	// Token: 0x060010EB RID: 4331 RVA: 0x0000E716 File Offset: 0x0000C916
	public void Continue()
	{
		this.current.MoveNext();
	}

	public StateNode current;
}
