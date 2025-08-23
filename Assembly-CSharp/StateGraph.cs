using System;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class StateGraph : NodeGraph
{
	// Token: 0x060010EC RID: 4332 RVA: 0x0000E716 File Offset: 0x0000C916
	public void Continue()
	{
		this.current.MoveNext();
	}

	public StateNode current;
}
