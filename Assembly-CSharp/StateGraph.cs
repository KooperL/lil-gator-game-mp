using System;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class StateGraph : NodeGraph
{
	// Token: 0x06000DCE RID: 3534 RVA: 0x00042FF8 File Offset: 0x000411F8
	public void Continue()
	{
		this.current.MoveNext();
	}

	public StateNode current;
}
