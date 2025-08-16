using System;
using UnityEngine;

public class ToggleFlashbackState : MonoBehaviour
{
	// Token: 0x06000878 RID: 2168 RVA: 0x000085BE File Offset: 0x000067BE
	public void Toggle()
	{
		Game.WorldState = ((Game.WorldState == WorldState.Flashback) ? WorldState.Story : WorldState.Flashback);
	}
}
