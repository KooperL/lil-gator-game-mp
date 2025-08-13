using System;
using System.Collections;
using System.Globalization;
using System.Text;
using Rewired;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D1 RID: 721
public class UINameInput : MonoBehaviour
{
	// Token: 0x06000F2C RID: 3884 RVA: 0x000492D8 File Offset: 0x000474D8
	public static YieldInstruction ShowNameInputPrompt()
	{
		UINameInput.isInputting = true;
		return CoroutineUtil.Start(Object.Instantiate<GameObject>(Prefabs.p.nameInput).GetComponent<UINameInput>().RunNameInputSequence());
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x000492FE File Offset: 0x000474FE
	private void Awake()
	{
		UINameInput.instance = this;
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x00049306 File Offset: 0x00047506
	private void Start()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.Submit), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UISubmitInput"));
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x00049341 File Offset: 0x00047541
	private void OnDestroy()
	{
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00049343 File Offset: 0x00047543
	private void Submit(InputActionEventData obj)
	{
		base.StartCoroutine(this.SubmitNextFrame());
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x00049352 File Offset: 0x00047552
	private IEnumerator SubmitNextFrame()
	{
		yield return null;
		this.Submit();
		yield break;
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x00049364 File Offset: 0x00047564
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

	// Token: 0x06000F33 RID: 3891 RVA: 0x000493BC File Offset: 0x000475BC
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

	// Token: 0x06000F34 RID: 3892 RVA: 0x00049468 File Offset: 0x00047668
	public void Confirm()
	{
		this.animator.SetBool("ShowError", false);
		string text = this.inputField.text;
		GameData.g.gameSaveData.playerName = this.inputField.text;
		this.inputField.gameObject.SetActive(false);
		this.confirmBox.SetActive(false);
		this.waitingForName = false;
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x000494D0 File Offset: 0x000476D0
	public void Cancel()
	{
		this.animator.SetBool("ShowError", false);
		this.confirmBox.SetActive(false);
		this.inputField.interactable = true;
		this.ShowOnScreenKeyboard();
		this.preventInputDeselection.enabled = true;
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.Submit), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UISubmitInput"));
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x00049540 File Offset: 0x00047740
	public void ConfirmError()
	{
		this.animator.SetBool("ShowError", false);
		this.errorBox.SetActive(false);
		this.inputField.interactable = true;
		this.ShowOnScreenKeyboard();
		this.preventInputDeselection.enabled = true;
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.Submit), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UISubmitInput"));
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x000495B0 File Offset: 0x000477B0
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

	// Token: 0x06000F38 RID: 3896 RVA: 0x000495E0 File Offset: 0x000477E0
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
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x000495EF File Offset: 0x000477EF
	public void EndAnimation()
	{
		this.isAnimating = false;
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x000495F8 File Offset: 0x000477F8
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

	// Token: 0x06000F3B RID: 3899 RVA: 0x00049607 File Offset: 0x00047807
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

	// Token: 0x06000F3C RID: 3900 RVA: 0x00049616 File Offset: 0x00047816
	public void WriteName()
	{
		this.outroText.text = GameData.g.gameSaveData.playerName;
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00049632 File Offset: 0x00047832
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

	// Token: 0x06000F3E RID: 3902 RVA: 0x00049644 File Offset: 0x00047844
	private void ShowOnScreenKeyboard()
	{
		SteamUtils.ShowFloatingGamepadTextInput(EFloatingGamepadTextInputMode.k_EFloatingGamepadTextInputModeModeSingleLine, (int)this.preferredOnScreenKeyboardTransform.anchoredPosition.x, (int)this.preferredOnScreenKeyboardTransform.anchoredPosition.y, (int)this.preferredOnScreenKeyboardTransform.sizeDelta.x, (int)this.preferredOnScreenKeyboardTransform.sizeDelta.y);
	}

	// Token: 0x040013F9 RID: 5113
	public static UINameInput instance;

	// Token: 0x040013FA RID: 5114
	public static bool isInputting;

	// Token: 0x040013FB RID: 5115
	public Animator animator;

	// Token: 0x040013FC RID: 5116
	private bool waitingForName;

	// Token: 0x040013FD RID: 5117
	private global::Rewired.Player rePlayer;

	// Token: 0x040013FE RID: 5118
	public UIPreventDeselection preventInputDeselection;

	// Token: 0x040013FF RID: 5119
	public InputField inputField;

	// Token: 0x04001400 RID: 5120
	public Button submitButton;

	// Token: 0x04001401 RID: 5121
	public GameObject confirmBox;

	// Token: 0x04001402 RID: 5122
	public Text confirmBoxText;

	// Token: 0x04001403 RID: 5123
	public GameObject errorBox;

	// Token: 0x04001404 RID: 5124
	public Text errorBoxText;

	// Token: 0x04001405 RID: 5125
	public MultilingualTextDocument document;

	// Token: 0x04001406 RID: 5126
	[Header("Error text")]
	[TextLookup("document")]
	public string errorTextEmpty;

	// Token: 0x04001407 RID: 5127
	[TextLookup("document")]
	public string errorTextFriend;

	// Token: 0x04001408 RID: 5128
	[Header("Switch-only text")]
	[TextLookup("document")]
	public string promptText1;

	// Token: 0x04001409 RID: 5129
	[TextLookup("document")]
	public string promptText2;

	// Token: 0x0400140A RID: 5130
	[TextLookup("document")]
	public string confirmText;

	// Token: 0x0400140B RID: 5131
	[Header("Outro")]
	public Text outroText;

	// Token: 0x0400140C RID: 5132
	private bool isAnimating;

	// Token: 0x0400140D RID: 5133
	public RectTransform preferredOnScreenKeyboardTransform;
}
