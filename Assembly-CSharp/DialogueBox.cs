using System;
using System.Collections;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
	// Token: 0x06000410 RID: 1040 RVA: 0x000050CD File Offset: 0x000032CD
	private static bool IsValidBreakSpot(char breakCharacter)
	{
		return breakCharacter == ' ';
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00028F30 File Offset: 0x00027130
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

	// (get) Token: 0x06000412 RID: 1042 RVA: 0x000050D4 File Offset: 0x000032D4
	public bool isTypingText
	{
		get
		{
			return this.typeTextCoroutine != null;
		}
	}

	// (get) Token: 0x06000413 RID: 1043 RVA: 0x0002904C File Offset: 0x0002724C
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

	// Token: 0x06000414 RID: 1044 RVA: 0x00029110 File Offset: 0x00027310
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

	// Token: 0x06000415 RID: 1045 RVA: 0x00002229 File Offset: 0x00000429
	private void OnEnable()
	{
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x000050DF File Offset: 0x000032DF
	private void Start()
	{
		this.defaultPosition = this.rectTransform.anchoredPosition;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x000050F2 File Offset: 0x000032F2
	private void OnDisable()
	{
		if (PlayerInteract.interactButtonPriority == base.gameObject)
		{
			PlayerInteract.interactButtonPriority = null;
		}
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x000291DC File Offset: 0x000273DC
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

	// Token: 0x06000419 RID: 1049 RVA: 0x000292B4 File Offset: 0x000274B4
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
				this.invisibleText.horizontalOverflow = HorizontalWrapMode.Overflow;
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

	// Token: 0x0600041A RID: 1050 RVA: 0x0002954C File Offset: 0x0002774C
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

	// Token: 0x0600041B RID: 1051 RVA: 0x000295A8 File Offset: 0x000277A8
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

	// Token: 0x0600041C RID: 1052 RVA: 0x00029610 File Offset: 0x00027810
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

	// Token: 0x0600041D RID: 1053 RVA: 0x0000510C File Offset: 0x0000330C
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

	// Token: 0x0600041E RID: 1054 RVA: 0x0000511B File Offset: 0x0000331B
	private IEnumerator WaitUntilAutoAdvanceOrInput()
	{
		float advanceTime = Time.time + this.automaticTextAdvanceDelay;
		while (Time.time < advanceTime && !this.buttonPrompt.triggered)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0000512A File Offset: 0x0000332A
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

	// Token: 0x06000420 RID: 1056 RVA: 0x00005140 File Offset: 0x00003340
	public void SkipText()
	{
		base.StopCoroutine(this.typeTextCoroutine);
		this.typeTextCoroutine = null;
		this.visibleText.text = this.text;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00029664 File Offset: 0x00027864
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

	// Token: 0x06000422 RID: 1058 RVA: 0x00005166 File Offset: 0x00003366
	public void Clear(bool validate = false)
	{
		if (validate)
		{
			this.clearTime = Time.time + 0.05f;
			return;
		}
		base.gameObject.SetActive(false);
	}

	public UIFollow follow;

	public UILineFollow lineFollow;

	private Animator animator;

	private string text;

	public RectTransform rectTransform;

	public Text visibleText;

	public Text invisibleText;

	public bool invisibleTextIsUnformatted;

	public Vector2 defaultPosition;

	public Image coloredImage;

	public Image[] primaryImages;

	public Image[] backgroundImages;

	public Image[] decorations;

	public Image pattern;

	public bool useInvisibleText = true;

	public bool grabSizeFromBestFit;

	public bool insertLineBreaks = true;

	public float textSpeed;

	public float commaWaitTime = 0.1f;

	public float fullStopWaitTime = 0.25f;

	public float automaticTextAdvanceDelay = 4f;

	private bool overrideWaitTime;

	private float overriddenWaitTime;

	public bool autoTextAdvanceWhenInput;

	private YieldInstruction textWait;

	private WaitForSeconds waitForTextBeforeInput;

	private WaitForSeconds textWaitTime;

	private WaitForSeconds commaWait;

	private WaitForSeconds fullStopWait;

	private WaitForSeconds waitForAutomaticTextAdvance;

	private WaitForSeconds textWaitPause;

	private IEnumerator typeTextCoroutine;

	private WaitUntil waitUntilSkipOrFinish;

	public RectTransform textArea;

	public Text textTemplate;

	private TextGenerationSettings textGenerationSettings;

	private bool cachedTextGenerationSettings;

	[Space]
	public UINameplate nameplate;

	public UIButtonPrompt buttonPrompt;

	public bool listenForSkip = true;

	private bool progressInput;

	public bool animateMouths = true;

	public bool alwaysTriggerAnimation;

	private DialogueActor actor;

	private bool hasInput = true;

	private global::Rewired.Player rePlayer;

	private float clearTime = -1f;
}
