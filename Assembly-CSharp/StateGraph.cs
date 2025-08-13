using System;
using UnityEngine;
using XNode;

// Token: 0x0200035E RID: 862
[CreateAssetMenu]
public class StateGraph : NodeGraph
{
	// Token: 0x06001090 RID: 4240 RVA: 0x0000E3A3 File Offset: 0x0000C5A3
	public void Continue()
	{
		this.current.MoveNext();
	}

	// Token: 0x0400159B RID: 5531
	public StateNode current;
}
