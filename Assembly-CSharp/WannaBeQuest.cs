using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002D9 RID: 729
public class WannaBeQuest : MonoBehaviour
{
	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0000CA3D File Offset: 0x0000AC3D
	// (set) Token: 0x06000E3B RID: 3643 RVA: 0x0000CA50 File Offset: 0x0000AC50
	private bool State
	{
		get
		{
			return GameData.g.ReadBool(this.id, false);
		}
		set
		{
			GameData.g.Write(this.id, value);
		}
	}

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0000CA63 File Offset: 0x0000AC63
	// (set) Token: 0x06000E3D RID: 3645 RVA: 0x0000CA76 File Offset: 0x0000AC76
	private int WannaBePose
	{
		get
		{
			return GameData.g.ReadInt(this.wannaBeId, 0);
		}
		set
		{
			GameData.g.Write(this.wannaBeId, value);
		}
	}

	// Token: 0x0400126E RID: 4718
	public string id;

	// Token: 0x0400126F RID: 4719
	public string wannaBeId;

	// Token: 0x04001270 RID: 4720
	public BreakableObject[] targets;

	// Token: 0x04001271 RID: 4721
	public DialogueActor[] actors;

	// Token: 0x04001272 RID: 4722
	public GameObject crier;

	// Token: 0x04001273 RID: 4723
	public string completeCry;

	// Token: 0x04001274 RID: 4724
	public string beforeText;

	// Token: 0x04001275 RID: 4725
	public string beforeState;

	// Token: 0x04001276 RID: 4726
	public UnityEvent onBefore;

	// Token: 0x04001277 RID: 4727
	public string rewardText;

	// Token: 0x04001278 RID: 4728
	public UnityEvent onReward;

	// Token: 0x04001279 RID: 4729
	public string afterText;

	// Token: 0x0400127A RID: 4730
	public string afterState;

	// Token: 0x0400127B RID: 4731
	public UnityEvent onAfter;
}
