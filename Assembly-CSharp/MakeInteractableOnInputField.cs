using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200037A RID: 890
public class MakeInteractableOnInputField : MonoBehaviour
{
	// Token: 0x060010FA RID: 4346 RVA: 0x0000E9AD File Offset: 0x0000CBAD
	private void Start()
	{
		this.inputField = base.GetComponent<InputField>();
		this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.OnInputFieldChange));
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x0000E9D7 File Offset: 0x0000CBD7
	private void OnInputFieldChange(string inputFieldText)
	{
		this.button.interactable = !string.IsNullOrEmpty(inputFieldText);
	}

	// Token: 0x040015F8 RID: 5624
	private InputField inputField;

	// Token: 0x040015F9 RID: 5625
	public Button button;
}
