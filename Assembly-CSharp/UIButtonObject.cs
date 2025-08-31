using System;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class UIButtonObject : ScriptableObject
{
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

	public Sprite generic;

	public Sprite kbm;

	public Sprite xb;

	public Sprite ps;

	public Sprite sw;

	public UnityEvent onDeviceChanged;

	public int inputMode;

	private global::Rewired.Player rePlayer;
}
