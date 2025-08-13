using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000059 RID: 89
public class WaterPump : MonoBehaviour
{
	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000138 RID: 312 RVA: 0x000030FF File Offset: 0x000012FF
	// (set) Token: 0x06000139 RID: 313 RVA: 0x00003112 File Offset: 0x00001312
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

	// Token: 0x0600013A RID: 314 RVA: 0x00003125 File Offset: 0x00001325
	private void Start()
	{
		this.SetState(this.State);
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00003133 File Offset: 0x00001333
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

	// Token: 0x0600013C RID: 316 RVA: 0x0001B450 File Offset: 0x00019650
	private void SetState(bool active)
	{
		this.activated = active;
		for (int i = 0; i < this.pipes.Length; i++)
		{
			this.pipes[i].enabled = this.activated;
		}
		this.ripples.SetActive(active);
	}

	// Token: 0x040001CC RID: 460
	public bool activated;

	// Token: 0x040001CD RID: 461
	public Renderer[] pipes;

	// Token: 0x040001CE RID: 462
	public Material runningWater;

	// Token: 0x040001CF RID: 463
	public Material glass;

	// Token: 0x040001D0 RID: 464
	public GameObject ripples;

	// Token: 0x040001D1 RID: 465
	public string pumpID = "Pump";

	// Token: 0x040001D2 RID: 466
	public UnityEvent OnActivate;
}
