using System;
using UnityEngine;

public class AutoSplitterCue : MonoBehaviour
{
	// Token: 0x06000C86 RID: 3206 RVA: 0x0003CEEA File Offset: 0x0003B0EA
	private void Start()
	{
		SpeedrunData.Cue(this.cueType);
	}

	public SpeedrunCueTime cueType;
}
