using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
public class ToggleFlashbackState : MonoBehaviour
{
	// Token: 0x06000838 RID: 2104 RVA: 0x000082C4 File Offset: 0x000064C4
	public void Toggle()
	{
		Game.WorldState = ((Game.WorldState == WorldState.Flashback) ? WorldState.Story : WorldState.Flashback);
	}
}
