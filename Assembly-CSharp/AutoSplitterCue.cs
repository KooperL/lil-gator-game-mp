using System;
using UnityEngine;

public class AutoSplitterCue : MonoBehaviour
{
	// Token: 0x06000F8E RID: 3982 RVA: 0x0000D712 File Offset: 0x0000B912
	private void Start()
	{
		SpeedrunData.Cue(this.cueType);
	}

	public SpeedrunCueTime cueType;
}
