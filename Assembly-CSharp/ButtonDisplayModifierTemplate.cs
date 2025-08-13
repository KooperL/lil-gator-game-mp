using System;
using UnityEngine;

// Token: 0x02000377 RID: 887
public class ButtonDisplayModifierTemplate : MonoBehaviour
{
	// Token: 0x060010F6 RID: 4342 RVA: 0x00056014 File Offset: 0x00054214
	public void LoadModifierAndText(string[] modifiers, string name)
	{
		foreach (string text in modifiers)
		{
			this.buttonTemplate.nameText.text = text;
			this.buttonTemplate = Object.Instantiate<GameObject>(this.buttonTemplate.gameObject, base.transform).GetComponent<ButtonDisplayTemplate>();
		}
		this.buttonTemplate.nameText.text = name;
	}

	// Token: 0x040015F3 RID: 5619
	public ButtonDisplayTemplate buttonTemplate;
}
