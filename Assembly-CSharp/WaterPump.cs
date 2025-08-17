using System;
using UnityEngine;
using UnityEngine.Events;

public class WaterPump : MonoBehaviour
{
	// (get) Token: 0x06000140 RID: 320 RVA: 0x000031A2 File Offset: 0x000013A2
	// (set) Token: 0x06000141 RID: 321 RVA: 0x000031B5 File Offset: 0x000013B5
	public bool State
	{
		get
		{
			return GameData.g.ReadBool(this.pumpID, false);
		}
		set
		{
			GameData.g.Write(this.pumpID, value);
		}
	}

	// Token: 0x06000142 RID: 322 RVA: 0x000031C8 File Offset: 0x000013C8
	private void Start()
	{
		this.SetState(this.State);
	}

	// Token: 0x06000143 RID: 323 RVA: 0x000031D6 File Offset: 0x000013D6
	public void Activate()
	{
		if (this.activated)
		{
			return;
		}
		this.State = true;
		this.SetState(true);
		this.OnActivate.Invoke();
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0001BC64 File Offset: 0x00019E64
	private void SetState(bool active)
	{
		this.activated = active;
		for (int i = 0; i < this.pipes.Length; i++)
		{
			this.pipes[i].enabled = this.activated;
		}
		this.ripples.SetActive(active);
	}

	public bool activated;

	public Renderer[] pipes;

	public Material runningWater;

	public Material glass;

	public GameObject ripples;

	public string pumpID = "Pump";

	public UnityEvent OnActivate;
}
