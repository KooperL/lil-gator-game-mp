using System;
using UnityEngine;
using UnityEngine.Playables;

// Token: 0x02000009 RID: 9
public class TimelineSync : MonoBehaviour
{
	// Token: 0x06000015 RID: 21 RVA: 0x00002601 File Offset: 0x00000801
	private void OnValidate()
	{
		if (this.director == null)
		{
			this.director = base.GetComponent<PlayableDirector>();
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000261D File Offset: 0x0000081D
	public void Wait()
	{
		if (this.continueTokens > 0)
		{
			this.continueTokens--;
			return;
		}
		this.isWaiting = true;
		this.director.Pause();
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002649 File Offset: 0x00000849
	public void ContinueTimeline()
	{
		if (this.isWaiting)
		{
			this.isWaiting = false;
			this.director.Resume();
			return;
		}
		this.continueTokens++;
	}

	// Token: 0x04000016 RID: 22
	public PlayableDirector director;

	// Token: 0x04000017 RID: 23
	private bool isWaiting;

	// Token: 0x04000018 RID: 24
	private int continueTokens;
}
