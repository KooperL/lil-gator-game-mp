using System;
using Rewired;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DebugButtons : MonoBehaviour
{
	// (get) Token: 0x060002E1 RID: 737 RVA: 0x00011390 File Offset: 0x0000F590
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

	// Token: 0x060002E2 RID: 738 RVA: 0x000113EA File Offset: 0x0000F5EA
	private void Awake()
	{
		DebugButtons.d = this;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x000113F4 File Offset: 0x0000F5F4
	public void OnEnable()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnSkip), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("SkipDialogue"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnSkip), UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, ReInput.mapping.GetActionId("SkipDialogue"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.SplitTimer), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("SR_Split"));
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0001148A File Offset: 0x0000F68A
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnSkip));
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.SplitTimer));
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x000114BA File Offset: 0x0000F6BA
	private void OnSkip(InputActionEventData obj)
	{
		if (UINameInput.isInputting)
		{
			return;
		}
		DebugButtons.isSkipHeld = obj.GetButton();
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x000114D0 File Offset: 0x0000F6D0
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

	// Token: 0x060002E7 RID: 743 RVA: 0x000114E6 File Offset: 0x0000F6E6
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

	// Token: 0x060002E8 RID: 744 RVA: 0x0001150E File Offset: 0x0000F70E
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
