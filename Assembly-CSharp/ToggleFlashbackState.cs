using System;
using UnityEngine;

public class ToggleFlashbackState : MonoBehaviour
{
	// Token: 0x06000878 RID: 2168 RVA: 0x000085D3 File Offset: 0x000067D3
	public void Toggle()
	{
		Debug.Log("FLASHBACK TOGGLED");
		Game.WorldState = ((Game.WorldState == WorldState.Flashback) ? WorldState.Story : WorldState.Flashback);
	}
}
