using System;
using UnityEngine;
using UnityEngine.Scripting;

// Token: 0x02000256 RID: 598
public class TriggerGC : MonoBehaviour
{
	// Token: 0x06000CEA RID: 3306 RVA: 0x0003E84C File Offset: 0x0003CA4C
	public static void TriggerGarbageCollection(bool isImmediate = true, bool alwaysRun = false)
	{
		if (Time.time - TriggerGC.lastGCCollectTime < 60f && !alwaysRun)
		{
			return;
		}
		if (isImmediate)
		{
			GC.Collect();
		}
		else
		{
			GarbageCollector.CollectIncremental(0UL);
		}
		TriggerGC.lastGCCollectTime = Time.time;
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0003E880 File Offset: 0x0003CA80
	private void OnEnable()
	{
		this.frameCount = 0;
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0003E889 File Offset: 0x0003CA89
	private void Update()
	{
		if (this.frameCount >= 2)
		{
			return;
		}
		this.frameCount++;
		if (this.frameCount >= 2)
		{
			TriggerGC.TriggerGarbageCollection(false, false);
		}
	}

	// Token: 0x04001109 RID: 4361
	public static float lastGCCollectTime = -1000f;

	// Token: 0x0400110A RID: 4362
	private const float minGCDelay = 60f;

	// Token: 0x0400110B RID: 4363
	private const int frameDelay = 2;

	// Token: 0x0400110C RID: 4364
	private int frameCount;
}
