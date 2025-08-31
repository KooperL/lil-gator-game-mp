using System;
using UnityEngine;
using UnityEngine.UI;

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

	public UIFollow uiFollow;

	private float raceStartTime;

	private float raceEndTime;

	public Image progress;

	private bool showTimer;

	public GameObject timer;

	public Text previousBestDisplay;

	public Text currentTimeDisplay;

	public Color betterColor;

	public Color worseColor;

	public UIButtonPrompt buttonPrompt;

	private bool isEnd;

	private float endTimeout = -10f;
}
