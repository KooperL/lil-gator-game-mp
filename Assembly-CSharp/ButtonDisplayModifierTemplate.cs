using System;
using UnityEngine;

public class ButtonDisplayModifierTemplate : MonoBehaviour
{
	// Token: 0x06001157 RID: 4439 RVA: 0x0005829C File Offset: 0x0005649C
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
