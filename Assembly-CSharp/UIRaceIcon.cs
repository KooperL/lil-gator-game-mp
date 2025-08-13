using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028F RID: 655
public class UIRaceIcon : MonoBehaviour
{
	// Token: 0x06000DF2 RID: 3570 RVA: 0x00043910 File Offset: 0x00041B10
	public static string TimerFormat(float time)
	{
		float num = (float)Mathf.FloorToInt(time / 60f);
		float num2 = time % 60f;
		return string.Format("{0:0}:{1:00.00}", num, num2);
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x0004394C File Offset: 0x00041B4C
	public void LoadRace(float raceLength, Transform finishLine, float previousBest = -1f)
	{
		this.isEnd = false;
		this.buttonPrompt.gameObject.SetActive(false);
		this.uiFollow.enabled = true;
		this.uiFollow.followTarget = finishLine;
		this.uiFollow.keepWithinBounds = true;
		this.raceStartTime = Time.time;
		this.raceEndTime = Time.time + raceLength;
		this.showTimer = previousBest >= 0f;
		if (this.showTimer)
		{
			this.previousBestDisplay.text = UIRaceIcon.TimerFormat(previousBest);
			this.currentTimeDisplay.text = UIRaceIcon.TimerFormat(0f);
			this.currentTimeDisplay.color = Color.white;
			this.timer.SetActive(true);
		}
		else
		{
			this.timer.SetActive(false);
		}
		this.UpdateDisplay();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x00043A28 File Offset: 0x00041C28
	public void LoadEnd(Transform finishLine, float previousBest, float newTime)
	{
		this.isEnd = true;
		this.endTimeout = Time.time + 10f;
		this.uiFollow.enabled = false;
		base.GetComponent<RectTransform>().anchoredPosition = new Vector2(790f, 932f);
		this.buttonPrompt.gameObject.SetActive(true);
		this.showTimer = true;
		this.previousBestDisplay.text = UIRaceIcon.TimerFormat(previousBest);
		this.currentTimeDisplay.text = UIRaceIcon.TimerFormat(newTime);
		this.currentTimeDisplay.color = ((newTime < previousBest) ? this.betterColor : this.worseColor);
		this.timer.SetActive(true);
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x00043AE2 File Offset: 0x00041CE2
	public void Clear()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x00043AF0 File Offset: 0x00041CF0
	private void Update()
	{
		if (!this.isEnd)
		{
			this.UpdateDisplay();
			return;
		}
		if (this.buttonPrompt.triggered || Time.time > this.endTimeout || Game.State != GameState.Play)
		{
			this.Clear();
		}
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x00043B28 File Offset: 0x00041D28
	private void UpdateDisplay()
	{
		this.progress.fillAmount = Mathf.InverseLerp(this.raceEndTime, this.raceStartTime, Time.time);
		if (this.showTimer)
		{
			this.currentTimeDisplay.text = UIRaceIcon.TimerFormat(Time.time - this.raceStartTime);
		}
	}

	// Token: 0x04001265 RID: 4709
	public UIFollow uiFollow;

	// Token: 0x04001266 RID: 4710
	private float raceStartTime;

	// Token: 0x04001267 RID: 4711
	private float raceEndTime;

	// Token: 0x04001268 RID: 4712
	public Image progress;

	// Token: 0x04001269 RID: 4713
	private bool showTimer;

	// Token: 0x0400126A RID: 4714
	public GameObject timer;

	// Token: 0x0400126B RID: 4715
	public Text previousBestDisplay;

	// Token: 0x0400126C RID: 4716
	public Text currentTimeDisplay;

	// Token: 0x0400126D RID: 4717
	public Color betterColor;

	// Token: 0x0400126E RID: 4718
	public Color worseColor;

	// Token: 0x0400126F RID: 4719
	public UIButtonPrompt buttonPrompt;

	// Token: 0x04001270 RID: 4720
	private bool isEnd;

	// Token: 0x04001271 RID: 4721
	private float endTimeout = -10f;
}
