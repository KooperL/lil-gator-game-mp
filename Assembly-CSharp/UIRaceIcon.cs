using System;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceIcon : MonoBehaviour
{
	// Token: 0x06001117 RID: 4375 RVA: 0x00057BF0 File Offset: 0x00055DF0
	public static string TimerFormat(float time)
	{
		float num = (float)Mathf.FloorToInt(time / 60f);
		float num2 = time % 60f;
		return string.Format("{0:0}:{1:00.00}", num, num2);
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x00057C2C File Offset: 0x00055E2C
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

	// Token: 0x06001119 RID: 4377 RVA: 0x00057D08 File Offset: 0x00055F08
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

	// Token: 0x0600111A RID: 4378 RVA: 0x000096AC File Offset: 0x000078AC
	public void Clear()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x0000E91A File Offset: 0x0000CB1A
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

	// Token: 0x0600111C RID: 4380 RVA: 0x00057DC4 File Offset: 0x00055FC4
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
