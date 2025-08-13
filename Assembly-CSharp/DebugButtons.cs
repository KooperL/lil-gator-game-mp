using System;
using Rewired;
using UnityEngine;
using UnityEngine.AddressableAssets;

// Token: 0x020000C1 RID: 193
public class DebugButtons : MonoBehaviour
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000324 RID: 804 RVA: 0x00024A98 File Offset: 0x00022C98
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

	// Token: 0x06000325 RID: 805 RVA: 0x00004708 File Offset: 0x00002908
	private void Awake()
	{
		DebugButtons.d = this;
	}

	// Token: 0x06000326 RID: 806 RVA: 0x00024AF4 File Offset: 0x00022CF4
	public void OnEnable()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnSkip), 0, 3, ReInput.mapping.GetActionId("SkipDialogue"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnSkip), 0, 4, ReInput.mapping.GetActionId("SkipDialogue"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.SplitTimer), 0, 3, ReInput.mapping.GetActionId("SR_Split"));
	}

	// Token: 0x06000327 RID: 807 RVA: 0x00004710 File Offset: 0x00002910
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnSkip));
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.SplitTimer));
	}

	// Token: 0x06000328 RID: 808 RVA: 0x00004740 File Offset: 0x00002940
	private void OnSkip(InputActionEventData obj)
	{
		if (UINameInput.isInputting)
		{
			return;
		}
		DebugButtons.isSkipHeld = obj.GetButton();
	}

	// Token: 0x06000329 RID: 809 RVA: 0x00004756 File Offset: 0x00002956
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

	// Token: 0x0600032A RID: 810 RVA: 0x0000476C File Offset: 0x0000296C
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

	// Token: 0x0600032B RID: 811 RVA: 0x00004794 File Offset: 0x00002994
	private void QuitToTitle(InputActionEventData obj)
	{
		if (!SpeedrunData.IsSpeedrunMode)
		{
			return;
		}
		LoadSceneSequence.LoadScene(this.prologueScene, LoadSceneSequence.LoadType.LoadingScreen);
	}

	// Token: 0x04000490 RID: 1168
	public static DebugButtons d;

	// Token: 0x04000491 RID: 1169
	public static double lastSkipTime;

	// Token: 0x04000492 RID: 1170
	private const double skipDelay = 0.016666666666666666;

	// Token: 0x04000493 RID: 1171
	private static bool isSkipHeld;

	// Token: 0x04000494 RID: 1172
	public DebugButtons.TestEnum enum2;

	// Token: 0x04000495 RID: 1173
	public ItemResource craftingResource;

	// Token: 0x04000496 RID: 1174
	public ItemResource populationResource;

	// Token: 0x04000497 RID: 1175
	public Canvas canvas;

	// Token: 0x04000498 RID: 1176
	public Canvas canvas2;

	// Token: 0x04000499 RID: 1177
	public GameObject lockedCamera;

	// Token: 0x0400049A RID: 1178
	private Player rePlayer;

	// Token: 0x0400049B RID: 1179
	public GameObject toggleSaveObject;

	// Token: 0x0400049C RID: 1180
	public AssetReference prologueScene;

	// Token: 0x020000C2 RID: 194
	public enum TestEnum
	{
		// Token: 0x0400049E RID: 1182
		Option1,
		// Token: 0x0400049F RID: 1183
		option2,
		// Token: 0x040004A0 RID: 1184
		option3
	}
}
