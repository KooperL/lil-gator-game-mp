using System;
using UnityEngine;
using UnityEngine.Scripting;

public class TriggerGC : MonoBehaviour
{
	// Token: 0x06000FF2 RID: 4082 RVA: 0x0000DC27 File Offset: 0x0000BE27
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

	// Token: 0x06000FF3 RID: 4083 RVA: 0x0000DC5B File Offset: 0x0000BE5B
	private void OnEnable()
	{
		this.frameCount = 0;
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x0000DC64 File Offset: 0x0000BE64
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

	public static float lastGCCollectTime = -1000f;

	private const float minGCDelay = 60f;

	private const int frameDelay = 2;

	private int frameCount;
}
