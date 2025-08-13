using System;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200039B RID: 923
[CreateAssetMenu]
public class UIButtonObject : ScriptableObject
{
	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x06001187 RID: 4487 RVA: 0x0000EFEF File Offset: 0x0000D1EF
	// (set) Token: 0x06001188 RID: 4488 RVA: 0x0000EFF7 File Offset: 0x0000D1F7
	public int InputMode
	{
		get
		{
			return this.inputMode;
		}
		set
		{
			this.inputMode = value;
			if (this.onDeviceChanged != null)
			{
				this.onDeviceChanged.Invoke();
			}
		}
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x06001189 RID: 4489 RVA: 0x00057CBC File Offset: 0x00055EBC
	public Sprite InputSprite
	{
		get
		{
			switch (this.inputMode)
			{
			case 1:
				return this.kbm;
			case 2:
				return this.xb;
			case 3:
				return this.ps;
			case 4:
				return this.sw;
			default:
				return this.generic;
			}
		}
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x0000F013 File Offset: 0x0000D213
	private void OnEnable()
	{
		if (!InputHelper.activeButtonObjects.Contains(this))
		{
			InputHelper.activeButtonObjects.Add(this);
		}
	}

	// Token: 0x04001699 RID: 5785
	public Sprite generic;

	// Token: 0x0400169A RID: 5786
	public Sprite kbm;

	// Token: 0x0400169B RID: 5787
	public Sprite xb;

	// Token: 0x0400169C RID: 5788
	public Sprite ps;

	// Token: 0x0400169D RID: 5789
	public Sprite sw;

	// Token: 0x0400169E RID: 5790
	public UnityEvent onDeviceChanged;

	// Token: 0x0400169F RID: 5791
	public int inputMode;

	// Token: 0x040016A0 RID: 5792
	private Player rePlayer;
}
