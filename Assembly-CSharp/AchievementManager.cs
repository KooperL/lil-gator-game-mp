using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
	// Token: 0x06000206 RID: 518 RVA: 0x00003AAF File Offset: 0x00001CAF
	public static void MarkAchievementToUnlock(Achievement achievement)
	{
		if (AchievementManager.achievementsToUnlock.Contains(achievement))
		{
			return;
		}
		AchievementManager.achievementsToUnlock.Add(achievement);
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0001E574 File Offset: 0x0001C774
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

	// (get) Token: 0x06000208 RID: 520 RVA: 0x00003ACA File Offset: 0x00001CCA
	public static AchievementManager a
	{
		get
		{
			if (AchievementManager.instance == null && Application.isPlaying)
			{
				AchievementManager.instance = global::UnityEngine.Object.FindObjectOfType<AchievementManager>();
				if (AchievementManager.instance != null)
				{
					AchievementManager.instance.Awake();
				}
			}
			return AchievementManager.instance;
		}
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00003B06 File Offset: 0x00001D06
	private void Awake()
	{
		if (AchievementManager.instance == null)
		{
			AchievementManager.instance = this;
		}
		AchievementManager.instance != this;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00003B27 File Offset: 0x00001D27
	private void Start()
	{
		if (AchievementManager.instance != this)
		{
			return;
		}
		this.m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0001E5D4 File Offset: 0x0001C7D4
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

	// Token: 0x0600020C RID: 524 RVA: 0x00003A8E File Offset: 0x00001C8E
	private bool SetAchievementProgress(Achievement achievement, int progress)
	{
		return false;
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0001E6B0 File Offset: 0x0001C8B0
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

	// Token: 0x0600020E RID: 526 RVA: 0x00003B4E File Offset: 0x00001D4E
	internal void LockAchievement(Achievement achievement)
	{
		if (!this.statsRecieved)
		{
			return;
		}
		SteamUserStats.ClearAchievement(achievement.steamID);
		this.Commit();
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00003B6B File Offset: 0x00001D6B
	private void Commit()
	{
		if (SteamUserStats.StoreStats())
		{
			this.needToCommit = false;
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
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

	public const bool hasAchievements = true;

	private static List<Achievement> achievementsToUnlock = new List<Achievement>();

	private static List<AchievementManager.AchievementProgress> achievementProgress = new List<AchievementManager.AchievementProgress>();

	private static AchievementManager instance;

	private bool requestedStats;

	private bool statsRecieved;

	protected Callback<UserStatsReceived_t> m_UserStatsReceived;

	private bool needToCommit;

	private struct AchievementProgress
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00003B91 File Offset: 0x00001D91
		public AchievementProgress(Achievement achievement, int progress)
		{
			this.achievement = achievement;
			this.progress = progress;
		}

		public Achievement achievement;

		public int progress;
	}
}
