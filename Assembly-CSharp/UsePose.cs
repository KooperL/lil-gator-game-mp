using System;
using UnityEngine;

public class UsePose : MonoBehaviour
{
	// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0000CCFA File Offset: 0x0000AEFA
	// (set) Token: 0x06000E81 RID: 3713 RVA: 0x0000CD0D File Offset: 0x0000AF0D
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

	// Token: 0x06000E82 RID: 3714 RVA: 0x0000CD20 File Offset: 0x0000AF20
	private void OnEnable()
	{
		if (this.isFirstEnable)
		{
			this.isFirstEnable = false;
			return;
		}
		this.UpdateState();
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0000CD38 File Offset: 0x0000AF38
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x0004D898 File Offset: 0x0004BA98
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
