using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000242 RID: 578
public class SpeedrunTimer : MonoBehaviour
{
	// Token: 0x06000C97 RID: 3223 RVA: 0x0003D289 File Offset: 0x0003B489
	public static void CreateSpeedrunTimer()
	{
		if (SpeedrunTimer.instance != null)
		{
			return;
		}
		Object.Instantiate<GameObject>(Prefabs.p.speedrunTimer);
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x0003D2A9 File Offset: 0x0003B4A9
	public static void DestroySpeedrunTimer()
	{
		if (SpeedrunTimer.instance == null)
		{
			return;
		}
		Object.Destroy(SpeedrunTimer.instance.gameObject);
		SpeedrunTimer.instance = null;
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x0003D2CE File Offset: 0x0003B4CE
	private void Awake()
	{
		SpeedrunTimer.instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
		this.lastFrameTime = Time.realtimeSinceStartupAsDouble;
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x0003D2EC File Offset: 0x0003B4EC
	private void Start()
	{
		this.UpdateIconColor(SpeedrunData.state);
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x0003D2FC File Offset: 0x0003B4FC
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

	// Token: 0x06000C9C RID: 3228 RVA: 0x0003D384 File Offset: 0x0003B584
	public static string TimerFormat(float time)
	{
		float num = (float)Mathf.FloorToInt(time / 3600f);
		float num2 = (float)Mathf.FloorToInt(time % 3600f / 60f);
		float num3 = time % 60f;
		return string.Format("{0:00}:{1:00}:{2:00.00}", num, num2, num3);
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x0003D3D8 File Offset: 0x0003B5D8
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

	// Token: 0x06000C9E RID: 3230 RVA: 0x0003D42D File Offset: 0x0003B62D
	public void Split()
	{
		if (!SpeedrunData.IsRunning)
		{
			return;
		}
		this.splitDisplay.text = SpeedrunTimer.TimerFormat((float)SpeedrunData.inGameTime);
		this.splitHide.Show();
	}

	// Token: 0x040010AA RID: 4266
	public const bool displaySplits = true;

	// Token: 0x040010AB RID: 4267
	public static SpeedrunTimer instance;

	// Token: 0x040010AC RID: 4268
	private double lastFrameTime = -1.0;

	// Token: 0x040010AD RID: 4269
	public Color runNotStarted;

	// Token: 0x040010AE RID: 4270
	public Color runStarted;

	// Token: 0x040010AF RID: 4271
	public Color runEnded;

	// Token: 0x040010B0 RID: 4272
	public Image icon;

	// Token: 0x040010B1 RID: 4273
	public Text timerDisplay;

	// Token: 0x040010B2 RID: 4274
	public UIHideBehavior splitHide;

	// Token: 0x040010B3 RID: 4275
	public Text splitDisplay;

	// Token: 0x040010B4 RID: 4276
	private bool wasTimerRunning;
}
