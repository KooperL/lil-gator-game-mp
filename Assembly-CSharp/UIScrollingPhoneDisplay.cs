using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002DF RID: 735
public class UIScrollingPhoneDisplay : MonoBehaviour
{
	// Token: 0x06000F8D RID: 3981 RVA: 0x0004AB38 File Offset: 0x00048D38
	private void Awake()
	{
		this.textGenerationSettings = this.textTemplate.GetGenerationSettings(this.textTemplate.rectTransform.rect.size);
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x0004AB70 File Offset: 0x00048D70
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

	// Token: 0x06000F8F RID: 3983 RVA: 0x0004ABC0 File Offset: 0x00048DC0
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

	// Token: 0x06000F90 RID: 3984 RVA: 0x0004AC68 File Offset: 0x00048E68
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

	// Token: 0x06000F91 RID: 3985 RVA: 0x0004ACC1 File Offset: 0x00048EC1
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

	// Token: 0x06000F92 RID: 3986 RVA: 0x0004ACE5 File Offset: 0x00048EE5
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

	// Token: 0x06000F93 RID: 3987 RVA: 0x0004AD11 File Offset: 0x00048F11
	public void ClearPhone()
	{
		this.phone.Deactivate();
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x0004AD20 File Offset: 0x00048F20
	private void Update()
	{
		this.position = Mathf.SmoothDamp(this.position, this.scrollingArea.rect.height, ref this.velocity, 0.05f);
		this.scrollingArea.anchoredPosition = this.position * Vector3.up;
	}

	// Token: 0x04001462 RID: 5218
	public UIPhone phone;

	// Token: 0x04001463 RID: 5219
	public RectTransform scrollingArea;

	// Token: 0x04001464 RID: 5220
	public GameObject textRightPrefab;

	// Token: 0x04001465 RID: 5221
	public GameObject textLeftPrefab;

	// Token: 0x04001466 RID: 5222
	public GameObject textSystemPrefab;

	// Token: 0x04001467 RID: 5223
	public GameObject imageRightPrefab;

	// Token: 0x04001468 RID: 5224
	public GameObject imageLeftPrefab;

	// Token: 0x04001469 RID: 5225
	public Text textTemplate;

	// Token: 0x0400146A RID: 5226
	private TextGenerationSettings textGenerationSettings;

	// Token: 0x0400146B RID: 5227
	public GameObject nameDisplay;

	// Token: 0x0400146C RID: 5228
	public UIButtonPrompt buttonPrompt;

	// Token: 0x0400146D RID: 5229
	private List<GameObject> contents = new List<GameObject>();

	// Token: 0x0400146E RID: 5230
	private CharacterProfile lastCharacterDisplayed;

	// Token: 0x0400146F RID: 5231
	public Sprite loadingImage;

	// Token: 0x04001470 RID: 5232
	public AudioSourceVariance messageSound;

	// Token: 0x04001471 RID: 5233
	private float position;

	// Token: 0x04001472 RID: 5234
	private float velocity;
}
