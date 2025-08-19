using System;
using UnityEngine;

public class ButtonDisplayModifierTemplate : MonoBehaviour
{
	// Token: 0x06001156 RID: 4438 RVA: 0x00057FB0 File Offset: 0x000561B0
	public void LoadModifierAndText(string[] modifiers, string name)
	{
		foreach (string text in modifiers)
		{
			this.buttonTemplate.nameText.text = text;
			this.buttonTemplate = global::UnityEngine.Object.Instantiate<GameObject>(this.buttonTemplate.gameObject, base.transform).GetComponent<ButtonDisplayTemplate>();
		}
		this.buttonTemplate.nameText.text = name;
	}

	public ButtonDisplayTemplate buttonTemplate;
}
