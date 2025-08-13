using System;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class ToggleFlashbackState : MonoBehaviour
{
	// Token: 0x060006FA RID: 1786 RVA: 0x00023486 File Offset: 0x00021686
	public void Toggle()
	{
		Game.WorldState = ((Game.WorldState == WorldState.Flashback) ? WorldState.Story : WorldState.Flashback);
	}
}
