using System;
using System.Collections;
using System.Globalization;
using System.Text;
using Rewired;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class UINameInput : MonoBehaviour
{
	// Token: 0x06001270 RID: 4720 RVA: 0x0000F9ED File Offset: 0x0000DBED
	public static YieldInstruction ShowNameInputPrompt()
	{
		UINameInput.isInputting = true;
		return CoroutineUtil.Start(global::UnityEngine.Object.Instantiate<GameObject>(Prefabs.p.nameInput).GetComponent<UINameInput>().RunNameInputSequence());
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x0000FA13 File Offset: 0x0000DC13
	private void Awake()
	{
		UINameInput.instance = this;
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x0000FA1B File Offset: 0x0000DC1B
	private void Start()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.Submit), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UISubmitInput"));
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x0000FA56 File Offset: 0x0000DC56
	private void Submit(InputActionEventData obj)
	{
		base.StartCoroutine(this.SubmitNextFrame());
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x0000FA65 File Offset: 0x0000DC65
	private IEnumerator SubmitNextFrame()
	{
		yield return null;
		this.Submit();
		yield break;
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x0005C40C File Offset: 0x0005A60C
	private static string RemoveDiacritics(string text)
	{
		string text2 = text.Normalize(NormalizationForm.FormD);
		StringBuilder stringBuilder = new StringBuilder(text2.Length);
		foreach (char c in text2)
		{
			if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
			{
				stringBuilder.Append(c);
			}
		}
		return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x0005C464 File Offset: 0x0005A664
	public void Submit()
	{
		if (!this.submitButton.enabled)
		{
			return;
		}
		if (this.isAnimating)
		{
			return;
		}
		this.animator.SetBool("ShowError", true);
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.Submit));
		string text = this.inputField.text;
		this.inputField.interactable = false;
		this.submitButton.interactable = false;
		this.preventInputDeselection.enabled = false;
		string text2;
		if (!this.Validate(text, out text2))
		{
			this.errorBoxText.text = text2;
			this.errorBox.SetActive(true);
			return;
		}
		this.confirmBox.SetActive(true);
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x0005C510 File Offset: 0x0005A710
	public void Confirm()
	{
		this.animator.SetBool("ShowError", false);
		string text = this.inputField.text;
		GameData.g.gameSaveData.playerName = this.inputField.text;
		this.inputField.gameObject.SetActive(false);
		this.confirmBox.SetActive(false);
		this.waitingForName = false;
	}

	// Token: 0x06001279 RID: 4729 RVA: 0x0005C578 File Offset: 0x0005A778
	public void Cancel()
	{
		this.animator.SetBool("ShowError", false);
		this.confirmBox.SetActive(false);
		this.inputField.interactable = true;
		this.ShowOnScreenKeyboard();
		this.preventInputDeselection.enabled = true;
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.Submit), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UISubmitInput"));
	}

	// Token: 0x0600127A RID: 4730 RVA: 0x0005C5E8 File Offset: 0x0005A7E8
	public void ConfirmError()
	{
		this.animator.SetBool("ShowError", false);
		this.errorBox.SetActive(false);
		this.inputField.interactable = true;
		this.ShowOnScreenKeyboard();
		this.preventInputDeselection.enabled = true;
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.Submit), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UISubmitInput"));
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x0000FA74 File Offset: 0x0000DC74
	private bool Validate(string inputText, out string errorMessage)
	{
		errorMessage = "";
		inputText.ToLower();
		if (string.IsNullOrEmpty(inputText))
		{
			errorMessage = this.document.FetchString(this.errorTextEmpty, Language.Auto);
			return false;
		}
		return true;
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
	public IEnumerator RunNameInputSequence()
	{
		Game.DialogueDepth++;
		this.waitingForName = true;
		yield return base.StartCoroutine(this.IntroCoroutine());
		if (SpeedrunData.GiveAutoName)
		{
			GameData.g.gameSaveData.playerName = SpeedrunData.AutoName;
			UINameInput.isInputting = false;
		}
		else
		{
			yield return base.StartCoroutine(this.NameInputCoroutine());
		}
		yield return base.StartCoroutine(this.OutroCoroutine());
		Game.DialogueDepth--;
		global::UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x0000FAB3 File Offset: 0x0000DCB3
	public void EndAnimation()
	{
		this.isAnimating = false;
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x0000FABC File Offset: 0x0000DCBC
	private IEnumerator IntroCoroutine()
	{
		yield return null;
		this.isAnimating = true;
		while (this.isAnimating)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x0000FACB File Offset: 0x0000DCCB
	private IEnumerator OutroCoroutine()
	{
		yield return null;
		this.isAnimating = true;
		this.animator.SetTrigger("Outro");
		while (this.isAnimating)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x0000FADA File Offset: 0x0000DCDA
	public void WriteName()
	{
		this.outroText.text = GameData.g.gameSaveData.playerName;
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x0000FAF6 File Offset: 0x0000DCF6
	private IEnumerator NameInputCoroutine()
	{
		yield return null;
		this.inputField.enabled = true;
		this.inputField.gameObject.SetActive(true);
		this.ShowOnScreenKeyboard();
		this.preventInputDeselection.enabled = true;
		while (this.waitingForName)
		{
			yield return null;
		}
		UINameInput.isInputting = false;
		yield break;
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x0005C658 File Offset: 0x0005A858
	private void ShowOnScreenKeyboard()
	{
		SteamUtils.ShowFloatingGamepadTextInput(EFloatingGamepadTextInputMode.k_EFloatingGamepadTextInputModeModeSingleLine, (int)this.preferredOnScreenKeyboardTransform.anchoredPosition.x, (int)this.preferredOnScreenKeyboardTransform.anchoredPosition.y, (int)this.preferredOnScreenKeyboardTransform.sizeDelta.x, (int)this.preferredOnScreenKeyboardTransform.sizeDelta.y);
	}

	public static UINameInput instance;

	public static bool isInputting;

	public Animator animator;

	private bool waitingForName;

	private global::Rewired.Player rePlayer;

	public UIPreventDeselection preventInputDeselection;

	public InputField inputField;

	public Button submitButton;

	public GameObject confirmBox;

	public Text confirmBoxText;

	public GameObject errorBox;

	public Text errorBoxText;

	public MultilingualTextDocument document;

	[Header("Error text")]
	[TextLookup("document")]
	public string errorTextEmpty;

	[TextLookup("document")]
	public string errorTextFriend;

	[Header("Switch-only text")]
	[TextLookup("document")]
	public string promptText1;

	[TextLookup("document")]
	public string promptText2;

	[TextLookup("document")]
	public string confirmText;

	[Header("Outro")]
	public Text outroText;

	private bool isAnimating;

	public RectTransform preferredOnScreenKeyboardTransform;
}
