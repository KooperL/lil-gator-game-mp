using System;
using UnityEngine;
using UnityEngine.Events;

public class MultiQuest : MonoBehaviour
{
	// (get) Token: 0x06000B9C RID: 2972 RVA: 0x000389ED File Offset: 0x00036BED
	// (set) Token: 0x06000B9D RID: 2973 RVA: 0x00038A00 File Offset: 0x00036C00
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

	// Token: 0x06000B9E RID: 2974 RVA: 0x00038A14 File Offset: 0x00036C14
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
