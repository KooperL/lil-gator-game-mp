using System;
using UnityEngine;
using XNode;

// Token: 0x02000286 RID: 646
[CreateAssetMenu]
public class StateGraph : NodeGraph
{
	// Token: 0x06000DCE RID: 3534 RVA: 0x00042FF8 File Offset: 0x000411F8
	public void Continue()
	{
		this.current.MoveNext();
	}

	// Token: 0x0400123B RID: 4667
	public StateNode current;
}
