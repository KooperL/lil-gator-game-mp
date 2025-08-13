using System;
using UnityEngine;

// Token: 0x02000208 RID: 520
public class Prefabs : MonoBehaviour
{
	// Token: 0x170000EC RID: 236
	// (get) Token: 0x0600099F RID: 2463 RVA: 0x00009577 File Offset: 0x00007777
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

	// Token: 0x060009A0 RID: 2464 RVA: 0x00009595 File Offset: 0x00007795
	private void Awake()
	{
		if (Prefabs.instance == null)
		{
			Prefabs.instance = this;
		}
	}

	// Token: 0x04000C47 RID: 3143
	private static Prefabs instance;

	// Token: 0x04000C48 RID: 3144
	public GameObject loadingSequence;

	// Token: 0x04000C49 RID: 3145
	public GameObject loadingSequenceFade;

	// Token: 0x04000C4A RID: 3146
	public GameObject nameInput;

	// Token: 0x04000C4B RID: 3147
	public GameObject npcMarker;

	// Token: 0x04000C4C RID: 3148
	public GameObject continuousSound;

	// Token: 0x04000C4D RID: 3149
	public GameObject speedrunTimer;
}
