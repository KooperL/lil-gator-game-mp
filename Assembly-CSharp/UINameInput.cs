using System;
using System.Collections;
using System.Globalization;
using System.Text;
using Rewired;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003B5 RID: 949
public class UINameInput : MonoBehaviour
{
	// Token: 0x06001210 RID: 4624 RVA: 0x0000F5FA File Offset: 0x0000D7FA
	public static YieldInstruction ShowNameInputPrompt()
	{
		UINameInput.isInputting = true;
		return CoroutineUtil.Start(Object.Instantiate<GameObject>(Prefabs.p.nameInput).GetComponent<UINameInput>().RunNameInputSequence());
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x0000F620 File Offset: 0x0000D820
	private void Awake()
	{
		UINameInput.instance = this;
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x0000F628 File Offset: 0x0000D828
	private void Start()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.Submit), 0, 3, ReInput.mapping.GetActionId("UISubmitInput"));
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x0000F663 File Offset: 0x0000D863
	private void Submit(InputActionEventData obj)
	{
		base.StartCoroutine(this.SubmitNextFrame());
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x0000F672 File Offset: 0x0000D872
	private IEnumerator SubmitNextFrame()
	{
		yield return null;
		this.Submit();
		yield break;
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x0005A46C File Offset: 0x0005866C
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

	// Token: 0x06001217 RID: 4631 RVA: 0x0005A4C4 File Offset: 0x000586C4
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
		string text = UINameInput.RemoveDiacritics(this.inputField.text);
		this.inputField.text = text;
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

	// Token: 0x06001218 RID: 4632 RVA: 0x0005A584 File Offset: 0x00058784
	public void Confirm()
	{
		this.animator.SetBool("ShowError", false);
		string text = this.inputField.text;
		GameData.g.gameSaveData.playerName = this.inputField.text;
		this.inputField.gameObject.SetActive(false);
		this.confirmBox.SetActive(false);
		this.waitingForName = false;
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x0005A5EC File Offset: 0x000587EC
	public void Cancel()
	{
		this.animator.SetBool("ShowError", false);
		this.confirmBox.SetActive(false);
		this.inputField.interactable = true;
		this.ShowOnScreenKeyboard();
		this.preventInputDeselection.enabled = true;
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.Submit), 0, 3, ReInput.mapping.GetActionId("UISubmitInput"));
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x0005A65C File Offset: 0x0005885C
	public void ConfirmError()
	{
		this.animator.SetBool("ShowError", false);
		this.errorBox.SetActive(false);
		this.inputField.interactable = true;
		this.ShowOnScreenKeyboard();
		this.preventInputDeselection.enabled = true;
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.Submit), 0, 3, ReInput.mapping.GetActionId("UISubmitInput"));
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x0000F681 File Offset: 0x0000D881
	private bool Validate(string inputText, out string errorMessage)
	{
		errorMessage = "";
		inputText.ToLower();
		if (string.IsNullOrEmpty(inputText))
		{
			errorMessage = this.errorTextEmpty;
			return false;
		}
		return true;
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
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

	// Token: 0x0600121D RID: 4637 RVA: 0x0000F6B3 File Offset: 0x0000D8B3
	public void EndAnimation()
	{
		this.isAnimating = false;
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x0000F6BC File Offset: 0x0000D8BC
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

	// Token: 0x0600121F RID: 4639 RVA: 0x0000F6CB File Offset: 0x0000D8CB
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

	// Token: 0x06001220 RID: 4640 RVA: 0x0000F6DA File Offset: 0x0000D8DA
	public void WriteName()
	{
		this.outroText.text = GameData.g.gameSaveData.playerName;
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x0000F6F6 File Offset: 0x0000D8F6
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

	// Token: 0x06001222 RID: 4642 RVA: 0x0005A6CC File Offset: 0x000588CC
	private void ShowOnScreenKeyboard()
	{
		SteamUtils.ShowFloatingGamepadTextInput(0, (int)this.preferredOnScreenKeyboardTransform.anchoredPosition.x, (int)this.preferredOnScreenKeyboardTransform.anchoredPosition.y, (int)this.preferredOnScreenKeyboardTransform.sizeDelta.x, (int)this.preferredOnScreenKeyboardTransform.sizeDelta.y);
	}

	// Token: 0x0400177D RID: 6013
	public static UINameInput instance;

	// Token: 0x0400177E RID: 6014
	public static bool isInputting;

	// Token: 0x0400177F RID: 6015
	public Animator animator;

	// Token: 0x04001780 RID: 6016
	private bool waitingForName;

	// Token: 0x04001781 RID: 6017
	private Player rePlayer;

	// Token: 0x04001782 RID: 6018
	public UIPreventDeselection preventInputDeselection;

	// Token: 0x04001783 RID: 6019
	public InputField inputField;

	// Token: 0x04001784 RID: 6020
	public Button submitButton;

	// Token: 0x04001785 RID: 6021
	public GameObject confirmBox;

	// Token: 0x04001786 RID: 6022
	public Text confirmBoxText;

	// Token: 0x04001787 RID: 6023
	public GameObject errorBox;

	// Token: 0x04001788 RID: 6024
	public Text errorBoxText;

	// Token: 0x04001789 RID: 6025
	[Header("Error text")]
	public string errorTextEmpty;

	// Token: 0x0400178A RID: 6026
	public string errorTextFriend;

	// Token: 0x0400178B RID: 6027
	[Header("Switch-only text")]
	public string promptText1;

	// Token: 0x0400178C RID: 6028
	public string promptText2;

	// Token: 0x0400178D RID: 6029
	public string confirmText;

	// Token: 0x0400178E RID: 6030
	[Header("Outro")]
	public Text outroText;

	// Token: 0x0400178F RID: 6031
	private bool isAnimating;

	// Token: 0x04001790 RID: 6032
	public RectTransform preferredOnScreenKeyboardTransform;
}
