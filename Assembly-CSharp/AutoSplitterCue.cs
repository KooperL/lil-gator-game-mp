using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class AutoSplitterCue : MonoBehaviour
{
	// Token: 0x06000F32 RID: 3890 RVA: 0x0000D36A File Offset: 0x0000B56A
	private void Start()
	{
		SpeedrunData.Cue(this.cueType);
	}

	// Token: 0x04001388 RID: 5000
	public SpeedrunCueTime cueType;
}
