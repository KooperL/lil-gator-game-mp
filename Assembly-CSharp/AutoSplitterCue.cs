using System;
using UnityEngine;

// Token: 0x0200023C RID: 572
public class AutoSplitterCue : MonoBehaviour
{
	// Token: 0x06000C86 RID: 3206 RVA: 0x0003CEEA File Offset: 0x0003B0EA
	private void Start()
	{
		SpeedrunData.Cue(this.cueType);
	}

	// Token: 0x04001072 RID: 4210
	public SpeedrunCueTime cueType;
}
