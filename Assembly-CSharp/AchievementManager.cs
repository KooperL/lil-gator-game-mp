using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class AchievementManager : MonoBehaviour
{
	// Token: 0x060001C3 RID: 451 RVA: 0x00009BEA File Offset: 0x00007DEA
	public static void MarkAchievementToUnlock(Achievement achievement)
	{
		if (AchievementManager.achievementsToUnlock.Contains(achievement))
		{
			return;
		}
		AchievementManager.achievementsToUnlock.Add(achievement);
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00009C08 File Offset: 0x00007E08
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

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060001C5 RID: 453 RVA: 0x00009C66 File Offset: 0x00007E66
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

	// Token: 0x060001C6 RID: 454 RVA: 0x00009CA2 File Offset: 0x00007EA2
	private void Awake()
	{
		if (AchievementManager.instance == null)
		{
			AchievementManager.instance = this;
		}
		AchievementManager.instance != this;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00009CC3 File Offset: 0x00007EC3
	private void Start()
	{
		if (AchievementManager.instance != this)
		{
			return;
		}
		this.m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00009CEC File Offset: 0x00007EEC
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

	// Token: 0x060001C9 RID: 457 RVA: 0x00009DC5 File Offset: 0x00007FC5
	private bool SetAchievementProgress(Achievement achievement, int progress)
	{
		return false;
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00009DC8 File Offset: 0x00007FC8
	private bool UnlockAchievement(Achievement achievement)
	{
		bool flag;
		if (SteamUserStats.GetAchievement(achievement.steamID, out flag) && flag)
		{
			return false;
		}
		SteamUserStats.SetAchievement(achievement.steamID);
		return true;
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00009DF6 File Offset: 0x00007FF6
	internal void LockAchievement(Achievement achievement)
	{
		if (!this.statsRecieved)
		{
			return;
		}
		SteamUserStats.ClearAchievement(achievement.steamID);
		this.Commit();
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00009E13 File Offset: 0x00008013
	private void Commit()
	{
		if (SteamUserStats.StoreStats())
		{
			this.needToCommit = false;
		}
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00009E24 File Offset: 0x00008024
	private void OnUserStatsReceived(UserStatsReceived_t pCallback)
	{
		if (1586800UL == pCallback.m_nGameID)
		{
			if (EResult.k_EResultOK == pCallback.m_eResult)
			{
				Debug.Log("Received stats and achievements from Steam\n");
				this.statsRecieved = true;
				return;
			}
			Debug.Log("RequestStats - failed, " + pCallback.m_eResult.ToString());
		}
	}

	// Token: 0x04000261 RID: 609
	public const bool hasAchievements = true;

	// Token: 0x04000262 RID: 610
	private static List<Achievement> achievementsToUnlock = new List<Achievement>();

	// Token: 0x04000263 RID: 611
	private static List<AchievementManager.AchievementProgress> achievementProgress = new List<AchievementManager.AchievementProgress>();

	// Token: 0x04000264 RID: 612
	private static AchievementManager instance;

	// Token: 0x04000265 RID: 613
	private bool requestedStats;

	// Token: 0x04000266 RID: 614
	private bool statsRecieved;

	// Token: 0x04000267 RID: 615
	protected Callback<UserStatsReceived_t> m_UserStatsReceived;

	// Token: 0x04000268 RID: 616
	private bool needToCommit;

	// Token: 0x02000371 RID: 881
	private struct AchievementProgress
	{
		// Token: 0x06001836 RID: 6198 RVA: 0x00067678 File Offset: 0x00065878
		public AchievementProgress(Achievement achievement, int progress)
		{
			this.achievement = achievement;
			this.progress = progress;
		}

		// Token: 0x04001A62 RID: 6754
		public Achievement achievement;

		// Token: 0x04001A63 RID: 6755
		public int progress;
	}
}
