using System;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class UsePose : MonoBehaviour
{
	// Token: 0x1700018F RID: 399
	// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
	// (set) Token: 0x06000E35 RID: 3637 RVA: 0x0000C9FB File Offset: 0x0000ABFB
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

	// Token: 0x06000E36 RID: 3638 RVA: 0x0000CA0E File Offset: 0x0000AC0E
	private void OnEnable()
	{
		if (this.isFirstEnable)
		{
			this.isFirstEnable = false;
			return;
		}
		this.UpdateState();
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x0000CA26 File Offset: 0x0000AC26
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x0004BD34 File Offset: 0x00049F34
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

	// Token: 0x0400126A RID: 4714
	public string poseId;

	// Token: 0x0400126B RID: 4715
	public string[] poses;

	// Token: 0x0400126C RID: 4716
	public DialogueActor actor;

	// Token: 0x0400126D RID: 4717
	private bool isFirstEnable = true;
}
