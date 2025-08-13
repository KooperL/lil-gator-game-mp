using System;
using System.Collections;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DE RID: 222
public class DialogueBox : MonoBehaviour
{
	// Token: 0x060003E9 RID: 1001 RVA: 0x00004E9A File Offset: 0x0000309A
	private static bool IsValidBreakSpot(char breakCharacter)
	{
		return breakCharacter == ' ';
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00027FA8 File Offset: 0x000261A8
	public static string InsertLineBreaks(string text, TextGenerationSettings settings)
	{
		string text2 = text;
		TextGenerator textGenerator = new TextGenerator();
		textGenerator.Populate(text, settings);
		int num = 0;
		int lineCount = textGenerator.lineCount;
		for (int i = 0; i < lineCount - 1; i++)
		{
			int num2 = num + Mathf.FloorToInt((float)(text.Length - num) / (float)(lineCount - i));
			int num3 = -1;
			if (DialogueBox.IsValidBreakSpot(text[num2]))
			{
				num3 = num2;
			}
			int num5;
			int num4 = (num5 = num2);
			while (num3 == -1 && (num5 > num || num4 < text.Length))
			{
				num5--;
				num4++;
				if (num5 > num && DialogueBox.IsValidBreakSpot(text[num5]))
				{
					num3 = num5;
				}
				else if (num4 < text.Length && DialogueBox.IsValidBreakSpot(text[num4]))
				{
					num3 = num4;
				}
			}
			if (num3 == -1)
			{
				break;
			}
			if (text[num3] == ' ')
			{
				text = text.Remove(num3, 1);
			}
			text = text.Insert(num3, Environment.NewLine);
			num = num3;
		}
		TextGenerator textGenerator2 = new TextGenerator();
		textGenerator2.Populate(text, settings);
		if (textGenerator2.lineCount != textGenerator.lineCount)
		{
			return text2;
		}
		return text;
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060003EB RID: 1003 RVA: 0x00004EA1 File Offset: 0x000030A1
	public bool isTypingText
	{
		get
		{
			return this.typeTextCoroutine != null;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060003EC RID: 1004 RVA: 0x000280C4 File Offset: 0x000262C4
	public TextGenerationSettings TextGenerationSettings
	{
		get
		{
			if (!this.cachedTextGenerationSettings)
			{
				this.cachedTextGenerationSettings = true;
				if (this.textTemplate != null)
				{
					this.textGenerationSettings = this.textTemplate.GetGenerationSettings(this.textTemplate.rectTransform.rect.size);
				}
				else
				{
					Text text = this.visibleText;
					if (this.invisibleText != null)
					{
						text = this.invisibleText;
					}
					if (this.textArea != null)
					{
						this.textGenerationSettings = text.GetGenerationSettings(this.textArea.rect.size);
					}
					else
					{
						this.textGenerationSettings = text.GetGenerationSettings(new Vector2(250f, 64f));
					}
				}
			}
			return this.textGenerationSettings;
		}
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00028188 File Offset: 0x00026388
	private void Awake()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.animator = base.GetComponent<Animator>();
		this.rectTransform = base.transform as RectTransform;
		if (this.textSpeed > 0f)
		{
			this.textWaitTime = new WaitForSeconds(1f / this.textSpeed);
		}
		this.commaWait = new WaitForSeconds(this.commaWaitTime);
		this.fullStopWait = new WaitForSeconds(this.fullStopWaitTime);
		this.waitForTextBeforeInput = new WaitForSeconds(0.025f);
		this.textWaitPause = new WaitForSeconds(0.5f);
		this.waitForAutomaticTextAdvance = new WaitForSeconds(this.automaticTextAdvanceDelay);
		this.waitUntilSkipOrFinish = new WaitUntil(() => this.typeTextCoroutine == null || this.progressInput || DebugButtons.IsSkipHeld);
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00002229 File Offset: 0x00000429
	private void OnEnable()
	{
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00004EAC File Offset: 0x000030AC
	private void Start()
	{
		this.defaultPosition = this.rectTransform.anchoredPosition;
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00004EBF File Offset: 0x000030BF
	private void OnDisable()
	{
		if (PlayerInteract.interactButtonPriority == base.gameObject)
		{
			PlayerInteract.interactButtonPriority = null;
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00028254 File Offset: 0x00026454
	public YieldInstruction Load(string text, DialogueActor actor = null, bool hasInput = true, float waitTime = 0f)
	{
		bool flag = this.actor == actor;
		this.actor = actor;
		Color color = Color.grey;
		Color color2 = Color.black;
		Sprite sprite = null;
		Sprite sprite2 = null;
		if (actor != null && actor.profile != null)
		{
			color = actor.profile.brightColor;
			color2 = actor.profile.darkColor;
			sprite = actor.profile.dialogueDecoration;
			sprite2 = actor.profile.pattern;
			this.visibleText.color = actor.profile.darkColor;
		}
		if (this.backgroundImages != null && this.backgroundImages.Length != 0)
		{
			for (int i = 0; i < this.backgroundImages.Length; i++)
			{
				this.backgroundImages[i].color = color2;
			}
		}
		return this.Load(text, color, sprite, flag, hasInput, sprite2, waitTime);
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0002832C File Offset: 0x0002652C
	public YieldInstruction Load(string text, Color color, Sprite decoration = null, bool ignoreAnimation = false, bool hasInput = true, Sprite pattern = null, float waitTime = 0f)
	{
		string text2 = text;
		if (this.insertLineBreaks && this.visibleText != null)
		{
			text = DialogueBox.InsertLineBreaks(text, this.TextGenerationSettings);
		}
		this.hasInput = hasInput;
		this.text = text;
		this.overrideWaitTime = waitTime != 0f;
		this.overriddenWaitTime = waitTime;
		if (this.nameplate != null)
		{
			if (this.actor != null && this.actor.profile != null && !string.IsNullOrEmpty(this.actor.profile.Name))
			{
				this.nameplate.SetNameplate(this.actor.profile);
			}
			else
			{
				this.nameplate.Clear();
			}
		}
		if (this.buttonPrompt != null)
		{
			this.buttonPrompt.gameObject.SetActive(false);
		}
		base.gameObject.SetActive(true);
		this.clearTime = -1f;
		if (this.invisibleText != null)
		{
			if (this.invisibleTextIsUnformatted || this.useInvisibleText)
			{
				this.invisibleText.text = text2;
			}
			else
			{
				this.invisibleText.text = text;
				this.invisibleText.horizontalOverflow = 1;
			}
		}
		if (this.textSpeed > 0f)
		{
			if (this.typeTextCoroutine != null)
			{
				base.StopCoroutine(this.typeTextCoroutine);
			}
			this.typeTextCoroutine = this.TypeText(text);
			base.StartCoroutine(this.typeTextCoroutine);
		}
		else
		{
			this.visibleText.text = text;
			this.typeTextCoroutine = null;
		}
		if (this.animator != null && (!ignoreAnimation || this.alwaysTriggerAnimation))
		{
			this.animator.SetTrigger("Open");
		}
		if (this.follow != null)
		{
			if (this.actor != null)
			{
				this.follow.enabled = true;
				this.follow.SetTarget(this.actor);
			}
			else
			{
				this.follow.enabled = false;
				this.rectTransform.anchoredPosition = this.defaultPosition;
			}
		}
		if (this.lineFollow != null)
		{
			if (this.actor != null)
			{
				this.lineFollow.gameObject.SetActive(true);
				this.lineFollow.SetTarget(this.actor);
			}
			else
			{
				this.follow.gameObject.SetActive(false);
				this.rectTransform.anchoredPosition = this.defaultPosition;
			}
		}
		this.SetColor(color);
		this.SetDecorations(decoration);
		this.SetPattern(pattern);
		return base.StartCoroutine(this.RunDialogue());
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x000285C4 File Offset: 0x000267C4
	public void SetColor(Color color)
	{
		if (this.coloredImage != null)
		{
			this.coloredImage.color = color;
		}
		if (this.primaryImages != null && this.primaryImages.Length != 0)
		{
			for (int i = 0; i < this.primaryImages.Length; i++)
			{
				this.primaryImages[i].color = color;
			}
		}
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00028620 File Offset: 0x00026820
	public void SetDecorations(Sprite decoration)
	{
		if (this.decorations == null || this.decorations.Length == 0)
		{
			return;
		}
		for (int i = 0; i < this.decorations.Length; i++)
		{
			if (decoration != null)
			{
				this.decorations[i].enabled = true;
				this.decorations[i].sprite = decoration;
			}
			else
			{
				this.decorations[i].enabled = false;
			}
		}
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00028688 File Offset: 0x00026888
	public void SetPattern(Sprite sprite)
	{
		if (this.pattern == null)
		{
			return;
		}
		if (sprite != null)
		{
			this.pattern.gameObject.SetActive(true);
			this.pattern.sprite = sprite;
			return;
		}
		this.pattern.gameObject.SetActive(false);
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00004ED9 File Offset: 0x000030D9
	private IEnumerator RunDialogue()
	{
		float dialogueStartTime = Time.time;
		if (this.typeTextCoroutine != null)
		{
			this.progressInput = false;
			if (this.listenForSkip && this.hasInput && !this.overrideWaitTime)
			{
				yield return this.waitForTextBeforeInput;
				PlayerInteract.interactButtonPriority = base.gameObject;
			}
			yield return this.waitUntilSkipOrFinish;
			if (PlayerInteract.interactButtonPriority == base.gameObject)
			{
				PlayerInteract.interactButtonPriority = null;
			}
			if (this.typeTextCoroutine != null)
			{
				this.SkipText();
			}
		}
		if (this.overrideWaitTime)
		{
			float num = this.overriddenWaitTime - (Time.time - dialogueStartTime);
			if (num > 0f)
			{
				yield return new WaitForSeconds(num);
			}
		}
		if (this.buttonPrompt != null && this.hasInput)
		{
			this.buttonPrompt.gameObject.SetActive(true);
			if (this.autoTextAdvanceWhenInput)
			{
				yield return CoroutineUtil.Start(this.WaitUntilAutoAdvanceOrInput());
			}
			else
			{
				yield return this.buttonPrompt.waitUntilTriggered;
			}
			this.buttonPrompt.gameObject.SetActive(false);
		}
		else if (!this.overrideWaitTime && this.automaticTextAdvanceDelay > 0f)
		{
			yield return this.waitForAutomaticTextAdvance;
		}
		yield break;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00004EE8 File Offset: 0x000030E8
	private IEnumerator WaitUntilAutoAdvanceOrInput()
	{
		float advanceTime = Time.time + this.automaticTextAdvanceDelay;
		while (Time.time < advanceTime && !this.buttonPrompt.triggered)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00004EF7 File Offset: 0x000030F7
	private IEnumerator TypeText(string text)
	{
		int index = 0;
		if (this.actor != null)
		{
			this.actor.MouthOpen();
		}
		if (this.grabSizeFromBestFit)
		{
			yield return null;
			this.visibleText.fontSize = this.invisibleText.cachedTextGenerator.fontSizeUsedForBestFit;
		}
		while (index < text.Length)
		{
			if (this.useInvisibleText)
			{
				if (index + 1 < text.Length)
				{
					this.visibleText.text = string.Format("{0}<color=#ffffff00>{1}</color>", text.Substring(0, index + 1), text.Substring(index + 1));
				}
				else
				{
					this.visibleText.text = string.Format("{0}<color=#ffffff00></color>", text);
				}
			}
			else
			{
				this.visibleText.text = text.Substring(0, index + 1);
			}
			if (this.actor != null && this.animateMouths && text[index] != '!' && text[index] != '.' && text[index] != '?' && text[index] != ',')
			{
				if (text[index] == ' ')
				{
					this.actor.MouthClose();
				}
				else
				{
					this.actor.MouthOpen();
				}
			}
			if (index + 1 >= text.Length)
			{
				yield return null;
			}
			else if (text[index] == ',')
			{
				yield return this.commaWait;
			}
			else if ((text[index] == '!' || text[index] == '.' || text[index] == '?') && text[index + 1] == ' ')
			{
				yield return this.fullStopWait;
			}
			else
			{
				yield return this.textWaitTime;
			}
			int num = index;
			index = num + 1;
		}
		this.typeTextCoroutine = null;
		yield break;
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00004F0D File Offset: 0x0000310D
	public void SkipText()
	{
		base.StopCoroutine(this.typeTextCoroutine);
		this.typeTextCoroutine = null;
		this.visibleText.text = this.text;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x000286DC File Offset: 0x000268DC
	private void Update()
	{
		if (this.clearTime > 0f && this.clearTime < Time.time)
		{
			base.gameObject.SetActive(false);
		}
		if (this.rePlayer != null && (this.rePlayer.GetButtonDown("Interact") || (Game.State == GameState.Dialogue && (this.rePlayer.GetButtonDown("Interact Dialogue") || this.rePlayer.GetButtonDown("UISubmit")))))
		{
			this.progressInput = true;
		}
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00004F33 File Offset: 0x00003133
	public void Clear(bool validate = false)
	{
		if (validate)
		{
			this.clearTime = Time.time + 0.05f;
			return;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000590 RID: 1424
	public UIFollow follow;

	// Token: 0x04000591 RID: 1425
	public UILineFollow lineFollow;

	// Token: 0x04000592 RID: 1426
	private Animator animator;

	// Token: 0x04000593 RID: 1427
	private string text;

	// Token: 0x04000594 RID: 1428
	public RectTransform rectTransform;

	// Token: 0x04000595 RID: 1429
	public Text visibleText;

	// Token: 0x04000596 RID: 1430
	public Text invisibleText;

	// Token: 0x04000597 RID: 1431
	public bool invisibleTextIsUnformatted;

	// Token: 0x04000598 RID: 1432
	public Vector2 defaultPosition;

	// Token: 0x04000599 RID: 1433
	public Image coloredImage;

	// Token: 0x0400059A RID: 1434
	public Image[] primaryImages;

	// Token: 0x0400059B RID: 1435
	public Image[] backgroundImages;

	// Token: 0x0400059C RID: 1436
	public Image[] decorations;

	// Token: 0x0400059D RID: 1437
	public Image pattern;

	// Token: 0x0400059E RID: 1438
	public bool useInvisibleText = true;

	// Token: 0x0400059F RID: 1439
	public bool grabSizeFromBestFit;

	// Token: 0x040005A0 RID: 1440
	public bool insertLineBreaks = true;

	// Token: 0x040005A1 RID: 1441
	public float textSpeed;

	// Token: 0x040005A2 RID: 1442
	public float commaWaitTime = 0.1f;

	// Token: 0x040005A3 RID: 1443
	public float fullStopWaitTime = 0.25f;

	// Token: 0x040005A4 RID: 1444
	public float automaticTextAdvanceDelay = 4f;

	// Token: 0x040005A5 RID: 1445
	private bool overrideWaitTime;

	// Token: 0x040005A6 RID: 1446
	private float overriddenWaitTime;

	// Token: 0x040005A7 RID: 1447
	public bool autoTextAdvanceWhenInput;

	// Token: 0x040005A8 RID: 1448
	private YieldInstruction textWait;

	// Token: 0x040005A9 RID: 1449
	private WaitForSeconds waitForTextBeforeInput;

	// Token: 0x040005AA RID: 1450
	private WaitForSeconds textWaitTime;

	// Token: 0x040005AB RID: 1451
	private WaitForSeconds commaWait;

	// Token: 0x040005AC RID: 1452
	private WaitForSeconds fullStopWait;

	// Token: 0x040005AD RID: 1453
	private WaitForSeconds waitForAutomaticTextAdvance;

	// Token: 0x040005AE RID: 1454
	private WaitForSeconds textWaitPause;

	// Token: 0x040005AF RID: 1455
	private IEnumerator typeTextCoroutine;

	// Token: 0x040005B0 RID: 1456
	private WaitUntil waitUntilSkipOrFinish;

	// Token: 0x040005B1 RID: 1457
	public RectTransform textArea;

	// Token: 0x040005B2 RID: 1458
	public Text textTemplate;

	// Token: 0x040005B3 RID: 1459
	private TextGenerationSettings textGenerationSettings;

	// Token: 0x040005B4 RID: 1460
	private bool cachedTextGenerationSettings;

	// Token: 0x040005B5 RID: 1461
	[Space]
	public UINameplate nameplate;

	// Token: 0x040005B6 RID: 1462
	public UIButtonPrompt buttonPrompt;

	// Token: 0x040005B7 RID: 1463
	public bool listenForSkip = true;

	// Token: 0x040005B8 RID: 1464
	private bool progressInput;

	// Token: 0x040005B9 RID: 1465
	public bool animateMouths = true;

	// Token: 0x040005BA RID: 1466
	public bool alwaysTriggerAnimation;

	// Token: 0x040005BB RID: 1467
	private DialogueActor actor;

	// Token: 0x040005BC RID: 1468
	private bool hasInput = true;

	// Token: 0x040005BD RID: 1469
	private Player rePlayer;

	// Token: 0x040005BE RID: 1470
	private float clearTime = -1f;
}
