using System;
using UnityEngine;

public class AutoSplitterCue : MonoBehaviour
{
	// Token: 0x06000F8E RID: 3982 RVA: 0x0000D6FD File Offset: 0x0000B8FD
	private void Start()
	{
		SpeedrunData.Cue(this.cueType);
	}

	public SpeedrunCueTime cueType;
}
