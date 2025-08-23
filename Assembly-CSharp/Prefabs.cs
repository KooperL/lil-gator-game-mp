using System;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
	// (get) Token: 0x060009E7 RID: 2535 RVA: 0x000098B5 File Offset: 0x00007AB5
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

	// Token: 0x060009E8 RID: 2536 RVA: 0x000098D3 File Offset: 0x00007AD3
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
