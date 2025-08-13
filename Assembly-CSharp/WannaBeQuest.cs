using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000227 RID: 551
public class WannaBeQuest : MonoBehaviour
{
	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00039165 File Offset: 0x00037365
	// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x00039178 File Offset: 0x00037378
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

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0003918B File Offset: 0x0003738B
	// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0003919E File Offset: 0x0003739E
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

	// Token: 0x04000F9D RID: 3997
	public string id;

	// Token: 0x04000F9E RID: 3998
	public string wannaBeId;

	// Token: 0x04000F9F RID: 3999
	public BreakableObject[] targets;

	// Token: 0x04000FA0 RID: 4000
	public DialogueActor[] actors;

	// Token: 0x04000FA1 RID: 4001
	public GameObject crier;

	// Token: 0x04000FA2 RID: 4002
	public string completeCry;

	// Token: 0x04000FA3 RID: 4003
	public string beforeText;

	// Token: 0x04000FA4 RID: 4004
	public string beforeState;

	// Token: 0x04000FA5 RID: 4005
	public UnityEvent onBefore;

	// Token: 0x04000FA6 RID: 4006
	public string rewardText;

	// Token: 0x04000FA7 RID: 4007
	public UnityEvent onReward;

	// Token: 0x04000FA8 RID: 4008
	public string afterText;

	// Token: 0x04000FA9 RID: 4009
	public string afterState;

	// Token: 0x04000FAA RID: 4010
	public UnityEvent onAfter;
}
