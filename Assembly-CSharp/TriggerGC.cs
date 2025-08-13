using System;
using UnityEngine;
using UnityEngine.Scripting;

// Token: 0x0200031C RID: 796
public class TriggerGC : MonoBehaviour
{
	// Token: 0x06000F97 RID: 3991 RVA: 0x0000D8D3 File Offset: 0x0000BAD3
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

	// Token: 0x06000F98 RID: 3992 RVA: 0x0000D907 File Offset: 0x0000BB07
	private void OnEnable()
	{
		this.frameCount = 0;
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x0000D910 File Offset: 0x0000BB10
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

	// Token: 0x04001425 RID: 5157
	public static float lastGCCollectTime = -1000f;

	// Token: 0x04001426 RID: 5158
	private const float minGCDelay = 60f;

	// Token: 0x04001427 RID: 5159
	private const int frameDelay = 2;

	// Token: 0x04001428 RID: 5160
	private int frameCount;
}
