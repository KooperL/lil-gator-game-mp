using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MakeInteractableOnInputField : MonoBehaviour
{
	// Token: 0x0600115B RID: 4443 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
	private void Start()
	{
		this.inputField = base.GetComponent<InputField>();
		this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.OnInputFieldChange));
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x0000EDCA File Offset: 0x0000CFCA
	private void OnInputFieldChange(string inputFieldText)
	{
		this.button.interactable = !string.IsNullOrEmpty(inputFieldText);
	}

	private InputField inputField;

	public Button button;
}
