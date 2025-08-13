using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000306 RID: 774
public class SpeedrunTimer : MonoBehaviour
{
	// Token: 0x06000F43 RID: 3907 RVA: 0x0000D44A File Offset: 0x0000B64A
	public static void CreateSpeedrunTimer()
	{
		if (SpeedrunTimer.instance != null)
		{
			return;
		}
		Object.Instantiate<GameObject>(Prefabs.p.speedrunTimer);
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x0000D46A File Offset: 0x0000B66A
	public static void DestroySpeedrunTimer()
	{
		if (SpeedrunTimer.instance == null)
		{
			return;
		}
		Object.Destroy(SpeedrunTimer.instance.gameObject);
		SpeedrunTimer.instance = null;
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x0000D48F File Offset: 0x0000B68F
	private void Awake()
	{
		SpeedrunTimer.instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
		this.lastFrameTime = Time.realtimeSinceStartupAsDouble;
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x0000D4AD File Offset: 0x0000B6AD
	private void Start()
	{
		this.UpdateIconColor(SpeedrunData.state);
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x00050240 File Offset: 0x0004E440
	private void Update()
	{
		double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
		if (this.lastFrameTime > 0.0 && SpeedrunData.IsTimerRunning && this.wasTimerRunning)
		{
			double num = realtimeSinceStartupAsDouble - this.lastFrameTime;
			if (num > 0.1)
			{
				num = 0.1;
			}
			SpeedrunData.inGameTime += num;
		}
		this.lastFrameTime = realtimeSinceStartupAsDouble;
		this.timerDisplay.text = SpeedrunTimer.TimerFormat((float)SpeedrunData.inGameTime);
		this.wasTimerRunning = SpeedrunData.IsTimerRunning;
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x000502C8 File Offset: 0x0004E4C8
	public static string TimerFormat(float time)
	{
		float num = (float)Mathf.FloorToInt(time / 3600f);
		float num2 = (float)Mathf.FloorToInt(time % 3600f / 60f);
		float num3 = time % 60f;
		return string.Format("{0:00}:{1:00}:{2:00.00}", num, num2, num3);
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x0005031C File Offset: 0x0004E51C
	public void UpdateIconColor(RunState runState)
	{
		switch (runState)
		{
		case RunState.NotStarted:
			this.icon.color = this.runNotStarted;
			return;
		case RunState.Started:
			this.icon.color = this.runStarted;
			return;
		case RunState.Done:
			this.icon.color = this.runEnded;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x0000D4BA File Offset: 0x0000B6BA
	public void Split()
	{
		if (!SpeedrunData.IsRunning)
		{
			return;
		}
		this.splitDisplay.text = SpeedrunTimer.TimerFormat((float)SpeedrunData.inGameTime);
		this.splitHide.Show();
	}

	// Token: 0x040013C0 RID: 5056
	public const bool displaySplits = true;

	// Token: 0x040013C1 RID: 5057
	public static SpeedrunTimer instance;

	// Token: 0x040013C2 RID: 5058
	private double lastFrameTime = -1.0;

	// Token: 0x040013C3 RID: 5059
	public Color runNotStarted;

	// Token: 0x040013C4 RID: 5060
	public Color runStarted;

	// Token: 0x040013C5 RID: 5061
	public Color runEnded;

	// Token: 0x040013C6 RID: 5062
	public Image icon;

	// Token: 0x040013C7 RID: 5063
	public Text timerDisplay;

	// Token: 0x040013C8 RID: 5064
	public UIHideBehavior splitHide;

	// Token: 0x040013C9 RID: 5065
	public Text splitDisplay;

	// Token: 0x040013CA RID: 5066
	private bool wasTimerRunning;
}
