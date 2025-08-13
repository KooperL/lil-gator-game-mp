using System;
using UnityEngine;

// Token: 0x02000236 RID: 566
public class SetWorldState : MonoBehaviour
{
	// Token: 0x06000C47 RID: 3143 RVA: 0x0003B115 File Offset: 0x00039315
	private void OnEnable()
	{
		Game.g.SetWorldState(this.worldState, this.forceChange, this.delaySceneChange);
	}

	// Token: 0x04001006 RID: 4102
	public WorldState worldState;

	// Token: 0x04001007 RID: 4103
	public bool forceChange;

	// Token: 0x04001008 RID: 4104
	public bool delaySceneChange;
}
