using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x02000092 RID: 146
public class AchievementManager : MonoBehaviour
{
	// Token: 0x060001F9 RID: 505 RVA: 0x000039C3 File Offset: 0x00001BC3
	public static void MarkAchievementToUnlock(Achievement achievement)
	{
		if (AchievementManager.achievementsToUnlock.Contains(achievement))
		{
			return;
		}
		AchievementManager.achievementsToUnlock.Add(achievement);
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0001DB1C File Offset: 0x0001BD1C
	public static void MarkAchievementProgress(Achievement achievement, int progress)
	{
		for (int i = 0; i < AchievementManager.achievementProgress.Count; i++)
		{
			if (AchievementManager.achievementProgress[i].achievement == achievement)
			{
				AchievementManager.achievementProgress[i] = new AchievementManager.AchievementProgress(achievement, progress);
				return;
			}
		}
		AchievementManager.achievementProgress.Add(new AchievementManager.AchievementProgress(achievement, progress));
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060001FB RID: 507 RVA: 0x000039DE File Offset: 0x00001BDE
	public static AchievementManager a
	{
		get
		{
			if (AchievementManager.instance == null && Application.isPlaying)
			{
				AchievementManager.instance = Object.FindObjectOfType<AchievementManager>();
				if (AchievementManager.instance != null)
				{
					AchievementManager.instance.Awake();
				}
			}
			return AchievementManager.instance;
		}
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00003A1A File Offset: 0x00001C1A
	private void Awake()
	{
		if (AchievementManager.instance == null)
		{
			AchievementManager.instance = this;
		}
		AchievementManager.instance != this;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00003A3B File Offset: 0x00001C3B
	private void Start()
	{
		if (AchievementManager.instance != this)
		{
			return;
		}
		this.m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0001DB7C File Offset: 0x0001BD7C
	private void Update()
	{
		if (!SteamManager.Initialized)
		{
			return;
		}
		if (!this.requestedStats && SteamUserStats.RequestCurrentStats())
		{
			this.requestedStats = true;
		}
		if (!this.statsRecieved)
		{
			return;
		}
		if (AchievementManager.achievementProgress.Count > 0)
		{
			while (AchievementManager.achievementProgress.Count > 0)
			{
				if (this.SetAchievementProgress(AchievementManager.achievementProgress[0].achievement, AchievementManager.achievementProgress[0].progress))
				{
					this.needToCommit = true;
				}
				AchievementManager.achievementProgress.RemoveAt(0);
			}
		}
		if (AchievementManager.achievementsToUnlock.Count > 0)
		{
			while (AchievementManager.achievementsToUnlock.Count > 0)
			{
				if (this.UnlockAchievement(AchievementManager.achievementsToUnlock[0]))
				{
					this.needToCommit = true;
				}
				AchievementManager.achievementsToUnlock.RemoveAt(0);
			}
		}
		if (this.needToCommit)
		{
			this.Commit();
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x000039A2 File Offset: 0x00001BA2
	private bool SetAchievementProgress(Achievement achievement, int progress)
	{
		return false;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0001DC58 File Offset: 0x0001BE58
	private bool UnlockAchievement(Achievement achievement)
	{
		bool flag;
		if (SteamUserStats.GetAchievement(achievement.steamID, ref flag) && flag)
		{
			return false;
		}
		SteamUserStats.SetAchievement(achievement.steamID);
		return true;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00003A62 File Offset: 0x00001C62
	internal void LockAchievement(Achievement achievement)
	{
		if (!this.statsRecieved)
		{
			return;
		}
		SteamUserStats.ClearAchievement(achievement.steamID);
		this.Commit();
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00003A7F File Offset: 0x00001C7F
	private void Commit()
	{
		if (SteamUserStats.StoreStats())
		{
			this.needToCommit = false;
		}
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0001DC88 File Offset: 0x0001BE88
	private void OnUserStatsReceived(UserStatsReceived_t pCallback)
	{
		if (1586800UL == pCallback.m_nGameID)
		{
			if (1 == pCallback.m_eResult)
			{
				Debug.Log("Received stats and achievements from Steam\n");
				this.statsRecieved = true;
				return;
			}
			Debug.Log("RequestStats - failed, " + pCallback.m_eResult.ToString());
		}
	}

	// Token: 0x040002E5 RID: 741
	public const bool hasAchievements = true;

	// Token: 0x040002E6 RID: 742
	private static List<Achievement> achievementsToUnlock = new List<Achievement>();

	// Token: 0x040002E7 RID: 743
	private static List<AchievementManager.AchievementProgress> achievementProgress = new List<AchievementManager.AchievementProgress>();

	// Token: 0x040002E8 RID: 744
	private static AchievementManager instance;

	// Token: 0x040002E9 RID: 745
	private bool requestedStats;

	// Token: 0x040002EA RID: 746
	private bool statsRecieved;

	// Token: 0x040002EB RID: 747
	protected Callback<UserStatsReceived_t> m_UserStatsReceived;

	// Token: 0x040002EC RID: 748
	private bool needToCommit;

	// Token: 0x02000093 RID: 147
	private struct AchievementProgress
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00003AA5 File Offset: 0x00001CA5
		public AchievementProgress(Achievement achievement, int progress)
		{
			this.achievement = achievement;
			this.progress = progress;
		}

		// Token: 0x040002ED RID: 749
		public Achievement achievement;

		// Token: 0x040002EE RID: 750
		public int progress;
	}
}
