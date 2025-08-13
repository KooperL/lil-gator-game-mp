using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003BF RID: 959
public class UIPhoneDialogue : MonoBehaviour
{
	// Token: 0x06001257 RID: 4695 RVA: 0x0000F88A File Offset: 0x0000DA8A
	public IEnumerator DisplayMessage(string text, CharacterProfile character)
	{
		if (this.oldMessages == null)
		{
			this.oldMessages = new List<Transform>();
		}
		UINameplate uinameplate = null;
		bool isNewCharacter = false;
		GameObject newTextMessage;
		if (character.isPlayer)
		{
			newTextMessage = Object.Instantiate<GameObject>(this.playerTextMessageElement, this.messagesParent);
		}
		else
		{
			newTextMessage = Object.Instantiate<GameObject>(this.textMessageElement, this.messagesParent);
			if (this.nameplateElement != null)
			{
				if (this.lastNameplate == null || this.lastNameplate.character != character)
				{
					uinameplate = Object.Instantiate<GameObject>(this.nameplateElement, this.messagesParent).GetComponent<UINameplate>();
					uinameplate.SetNameplate(character);
				}
				else
				{
					uinameplate = this.lastNameplate;
				}
			}
		}
		if (uinameplate != this.lastNameplate)
		{
			isNewCharacter = true;
			if (this.lastNameplate != null)
			{
				this.oldMessages.Add(this.lastNameplate.transform);
			}
			this.lastNameplate = uinameplate;
		}
		UITextBox textBox = newTextMessage.GetComponent<UITextBox>();
		textBox.SetText("...");
		textBox.SetColor(character.darkColor);
		float time = Time.time;
		yield return null;
		float num = textBox.Height;
		num += (isNewCharacter ? this.differentCharacterGap : this.messageGap);
		Coroutine shiftMessages = base.StartCoroutine(this.ShiftMessages(num));
		float num2 = Time.time - time;
		if (num2 < this.messageDelay)
		{
			yield return new WaitForSeconds(this.messageDelay - num2);
		}
		textBox.SetText(text);
		yield return shiftMessages;
		yield return null;
		this.oldMessages.Add(newTextMessage.transform);
		UIButtonPrompt buttonPrompt = (character.isPlayer ? this.rightButtonPrompt : this.leftButtonPrompt);
		if (buttonPrompt != null)
		{
			buttonPrompt.gameObject.SetActive(true);
			Rect rect = newTextMessage.GetComponent<RectTransform>().rect;
			buttonPrompt.GetComponent<RectTransform>().anchoredPosition = new Vector2(rect.xMax, rect.yMin) + this.buttonPromptOffset;
			yield return buttonPrompt.waitUntilTriggered;
			buttonPrompt.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x0000F8A7 File Offset: 0x0000DAA7
	private IEnumerator ShiftMessages(float height)
	{
		float offset = 0f;
		while (offset < height)
		{
			float num = Mathf.Min(this.messageMoveSpeed * Time.deltaTime, height - offset);
			offset += num;
			for (int i = 0; i < this.oldMessages.Count; i++)
			{
				this.oldMessages[i].localPosition += num * Vector3.up;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x0005AD38 File Offset: 0x00058F38
	public void Clear()
	{
		for (int i = 0; i < this.messagesParent.childCount; i++)
		{
			Object.Destroy(this.messagesParent.GetChild(i).gameObject);
		}
		this.oldMessages = new List<Transform>();
	}

	// Token: 0x040017B6 RID: 6070
	public GameObject textMessageElement;

	// Token: 0x040017B7 RID: 6071
	public GameObject playerTextMessageElement;

	// Token: 0x040017B8 RID: 6072
	public GameObject systemTextMessageElement;

	// Token: 0x040017B9 RID: 6073
	public GameObject nameplateElement;

	// Token: 0x040017BA RID: 6074
	private UINameplate lastNameplate;

	// Token: 0x040017BB RID: 6075
	public float messageMoveSpeed = 3f;

	// Token: 0x040017BC RID: 6076
	public float messageDelay = 0.5f;

	// Token: 0x040017BD RID: 6077
	public float messageGap = 5f;

	// Token: 0x040017BE RID: 6078
	public float differentCharacterGap = 50f;

	// Token: 0x040017BF RID: 6079
	private List<Transform> oldMessages;

	// Token: 0x040017C0 RID: 6080
	public UIButtonPrompt leftButtonPrompt;

	// Token: 0x040017C1 RID: 6081
	public UIButtonPrompt rightButtonPrompt;

	// Token: 0x040017C2 RID: 6082
	public Vector2 buttonPromptOffset;

	// Token: 0x040017C3 RID: 6083
	public Transform messagesParent;
}
