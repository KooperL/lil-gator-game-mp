using System;
using Rewired;
using UnityEngine;
using UnityEngine.AddressableAssets;

// Token: 0x0200009A RID: 154
public class DebugButtons : MonoBehaviour
{
	// Token: 0x1700001E RID: 30
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

	// Token: 0x040003FE RID: 1022
	public static DebugButtons d;

	// Token: 0x040003FF RID: 1023
	public static double lastSkipTime;

	// Token: 0x04000400 RID: 1024
	private const double skipDelay = 0.016666666666666666;

	// Token: 0x04000401 RID: 1025
	private static bool isSkipHeld;

	// Token: 0x04000402 RID: 1026
	public DebugButtons.TestEnum enum2;

	// Token: 0x04000403 RID: 1027
	public ItemResource craftingResource;

	// Token: 0x04000404 RID: 1028
	public ItemResource populationResource;

	// Token: 0x04000405 RID: 1029
	public Canvas canvas;

	// Token: 0x04000406 RID: 1030
	public Canvas canvas2;

	// Token: 0x04000407 RID: 1031
	public GameObject lockedCamera;

	// Token: 0x04000408 RID: 1032
	private global::Rewired.Player rePlayer;

	// Token: 0x04000409 RID: 1033
	public GameObject toggleSaveObject;

	// Token: 0x0400040A RID: 1034
	public AssetReference prologueScene;

	// Token: 0x0200037D RID: 893
	public enum TestEnum
	{
		// Token: 0x04001A92 RID: 6802
		Option1,
		// Token: 0x04001A93 RID: 6803
		option2,
		// Token: 0x04001A94 RID: 6804
		option3
	}
}
