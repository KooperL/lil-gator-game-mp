using System;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
	// Token: 0x06001191 RID: 4497 RVA: 0x0000EFF6 File Offset: 0x0000D1F6
	private void OnEnable()
	{
		ButtonManager.b = this;
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x00058894 File Offset: 0x00056A94
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
