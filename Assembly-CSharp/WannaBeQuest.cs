using System;
using UnityEngine;
using UnityEngine.Events;

public class WannaBeQuest : MonoBehaviour
{
	// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0000CD30 File Offset: 0x0000AF30
	// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0000CD43 File Offset: 0x0000AF43
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

	// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0000CD56 File Offset: 0x0000AF56
	// (set) Token: 0x06000E89 RID: 3721 RVA: 0x0000CD69 File Offset: 0x0000AF69
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

	public string id;

	public string wannaBeId;

	public BreakableObject[] targets;

	public DialogueActor[] actors;

	public GameObject crier;

	public string completeCry;

	public string beforeText;

	public string beforeState;

	public UnityEvent onBefore;

	public string rewardText;

	public UnityEvent onReward;

	public string afterText;

	public string afterState;

	public UnityEvent onAfter;
}
