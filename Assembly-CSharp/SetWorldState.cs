using System;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class SetWorldState : MonoBehaviour
{
	// Token: 0x06000EE6 RID: 3814 RVA: 0x0000CFAD File Offset: 0x0000B1AD
	private void OnEnable()
	{
		Game.g.SetWorldState(this.worldState, this.forceChange, this.delaySceneChange);
	}

	// Token: 0x04001307 RID: 4871
	public WorldState worldState;

	// Token: 0x04001308 RID: 4872
	public bool forceChange;

	// Token: 0x04001309 RID: 4873
	public bool delaySceneChange;
}
