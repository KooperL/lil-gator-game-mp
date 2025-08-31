using System;
using UnityEngine;

public class UsePose : MonoBehaviour
{
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

	public string poseId;

	public string[] poses;

	public DialogueActor actor;

	private bool isFirstEnable = true;
}
