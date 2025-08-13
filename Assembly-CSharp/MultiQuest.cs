using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002C7 RID: 711
public class MultiQuest : MonoBehaviour
{
	// Token: 0x17000186 RID: 390
	// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0000C7A7 File Offset: 0x0000A9A7
	// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x0000C7BA File Offset: 0x0000A9BA
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

	// Token: 0x06000DF3 RID: 3571 RVA: 0x0004B3F8 File Offset: 0x000495F8
	public void CompleteSubQuest()
	{
		int num = this.Count + 1;
		this.Count = num;
		if (num == this.totalSubQuests)
		{
			this.onAllComplete.Invoke();
		}
	}

	// Token: 0x0400122D RID: 4653
	public string id;

	// Token: 0x0400122E RID: 4654
	public int totalSubQuests;

	// Token: 0x0400122F RID: 4655
	public UnityEvent onAllComplete;
}
