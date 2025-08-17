using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MakeInteractableOnInputField : MonoBehaviour
{
	// Token: 0x0600115A RID: 4442 RVA: 0x0000ED96 File Offset: 0x0000CF96
	private void Start()
	{
		this.inputField = base.GetComponent<InputField>();
		this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.OnInputFieldChange));
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
	private void OnInputFieldChange(string inputFieldText)
	{
		this.button.interactable = !string.IsNullOrEmpty(inputFieldText);
	}

	private InputField inputField;

	public Button button;
}
