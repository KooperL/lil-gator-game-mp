using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000369 RID: 873
public class UIRaceIcon : MonoBehaviour
{
	// Token: 0x060010BB RID: 4283 RVA: 0x000559B4 File Offset: 0x00053BB4
	public static string TimerFormat(float time)
	{
		float num = (float)Mathf.FloorToInt(time / 60f);
		float num2 = time % 60f;
		return string.Format("{0:0}:{1:00.00}", num, num2);
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x000559F0 File Offset: 0x00053BF0
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

	// Token: 0x060010BD RID: 4285 RVA: 0x00055ACC File Offset: 0x00053CCC
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

	// Token: 0x060010BE RID: 4286 RVA: 0x00009344 File Offset: 0x00007544
	public void Clear()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x0000E5A7 File Offset: 0x0000C7A7
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

	// Token: 0x060010C0 RID: 4288 RVA: 0x00055B88 File Offset: 0x00053D88
	private void UpdateDisplay()
	{
		this.progress.fillAmount = Mathf.InverseLerp(this.raceEndTime, this.raceStartTime, Time.time);
		if (this.showTimer)
		{
			this.currentTimeDisplay.text = UIRaceIcon.TimerFormat(Time.time - this.raceStartTime);
		}
	}

	// Token: 0x040015C5 RID: 5573
	public UIFollow uiFollow;

	// Token: 0x040015C6 RID: 5574
	private float raceStartTime;

	// Token: 0x040015C7 RID: 5575
	private float raceEndTime;

	// Token: 0x040015C8 RID: 5576
	public Image progress;

	// Token: 0x040015C9 RID: 5577
	private bool showTimer;

	// Token: 0x040015CA RID: 5578
	public GameObject timer;

	// Token: 0x040015CB RID: 5579
	public Text previousBestDisplay;

	// Token: 0x040015CC RID: 5580
	public Text currentTimeDisplay;

	// Token: 0x040015CD RID: 5581
	public Color betterColor;

	// Token: 0x040015CE RID: 5582
	public Color worseColor;

	// Token: 0x040015CF RID: 5583
	public UIButtonPrompt buttonPrompt;

	// Token: 0x040015D0 RID: 5584
	private bool isEnd;

	// Token: 0x040015D1 RID: 5585
	private float endTimeout = -10f;
}
