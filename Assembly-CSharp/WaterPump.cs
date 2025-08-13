using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000046 RID: 70
public class WaterPump : MonoBehaviour
{
	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000113 RID: 275 RVA: 0x00006D17 File Offset: 0x00004F17
	// (set) Token: 0x06000114 RID: 276 RVA: 0x00006D2A File Offset: 0x00004F2A
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

	// Token: 0x06000115 RID: 277 RVA: 0x00006D3D File Offset: 0x00004F3D
	private void Start()
	{
		this.SetState(this.State);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00006D4B File Offset: 0x00004F4B
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

	// Token: 0x06000117 RID: 279 RVA: 0x00006D70 File Offset: 0x00004F70
	private void SetState(bool active)
	{
		this.activated = active;
		for (int i = 0; i < this.pipes.Length; i++)
		{
			this.pipes[i].enabled = this.activated;
		}
		this.ripples.SetActive(active);
	}

	// Token: 0x04000182 RID: 386
	public bool activated;

	// Token: 0x04000183 RID: 387
	public Renderer[] pipes;

	// Token: 0x04000184 RID: 388
	public Material runningWater;

	// Token: 0x04000185 RID: 389
	public Material glass;

	// Token: 0x04000186 RID: 390
	public GameObject ripples;

	// Token: 0x04000187 RID: 391
	public string pumpID = "Pump";

	// Token: 0x04000188 RID: 392
	public UnityEvent OnActivate;
}
