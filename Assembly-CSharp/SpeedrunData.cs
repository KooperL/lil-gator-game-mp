using System;
using System.Collections.Generic;

// Token: 0x02000301 RID: 769
public static class SpeedrunData
{
	// Token: 0x170001BA RID: 442
	// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0000D377 File Offset: 0x0000B577
	// (set) Token: 0x06000F35 RID: 3893 RVA: 0x0000D37E File Offset: 0x0000B57E
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

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0000D377 File Offset: 0x0000B577
	public static bool ShouldSkip
	{
		get
		{
			return SpeedrunData.isSpeedrunMode;
		}
	}

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
	public static bool GiveAutoName
	{
		get
		{
			return SpeedrunData.isSpeedrunMode && SpeedrunData.state == RunState.Started && SpeedrunData.autoName > AutoNameFunctionality.Off;
		}
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0004FF9C File Offset: 0x0004E19C
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

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0000D377 File Offset: 0x0000B577
	public static bool MaxSpeedAutoSword
	{
		get
		{
			return SpeedrunData.isSpeedrunMode;
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0000D3CB File Offset: 0x0000B5CB
	public static bool IsRunning
	{
		get
		{
			return SpeedrunData.isSpeedrunMode && SpeedrunData.state == RunState.Started;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0000D3DE File Offset: 0x0000B5DE
	public static bool IsTimerRunning
	{
		get
		{
			return SpeedrunData.state == RunState.Started && !SpeedrunData.isLoading;
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0000D377 File Offset: 0x0000B577
	public static bool ShouldTrack
	{
		get
		{
			return SpeedrunData.isSpeedrunMode;
		}
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x0000D3F2 File Offset: 0x0000B5F2
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

	// Token: 0x06000F3E RID: 3902 RVA: 0x00002229 File Offset: 0x00000429
	public static void EndRun()
	{
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x0000D425 File Offset: 0x0000B625
	public static void ResetRun()
	{
		SpeedrunData.state = RunState.NotStarted;
		SpeedrunData.ClearStats();
		if (SpeedrunTimer.instance != null)
		{
			SpeedrunTimer.instance.UpdateIconColor(RunState.NotStarted);
		}
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x00050040 File Offset: 0x0004E240
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

	// Token: 0x06000F41 RID: 3905 RVA: 0x000500A8 File Offset: 0x0004E2A8
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

	// Token: 0x04001389 RID: 5001
	private static bool isSpeedrunMode = false;

	// Token: 0x0400138A RID: 5002
	public static AutoNameFunctionality autoName = AutoNameFunctionality.LilGator;

	// Token: 0x0400138B RID: 5003
	public const bool maxSpeedAutoSword = true;

	// Token: 0x0400138C RID: 5004
	public static bool isLoading = false;

	// Token: 0x0400138D RID: 5005
	public static RunState state = RunState.NotStarted;

	// Token: 0x0400138E RID: 5006
	public static double inGameTime = 0.0;

	// Token: 0x0400138F RID: 5007
	public static List<string> unlockedFriends = new List<string>();

	// Token: 0x04001390 RID: 5008
	public static List<string> unlockedItems = new List<string>();

	// Token: 0x04001391 RID: 5009
	public static List<string> completedQuests = new List<string>();

	// Token: 0x04001392 RID: 5010
	public static bool credits_lastInput = false;

	// Token: 0x04001393 RID: 5011
	public static bool credits = false;

	// Token: 0x04001394 RID: 5012
	public static bool thanksForPlaying_lastInput = false;

	// Token: 0x04001395 RID: 5013
	public static bool thanksForPlaying = false;

	// Token: 0x04001396 RID: 5014
	public static bool tutorialEnd_LastInput;

	// Token: 0x04001397 RID: 5015
	public static bool tutorialEnd;

	// Token: 0x04001398 RID: 5016
	public static bool jillQuestComplete;

	// Token: 0x04001399 RID: 5017
	public static bool martinQuestComplete;

	// Token: 0x0400139A RID: 5018
	public static bool averyQuestComplete;

	// Token: 0x0400139B RID: 5019
	public static bool showTownToSis;

	// Token: 0x0400139C RID: 5020
	public static bool split_tutorialEnd;

	// Token: 0x0400139D RID: 5021
	public static bool split_jillQuest;

	// Token: 0x0400139E RID: 5022
	public static bool split_martinQuest;

	// Token: 0x0400139F RID: 5023
	public static bool split_averyQuest;

	// Token: 0x040013A0 RID: 5024
	public static bool split_flashbackEnd;

	// Token: 0x040013A1 RID: 5025
	public static bool split_credits;

	// Token: 0x040013A2 RID: 5026
	public static bool split_goHome;

	// Token: 0x040013A3 RID: 5027
	public static bool split_showTownToSis;

	// Token: 0x040013A4 RID: 5028
	public static SpeedrunCueTime timerEndPoint;
}
