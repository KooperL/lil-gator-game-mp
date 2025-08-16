using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollingPhoneDisplay : MonoBehaviour
{
	// Token: 0x06001301 RID: 4865 RVA: 0x0005DBA0 File Offset: 0x0005BDA0
	private void Awake()
	{
		this.textGenerationSettings = this.textTemplate.GetGenerationSettings(this.textTemplate.rectTransform.rect.size);
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x0005DBD8 File Offset: 0x0005BDD8
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

	// Token: 0x06001303 RID: 4867 RVA: 0x0005DC28 File Offset: 0x0005BE28
	private void OnDisable()
	{
		if (this.contents != null && this.contents.Count > 0)
		{
			foreach (GameObject gameObject in this.contents)
			{
				if (gameObject != null)
				{
					global::UnityEngine.Object.Destroy(gameObject);
				}
			}
			this.contents.Clear();
		}
		this.position = (this.velocity = 0f);
		this.scrollingArea.anchoredPosition = Vector3.zero;
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x0005DCD0 File Offset: 0x0005BED0
	private void InsertName(CharacterProfile character)
	{
		if (!character.isPlayer && this.lastCharacterDisplayed != character)
		{
			UICharacterDisplay component = global::UnityEngine.Object.Instantiate<GameObject>(this.nameDisplay, this.scrollingArea).GetComponent<UICharacterDisplay>();
			component.Load(character);
			this.contents.Add(component.gameObject);
		}
		this.lastCharacterDisplayed = character;
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x000100C3 File Offset: 0x0000E2C3
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
		UITextBox newTextbox = global::UnityEngine.Object.Instantiate<GameObject>(gameObject, this.scrollingArea).GetComponent<UITextBox>();
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

	// Token: 0x06001306 RID: 4870 RVA: 0x000100E7 File Offset: 0x0000E2E7
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
		UIImageBox newImageBox = global::UnityEngine.Object.Instantiate<GameObject>(gameObject, this.scrollingArea).GetComponent<UIImageBox>();
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

	// Token: 0x06001307 RID: 4871 RVA: 0x00010113 File Offset: 0x0000E313
	public void ClearPhone()
	{
		this.phone.Deactivate();
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x0005DD2C File Offset: 0x0005BF2C
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
