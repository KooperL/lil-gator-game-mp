using System;
using Rewired;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DebugButtons : MonoBehaviour
{
	// (get) Token: 0x0600034A RID: 842 RVA: 0x00025A50 File Offset: 0x00023C50
	public static bool IsSkipHeld
	{
		get
		{
			if (DebugButtons.isSkipHeld || SpeedrunData.ShouldSkip)
			{
				double unscaledTimeAsDouble = Time.unscaledTimeAsDouble;
				if (unscaledTimeAsDouble - DebugButtons.lastSkipTime > 0.016666666666666666)
				{
					DebugButtons.lastSkipTime = Math.Max(unscaledTimeAsDouble + 0.008333333333333333, DebugButtons.lastSkipTime + 0.016666666666666666);
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x0600034B RID: 843 RVA: 0x000048EC File Offset: 0x00002AEC
	private void Awake()
	{
		DebugButtons.d = this;
	}

	// Token: 0x0600034C RID: 844 RVA: 0x00025AAC File Offset: 0x00023CAC
	public void OnEnable()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnSkip), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("SkipDialogue"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnSkip), UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, ReInput.mapping.GetActionId("SkipDialogue"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.SplitTimer), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("SR_Split"));
	}

	// Token: 0x0600034D RID: 845 RVA: 0x000048F4 File Offset: 0x00002AF4
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnSkip));
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.SplitTimer));
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00004924 File Offset: 0x00002B24
	private void OnSkip(InputActionEventData obj)
	{
		if (UINameInput.isInputting)
		{
			return;
		}
		DebugButtons.isSkipHeld = obj.GetButton();
	}

	// Token: 0x0600034F RID: 847 RVA: 0x0000493A File Offset: 0x00002B3A
	private void StopTimer(InputActionEventData obj)
	{
		if (!SpeedrunData.IsSpeedrunMode)
		{
			return;
		}
		if (SpeedrunData.IsRunning)
		{
			SpeedrunData.EndRun();
		}
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00004950 File Offset: 0x00002B50
	private void SplitTimer(InputActionEventData obj)
	{
		if (!SpeedrunData.IsSpeedrunMode)
		{
			return;
		}
		if (SpeedrunData.IsRunning && SpeedrunTimer.instance != null)
		{
			SpeedrunTimer.instance.Split();
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00004978 File Offset: 0x00002B78
	private void QuitToTitle(InputActionEventData obj)
	{
		if (!SpeedrunData.IsSpeedrunMode)
		{
			return;
		}
		LoadSceneSequence.LoadScene(this.prologueScene, LoadSceneSequence.LoadType.LoadingScreen);
	}

	public static DebugButtons d;

	public static double lastSkipTime;

	private const double skipDelay = 0.016666666666666666;

	private static bool isSkipHeld;

	public DebugButtons.TestEnum enum2;

	public ItemResource craftingResource;

	public ItemResource populationResource;

	public Canvas canvas;

	public Canvas canvas2;

	public GameObject lockedCamera;

	private global::Rewired.Player rePlayer;

	public GameObject toggleSaveObject;

	public AssetReference prologueScene;

	public enum TestEnum
	{
		Option1,
		option2,
		option3
	}
}
