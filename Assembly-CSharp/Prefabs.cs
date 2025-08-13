using System;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class Prefabs : MonoBehaviour
{
	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000830 RID: 2096 RVA: 0x000272D5 File Offset: 0x000254D5
	public static Prefabs p
	{
		get
		{
			if (Prefabs.instance == null)
			{
				Prefabs.instance = Object.FindObjectOfType<Prefabs>();
			}
			return Prefabs.instance;
		}
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x000272F3 File Offset: 0x000254F3
	private void Awake()
	{
		if (Prefabs.instance == null)
		{
			Prefabs.instance = this;
		}
	}

	// Token: 0x04000A5B RID: 2651
	private static Prefabs instance;

	// Token: 0x04000A5C RID: 2652
	public GameObject loadingSequence;

	// Token: 0x04000A5D RID: 2653
	public GameObject loadingSequenceFade;

	// Token: 0x04000A5E RID: 2654
	public GameObject nameInput;

	// Token: 0x04000A5F RID: 2655
	public GameObject npcMarker;

	// Token: 0x04000A60 RID: 2656
	public GameObject continuousSound;

	// Token: 0x04000A61 RID: 2657
	public GameObject speedrunTimer;
}
