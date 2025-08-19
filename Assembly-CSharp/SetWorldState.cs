using System;
using UnityEngine;

public class SetWorldState : MonoBehaviour
{
	// Token: 0x06000F42 RID: 3906 RVA: 0x0000D35F File Offset: 0x0000B55F
	private void OnEnable()
	{
		Game.g.SetWorldState(this.worldState, this.forceChange, this.delaySceneChange);
	}

	public WorldState worldState;

	public bool forceChange;

	public bool delaySceneChange;
}
