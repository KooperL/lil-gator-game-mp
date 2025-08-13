using System;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class UsePose : MonoBehaviour
{
	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x000390D2 File Offset: 0x000372D2
	// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x000390E5 File Offset: 0x000372E5
	private int Pose
	{
		get
		{
			return GameData.g.ReadInt(this.poseId, 0);
		}
		set
		{
			GameData.g.Write(this.poseId, value);
		}
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x000390F8 File Offset: 0x000372F8
	private void OnEnable()
	{
		if (this.isFirstEnable)
		{
			this.isFirstEnable = false;
			return;
		}
		this.UpdateState();
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x00039110 File Offset: 0x00037310
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x00039118 File Offset: 0x00037318
	private void UpdateState()
	{
		int pose = this.Pose;
		for (int i = 0; i < this.poses.Length; i++)
		{
			if (pose == i)
			{
				this.actor.SetStateString(this.poses[i]);
			}
		}
	}

	// Token: 0x04000F99 RID: 3993
	public string poseId;

	// Token: 0x04000F9A RID: 3994
	public string[] poses;

	// Token: 0x04000F9B RID: 3995
	public DialogueActor actor;

	// Token: 0x04000F9C RID: 3996
	private bool isFirstEnable = true;
}
