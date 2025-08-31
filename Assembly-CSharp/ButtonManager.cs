using System;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
	// Token: 0x06000E60 RID: 3680 RVA: 0x00044C89 File Offset: 0x00042E89
	private void OnEnable()
	{
		ButtonManager.b = this;
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x00044C94 File Offset: 0x00042E94
	public Sprite GetButtonSprite(ButtonList button)
	{
		switch (this.buttonStyle)
		{
		case 0:
			return this.kbButtons[(int)button];
		case 1:
			return this.xbButtons[(int)button];
		case 2:
			return this.psButtons[(int)button];
		default:
			return null;
		}
	}

	public static ButtonManager b;

	public Sprite[] kbButtons;

	public Sprite[] xbButtons;

	public Sprite[] psButtons;

	private int buttonStyle;
}
