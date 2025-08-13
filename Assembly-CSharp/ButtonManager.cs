using System;
using UnityEngine;

// Token: 0x02000382 RID: 898
public class ButtonManager : MonoBehaviour
{
	// Token: 0x06001130 RID: 4400 RVA: 0x0000EC03 File Offset: 0x0000CE03
	private void OnEnable()
	{
		ButtonManager.b = this;
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x0005660C File Offset: 0x0005480C
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

	// Token: 0x0400161A RID: 5658
	public static ButtonManager b;

	// Token: 0x0400161B RID: 5659
	public Sprite[] kbButtons;

	// Token: 0x0400161C RID: 5660
	public Sprite[] xbButtons;

	// Token: 0x0400161D RID: 5661
	public Sprite[] psButtons;

	// Token: 0x0400161E RID: 5662
	private int buttonStyle;
}
