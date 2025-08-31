using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public UIPhone phone;

	public RectTransform scrollingArea;

	public GameObject textRightPrefab;

	public GameObject textLeftPrefab;

	public GameObject textSystemPrefab;

	public GameObject imageRightPrefab;

	public GameObject imageLeftPrefab;

	public Text textTemplate;

	private TextGenerationSettings textGenerationSettings;

	public GameObject nameDisplay;

	public UIButtonPrompt buttonPrompt;

	private List<GameObject> contents = new List<GameObject>();

	private CharacterProfile lastCharacterDisplayed;

	public Sprite loadingImage;

	public AudioSourceVariance messageSound;

	private float position;

	private float velocity;
}
