using System;
using UnityEngine;
using UnityEngine.Events;

public class WannaBeQuest : MonoBehaviour
{
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
