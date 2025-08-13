using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class UIPhoneDialogue : MonoBehaviour
{
	// Token: 0x06000F4F RID: 3919 RVA: 0x00049A7F File Offset: 0x00047C7F
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

	// Token: 0x06000F50 RID: 3920 RVA: 0x00049A9C File Offset: 0x00047C9C
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

	// Token: 0x06000F51 RID: 3921 RVA: 0x00049AB4 File Offset: 0x00047CB4
	public void Clear()
	{
		for (int i = 0; i < this.messagesParent.childCount; i++)
		{
			Object.Destroy(this.messagesParent.GetChild(i).gameObject);
		}
		this.oldMessages = new List<Transform>();
	}

	// Token: 0x04001421 RID: 5153
	public GameObject textMessageElement;

	// Token: 0x04001422 RID: 5154
	public GameObject playerTextMessageElement;

	// Token: 0x04001423 RID: 5155
	public GameObject systemTextMessageElement;

	// Token: 0x04001424 RID: 5156
	public GameObject nameplateElement;

	// Token: 0x04001425 RID: 5157
	private UINameplate lastNameplate;

	// Token: 0x04001426 RID: 5158
	public float messageMoveSpeed = 3f;

	// Token: 0x04001427 RID: 5159
	public float messageDelay = 0.5f;

	// Token: 0x04001428 RID: 5160
	public float messageGap = 5f;

	// Token: 0x04001429 RID: 5161
	public float differentCharacterGap = 50f;

	// Token: 0x0400142A RID: 5162
	private List<Transform> oldMessages;

	// Token: 0x0400142B RID: 5163
	public UIButtonPrompt leftButtonPrompt;

	// Token: 0x0400142C RID: 5164
	public UIButtonPrompt rightButtonPrompt;

	// Token: 0x0400142D RID: 5165
	public Vector2 buttonPromptOffset;

	// Token: 0x0400142E RID: 5166
	public Transform messagesParent;
}
