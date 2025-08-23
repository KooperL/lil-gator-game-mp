using System;
using UnityEngine;
using UnityEngine.Events;

public class MultiQuest : MonoBehaviour
{
	// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0000CAB9 File Offset: 0x0000ACB9
	// (set) Token: 0x06000E3F RID: 3647 RVA: 0x0000CACC File Offset: 0x0000ACCC
	public int Count
	{
		get
		{
			return GameData.g.ReadInt(this.id, 0);
		}
		set
		{
			GameData.g.Write(this.id, value);
		}
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x0004D248 File Offset: 0x0004B448
	public void CompleteSubQuest()
	{
		int num = this.Count + 1;
		this.Count = num;
		if (num == this.totalSubQuests)
		{
			this.onAllComplete.Invoke();
		}
	}

	public string id;

	public int totalSubQuests;

	public UnityEvent onAllComplete;
}
