using System;
using UnityEngine;

// Token: 0x020002A5 RID: 677
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

	// Token: 0x040012B2 RID: 4786
	public static ButtonManager b;

	// Token: 0x040012B3 RID: 4787
	public Sprite[] kbButtons;

	// Token: 0x040012B4 RID: 4788
	public Sprite[] xbButtons;

	// Token: 0x040012B5 RID: 4789
	public Sprite[] psButtons;

	// Token: 0x040012B6 RID: 4790
	private int buttonStyle;
}
