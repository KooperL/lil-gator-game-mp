using System;
using UnityEngine;

public class SetWorldState : MonoBehaviour
{
	// Token: 0x06000C47 RID: 3143 RVA: 0x0003B115 File Offset: 0x00039315
	private void OnEnable()
	{
		Game.g.SetWorldState(this.worldState, this.forceChange, this.delaySceneChange);
	}

	public WorldState worldState;

	public bool forceChange;

	public bool delaySceneChange;
}
