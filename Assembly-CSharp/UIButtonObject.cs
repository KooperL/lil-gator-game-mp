using System;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class UIButtonObject : ScriptableObject
{
	// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0000F3E2 File Offset: 0x0000D5E2
	// (set) Token: 0x060011E9 RID: 4585 RVA: 0x0000F3EA File Offset: 0x0000D5EA
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

	// (get) Token: 0x060011EA RID: 4586 RVA: 0x00059F48 File Offset: 0x00058148
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

	// Token: 0x060011EB RID: 4587 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x0000F406 File Offset: 0x0000D606
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
