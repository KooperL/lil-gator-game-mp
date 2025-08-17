using System;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
	// (get) Token: 0x060009E6 RID: 2534 RVA: 0x000098AB File Offset: 0x00007AAB
	public static Prefabs p
	{
		get
		{
			if (Prefabs.instance == null)
			{
				Prefabs.instance = global::UnityEngine.Object.FindObjectOfType<Prefabs>();
			}
			return Prefabs.instance;
		}
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x000098C9 File Offset: 0x00007AC9
	private void Awake()
	{
		if (Prefabs.instance == null)
		{
			Prefabs.instance = this;
		}
	}

	private static Prefabs instance;

	public GameObject loadingSequence;

	public GameObject loadingSequenceFade;

	public GameObject nameInput;

	public GameObject npcMarker;

	public GameObject continuousSound;

	public GameObject speedrunTimer;
}
