using System;
using System.Collections.Generic;

public static class SpeedrunData
{
	// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0003CEFF File Offset: 0x0003B0FF
	// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0003CF06 File Offset: 0x0003B106
	public static bool IsSpeedrunMode
	{
		get
		{
			return SpeedrunData.isSpeedrunMode;
		}
		set
		{
			if (value == SpeedrunData.isSpeedrunMode)
			{
				return;
			}
			SpeedrunData.isSpeedrunMode = value;
			SpeedrunData.ResetRun();
			SpeedrunData.inGameTime = 0.0;
			if (value)
			{
				SpeedrunTimer.CreateSpeedrunTimer();
				return;
			}
			SpeedrunTimer.DestroySpeedrunTimer();
		}
	}

	// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0003CF38 File Offset: 0x0003B138
	public static bool ShouldSkip
	{
		get
		{
			return SpeedrunData.isSpeedrunMode;
		}
	}

	// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0003CF3F File Offset: 0x0003B13F
	public static bool GiveAutoName
	{
		get
		{
			return SpeedrunData.isSpeedrunMode && SpeedrunData.state == RunState.Started && SpeedrunData.autoName > AutoNameFunctionality.Off;
		}
	}

	// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0003CF5C File Offset: 0x0003B15C
	public static string AutoName
	{
		get
		{
			switch (SpeedrunData.autoName)
			{
			case AutoNameFunctionality.LilGator:
				return "lil gator";
			case AutoNameFunctionality.File1:
				if (FileUtil.gameSaveDataInfo[0].isStarted)
				{
					return FileUtil.gameSaveDataInfo[0].playerName;
				}
				break;
			case AutoNameFunctionality.File2:
				if (FileUtil.gameSaveDataInfo[1].isStarted)
				{
					return FileUtil.gameSaveDataInfo[1].playerName;
				}
				break;
			case AutoNameFunctionality.File3:
				if (FileUtil.gameSaveDataInfo[2].isStarted)
				{
					return FileUtil.gameSaveDataInfo[2].playerName;
				}
				break;
			}
			return "lil gator";
		}
	}

	// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0003CFFD File Offset: 0x0003B1FD
	public static bool MaxSpeedAutoSword
	{
		get
		{
			return SpeedrunData.isSpeedrunMode;
		}
	}

	// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0003D004 File Offset: 0x0003B204
	public static bool IsRunning
	{
		get
		{
			return SpeedrunData.isSpeedrunMode && SpeedrunData.state == RunState.Started;
		}
	}

	// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0003D017 File Offset: 0x0003B217
	public static bool IsTimerRunning
	{
		get
		{
			return SpeedrunData.state == RunState.Started && !SpeedrunData.isLoading;
		}
	}

	// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0003D02B File Offset: 0x0003B22B
	public static bool ShouldTrack
	{
		get
		{
			return SpeedrunData.isSpeedrunMode;
		}
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x0003D032 File Offset: 0x0003B232
	public static void StartNewRun()
	{
		SpeedrunData.inGameTime = 0.0;
		SpeedrunData.state = RunState.Started;
		SpeedrunData.ClearStats();
		if (SpeedrunTimer.instance != null)
		{
			SpeedrunTimer.instance.UpdateIconColor(RunState.Started);
		}
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x0003D065 File Offset: 0x0003B265
	public static void EndRun()
	{
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x0003D067 File Offset: 0x0003B267
	public static void ResetRun()
	{
		SpeedrunData.state = RunState.NotStarted;
		SpeedrunData.ClearStats();
		if (SpeedrunTimer.instance != null)
		{
			SpeedrunTimer.instance.UpdateIconColor(RunState.NotStarted);
		}
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x0003D08C File Offset: 0x0003B28C
	private static void ClearStats()
	{
		SpeedrunData.unlockedFriends = new List<string>();
		SpeedrunData.unlockedItems = new List<string>();
		SpeedrunData.completedQuests = new List<string>();
		SpeedrunData.credits_lastInput = (SpeedrunData.credits = false);
		SpeedrunData.thanksForPlaying_lastInput = (SpeedrunData.thanksForPlaying = false);
		SpeedrunData.tutorialEnd_LastInput = (SpeedrunData.tutorialEnd = false);
		SpeedrunData.jillQuestComplete = (SpeedrunData.averyQuestComplete = (SpeedrunData.martinQuestComplete = false));
		SpeedrunData.showTownToSis = false;
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x0003D0F4 File Offset: 0x0003B2F4
	public static void Cue(SpeedrunCueTime cueType)
	{
		bool flag = false;
		if (cueType <= SpeedrunCueTime.TutorialEnd_LastInput)
		{
			if (cueType <= SpeedrunCueTime.Credits)
			{
				if (cueType != SpeedrunCueTime.Credits_LastInput)
				{
					if (cueType == SpeedrunCueTime.Credits)
					{
						SpeedrunData.credits = true;
						if (SpeedrunData.split_credits)
						{
							flag = true;
						}
					}
				}
				else
				{
					SpeedrunData.credits_lastInput = true;
					if (SpeedrunData.split_flashbackEnd)
					{
						flag = true;
					}
				}
			}
			else if (cueType != SpeedrunCueTime.ThanksForPlaying_LastInput)
			{
				if (cueType != SpeedrunCueTime.ThanksForPlaying)
				{
					if (cueType == SpeedrunCueTime.TutorialEnd_LastInput)
					{
						SpeedrunData.tutorialEnd_LastInput = true;
						if (SpeedrunData.split_tutorialEnd)
						{
							flag = true;
						}
					}
				}
				else
				{
					SpeedrunData.thanksForPlaying = true;
				}
			}
			else
			{
				SpeedrunData.thanksForPlaying_lastInput = true;
				if (SpeedrunData.split_goHome)
				{
					flag = true;
				}
			}
		}
		else if (cueType <= SpeedrunCueTime.JillQuestComplete)
		{
			if (cueType != SpeedrunCueTime.TutorialEnd)
			{
				if (cueType == SpeedrunCueTime.JillQuestComplete)
				{
					SpeedrunData.jillQuestComplete = true;
					if (SpeedrunData.split_jillQuest)
					{
						flag = true;
					}
				}
			}
			else
			{
				SpeedrunData.tutorialEnd = true;
			}
		}
		else if (cueType != SpeedrunCueTime.MartinQuestComplete)
		{
			if (cueType != SpeedrunCueTime.AveryQuestComplete)
			{
				if (cueType == SpeedrunCueTime.ShowTownToSis)
				{
					SpeedrunData.showTownToSis = true;
					if (SpeedrunData.split_showTownToSis)
					{
						flag = true;
					}
				}
			}
			else
			{
				SpeedrunData.averyQuestComplete = true;
				if (SpeedrunData.split_averyQuest)
				{
					flag = true;
				}
			}
		}
		else
		{
			SpeedrunData.martinQuestComplete = true;
			if (SpeedrunData.split_martinQuest)
			{
				flag = true;
			}
		}
		if (flag && SpeedrunData.IsTimerRunning && SpeedrunTimer.instance != null)
		{
			SpeedrunTimer.instance.Split();
		}
	}

	private static bool isSpeedrunMode = false;

	public static AutoNameFunctionality autoName = AutoNameFunctionality.LilGator;

	public const bool maxSpeedAutoSword = true;

	public static bool isLoading = false;

	public static RunState state = RunState.NotStarted;

	public static double inGameTime = 0.0;

	public static List<string> unlockedFriends = new List<string>();

	public static List<string> unlockedItems = new List<string>();

	public static List<string> completedQuests = new List<string>();

	public static bool credits_lastInput = false;

	public static bool credits = false;

	public static bool thanksForPlaying_lastInput = false;

	public static bool thanksForPlaying = false;

	public static bool tutorialEnd_LastInput;

	public static bool tutorialEnd;

	public static bool jillQuestComplete;

	public static bool martinQuestComplete;

	public static bool averyQuestComplete;

	public static bool showTownToSis;

	public static bool split_tutorialEnd;

	public static bool split_jillQuest;

	public static bool split_martinQuest;

	public static bool split_averyQuest;

	public static bool split_flashbackEnd;

	public static bool split_credits;

	public static bool split_goHome;

	public static bool split_showTownToSis;

	public static SpeedrunCueTime timerEndPoint;
}
