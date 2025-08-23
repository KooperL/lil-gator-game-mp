using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedrunTimer : MonoBehaviour
{
	// Token: 0x06000FA0 RID: 4000 RVA: 0x0000D7FC File Offset: 0x0000B9FC
	public static void CreateSpeedrunTimer()
	{
		if (SpeedrunTimer.instance != null)
		{
			return;
		}
		global::UnityEngine.Object.Instantiate<GameObject>(Prefabs.p.speedrunTimer);
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x0000D81C File Offset: 0x0000BA1C
	public static void DestroySpeedrunTimer()
	{
		if (SpeedrunTimer.instance == null)
		{
			return;
		}
		global::UnityEngine.Object.Destroy(SpeedrunTimer.instance.gameObject);
		SpeedrunTimer.instance = null;
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0000D841 File Offset: 0x0000BA41
	private void Awake()
	{
		SpeedrunTimer.instance = this;
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.lastFrameTime = Time.realtimeSinceStartupAsDouble;
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x0000D85F File Offset: 0x0000BA5F
	private void Start()
	{
		this.UpdateIconColor(SpeedrunData.state);
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x0005242C File Offset: 0x0005062C
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

	// Token: 0x06000FA5 RID: 4005 RVA: 0x000524B4 File Offset: 0x000506B4
	public static string TimerFormat(float time)
	{
		float num = (float)Mathf.FloorToInt(time / 3600f);
		float num2 = (float)Mathf.FloorToInt(time % 3600f / 60f);
		float num3 = time % 60f;
		return string.Format("{0:00}:{1:00}:{2:00.00}", num, num2, num3);
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x00052508 File Offset: 0x00050708
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

	// Token: 0x06000FA7 RID: 4007 RVA: 0x0000D86C File Offset: 0x0000BA6C
	public void Split()
	{
		if (!SpeedrunData.IsRunning)
		{
			return;
		}
		this.splitDisplay.text = SpeedrunTimer.TimerFormat((float)SpeedrunData.inGameTime);
		this.splitHide.Show();
	}

	public const bool displaySplits = true;

	public static SpeedrunTimer instance;

	private double lastFrameTime = -1.0;

	public Color runNotStarted;

	public Color runStarted;

	public Color runEnded;

	public Image icon;

	public Text timerDisplay;

	public UIHideBehavior splitHide;

	public Text splitDisplay;

	private bool wasTimerRunning;
}
