using System;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002B9 RID: 697
[CreateAssetMenu]
public class UIButtonObject : ScriptableObject
{
	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0004665C File Offset: 0x0004485C
	// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x00046664 File Offset: 0x00044864
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

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00046680 File Offset: 0x00044880
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

	// Token: 0x06000EB2 RID: 3762 RVA: 0x000466D0 File Offset: 0x000448D0
	private void Awake()
	{
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x000466D2 File Offset: 0x000448D2
	private void OnEnable()
	{
		if (!InputHelper.activeButtonObjects.Contains(this))
		{
			InputHelper.activeButtonObjects.Add(this);
		}
	}

	// Token: 0x04001323 RID: 4899
	public Sprite generic;

	// Token: 0x04001324 RID: 4900
	public Sprite kbm;

	// Token: 0x04001325 RID: 4901
	public Sprite xb;

	// Token: 0x04001326 RID: 4902
	public Sprite ps;

	// Token: 0x04001327 RID: 4903
	public Sprite sw;

	// Token: 0x04001328 RID: 4904
	public UnityEvent onDeviceChanged;

	// Token: 0x04001329 RID: 4905
	public int inputMode;

	// Token: 0x0400132A RID: 4906
	private global::Rewired.Player rePlayer;
}
