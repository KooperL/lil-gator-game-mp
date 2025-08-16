using System;
using UnityEngine;

public class ChangeMusicLoop : MonoBehaviour
{
	// Token: 0x0600005D RID: 93 RVA: 0x00002466 File Offset: 0x00000666
	public void OnEnable()
	{
		this.system.ChangeLoop(this.loopStartBeats, this.loopEndBeats, this.changeBehavior);
	}

	public MusicSystem system;

	public MusicSystem.LoopChangeBehavior changeBehavior;

	public int loopStartBeats;

	public int loopEndBeats;
}
