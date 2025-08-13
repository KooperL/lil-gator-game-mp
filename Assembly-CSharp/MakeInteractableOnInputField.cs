using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002A0 RID: 672
public class MakeInteractableOnInputField : MonoBehaviour
{
	// Token: 0x06000E36 RID: 3638 RVA: 0x00044540 File Offset: 0x00042740
	private void Start()
	{
		this.inputField = base.GetComponent<InputField>();
		this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.OnInputFieldChange));
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x0004456A File Offset: 0x0004276A
	private void OnInputFieldChange(string inputFieldText)
	{
		this.button.interactable = !string.IsNullOrEmpty(inputFieldText);
	}

	// Token: 0x0400129B RID: 4763
	private InputField inputField;

	// Token: 0x0400129C RID: 4764
	public Button button;
}
