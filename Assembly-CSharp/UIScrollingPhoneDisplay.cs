using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003CB RID: 971
public class UIScrollingPhoneDisplay : MonoBehaviour
{
	// Token: 0x060012A1 RID: 4769 RVA: 0x0005BD0C File Offset: 0x00059F0C
	private void Awake()
	{
		this.textGenerationSettings = this.textTemplate.GetGenerationSettings(this.textTemplate.rectTransform.rect.size);
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x0005BD44 File Offset: 0x00059F44
	private void OnEnable()
	{
		if (this.contents == null)
		{
			this.contents = new List<GameObject>();
		}
		this.lastCharacterDisplayed = null;
		this.position = (this.velocity = 0f);
		this.scrollingArea.anchoredPosition = Vector3.zero;
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x0005BD94 File Offset: 0x00059F94
	private void OnDisable()
	{
		if (this.contents != null && this.contents.Count > 0)
		{
			foreach (GameObject gameObject in this.contents)
			{
				if (gameObject != null)
				{
					Object.Destroy(gameObject);
				}
			}
			this.contents.Clear();
		}
		this.position = (this.velocity = 0f);
		this.scrollingArea.anchoredPosition = Vector3.zero;
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x0005BE3C File Offset: 0x0005A03C
	private void InsertName(CharacterProfile character)
	{
		if (!character.isPlayer && this.lastCharacterDisplayed != character)
		{
			UICharacterDisplay component = Object.Instantiate<GameObject>(this.nameDisplay, this.scrollingArea).GetComponent<UICharacterDisplay>();
			component.Load(character);
			this.contents.Add(component.gameObject);
		}
		this.lastCharacterDisplayed = character;
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x0000FCDB File Offset: 0x0000DEDB
	public IEnumerator DisplayTextMessage(string message, CharacterProfile character, bool displayNames = true)
	{
		if (!base.gameObject.activeSelf)
		{
			this.phone.Activate();
		}
		if (displayNames && character != null)
		{
			this.InsertName(character);
		}
		message = DialogueBox.InsertLineBreaks(message, this.textGenerationSettings);
		GameObject gameObject = ((character == null) ? this.textSystemPrefab : (character.isPlayer ? this.textRightPrefab : this.textLeftPrefab));
		UITextBox newTextbox = Object.Instantiate<GameObject>(gameObject, this.scrollingArea).GetComponent<UITextBox>();
		if (character != null)
		{
			newTextbox.SetColor(character.darkColor);
			newTextbox.SetText("...");
			yield return new WaitForSeconds(0.25f);
		}
		newTextbox.SetText(message);
		this.messageSound.Play();
		this.contents.Add(newTextbox.gameObject);
		this.buttonPrompt.gameObject.SetActive(true);
		yield return this.buttonPrompt.waitUntilTriggered;
		this.buttonPrompt.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x0000FCFF File Offset: 0x0000DEFF
	public IEnumerator DisplayImage(Sprite image, CharacterProfile character, bool displayNames = true, bool clearAfter = true)
	{
		if (!base.gameObject.activeSelf)
		{
			this.phone.Activate();
		}
		if (displayNames)
		{
			this.InsertName(character);
		}
		GameObject gameObject = (character.isPlayer ? this.imageRightPrefab : this.imageLeftPrefab);
		UIImageBox newImageBox = Object.Instantiate<GameObject>(gameObject, this.scrollingArea).GetComponent<UIImageBox>();
		newImageBox.SetColor(character.darkColor);
		newImageBox.SetImage(this.loadingImage);
		yield return new WaitForSeconds(0.25f);
		newImageBox.SetImage(image);
		this.messageSound.Play();
		this.contents.Add(newImageBox.gameObject);
		this.buttonPrompt.gameObject.SetActive(true);
		yield return this.buttonPrompt.waitUntilTriggered;
		this.buttonPrompt.gameObject.SetActive(false);
		if (clearAfter)
		{
			this.ClearPhone();
		}
		yield break;
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x0000FD2B File Offset: 0x0000DF2B
	public void ClearPhone()
	{
		this.phone.Deactivate();
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x0005BE98 File Offset: 0x0005A098
	private void Update()
	{
		this.position = Mathf.SmoothDamp(this.position, this.scrollingArea.rect.height, ref this.velocity, 0.05f);
		this.scrollingArea.anchoredPosition = this.position * Vector3.up;
	}

	// Token: 0x04001805 RID: 6149
	public UIPhone phone;

	// Token: 0x04001806 RID: 6150
	public RectTransform scrollingArea;

	// Token: 0x04001807 RID: 6151
	public GameObject textRightPrefab;

	// Token: 0x04001808 RID: 6152
	public GameObject textLeftPrefab;

	// Token: 0x04001809 RID: 6153
	public GameObject textSystemPrefab;

	// Token: 0x0400180A RID: 6154
	public GameObject imageRightPrefab;

	// Token: 0x0400180B RID: 6155
	public GameObject imageLeftPrefab;

	// Token: 0x0400180C RID: 6156
	public Text textTemplate;

	// Token: 0x0400180D RID: 6157
	private TextGenerationSettings textGenerationSettings;

	// Token: 0x0400180E RID: 6158
	public GameObject nameDisplay;

	// Token: 0x0400180F RID: 6159
	public UIButtonPrompt buttonPrompt;

	// Token: 0x04001810 RID: 6160
	private List<GameObject> contents = new List<GameObject>();

	// Token: 0x04001811 RID: 6161
	private CharacterProfile lastCharacterDisplayed;

	// Token: 0x04001812 RID: 6162
	public Sprite loadingImage;

	// Token: 0x04001813 RID: 6163
	public AudioSourceVariance messageSound;

	// Token: 0x04001814 RID: 6164
	private float position;

	// Token: 0x04001815 RID: 6165
	private float velocity;
}
