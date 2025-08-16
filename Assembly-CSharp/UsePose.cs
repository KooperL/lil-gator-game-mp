using System;
using UnityEngine;

public class UsePose : MonoBehaviour
{
	// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0000CCDB File Offset: 0x0000AEDB
	// (set) Token: 0x06000E81 RID: 3713 RVA: 0x0000CCEE File Offset: 0x0000AEEE
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

	// Token: 0x06000E82 RID: 3714 RVA: 0x0000CD01 File Offset: 0x0000AF01
	private void OnEnable()
	{
		if (this.isFirstEnable)
		{
			this.isFirstEnable = false;
			return;
		}
		this.UpdateState();
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0000CD19 File Offset: 0x0000AF19
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x0004D728 File Offset: 0x0004B928
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
