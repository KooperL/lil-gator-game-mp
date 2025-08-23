using System;
using System.Collections.Generic;

public static class SpeedrunData
{
	// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0000D729 File Offset: 0x0000B929
	// (set) Token: 0x06000F92 RID: 3986 RVA: 0x0000D730 File Offset: 0x0000B930
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

	// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0000D729 File Offset: 0x0000B929
	public static bool ShouldSkip
	{
		get
		{
			return SpeedrunData.isSpeedrunMode;
		}
	}

	// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0000D762 File Offset: 0x0000B962
	public static bool GiveAutoName
	{
		get
		{
			return SpeedrunData.isSpeedrunMode && SpeedrunData.state == RunState.Started && SpeedrunData.autoName > AutoNameFunctionality.Off;
		}
	}

	// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00052188 File Offset: 0x00050388
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

	// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0000D729 File Offset: 0x0000B929
	public static bool MaxSpeedAutoSword
	{
		get
		{
			return SpeedrunData.isSpeedrunMode;
		}
	}

	// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0000D77D File Offset: 0x0000B97D
	public static bool IsRunning
	{
		get
		{
			return SpeedrunData.isSpeedrunMode && SpeedrunData.state == RunState.Started;
		}
	}

	// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0000D790 File Offset: 0x0000B990
	public static bool IsTimerRunning
	{
		get
		{
			return SpeedrunData.state == RunState.Started && !SpeedrunData.isLoading;
		}
	}

	// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0000D729 File Offset: 0x0000B929
	public static bool ShouldTrack
	{
		get
		{
			return SpeedrunData.isSpeedrunMode;
		}
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
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

	// Token: 0x06000F9B RID: 3995 RVA: 0x00002229 File Offset: 0x00000429
	public static void EndRun()
	{
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x0000D7D7 File Offset: 0x0000B9D7
	public static void ResetRun()
	{
		SpeedrunData.state = RunState.NotStarted;
		SpeedrunData.ClearStats();
		if (SpeedrunTimer.instance != null)
		{
			SpeedrunTimer.instance.UpdateIconColor(RunState.NotStarted);
		}
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x0005222C File Offset: 0x0005042C
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

	// Token: 0x06000F9E RID: 3998 RVA: 0x00052294 File Offset: 0x00050494
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
