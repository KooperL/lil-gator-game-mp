using System;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
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

	private static Prefabs instance;

	public GameObject loadingSequence;

	public GameObject loadingSequenceFade;

	public GameObject nameInput;

	public GameObject npcMarker;

	public GameObject continuousSound;

	public GameObject speedrunTimer;
}
