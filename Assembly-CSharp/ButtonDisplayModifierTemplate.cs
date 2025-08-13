using System;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class ButtonDisplayModifierTemplate : MonoBehaviour
{
	// Token: 0x06000E32 RID: 3634 RVA: 0x000444C4 File Offset: 0x000426C4
	public void LoadModifierAndText(string[] modifiers, string name)
	{
		foreach (string text in modifiers)
		{
			this.buttonTemplate.nameText.text = text;
			this.buttonTemplate = Object.Instantiate<GameObject>(this.buttonTemplate.gameObject, base.transform).GetComponent<ButtonDisplayTemplate>();
		}
		this.buttonTemplate.nameText.text = name;
	}

	// Token: 0x04001296 RID: 4758
	public ButtonDisplayTemplate buttonTemplate;
}
