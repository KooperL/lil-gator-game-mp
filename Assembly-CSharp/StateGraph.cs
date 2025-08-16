using System;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class StateGraph : NodeGraph
{
	// Token: 0x060010EB RID: 4331 RVA: 0x0000E6F7 File Offset: 0x0000C8F7
	public void Continue()
	{
		this.current.MoveNext();
	}

	public StateNode current;
}
