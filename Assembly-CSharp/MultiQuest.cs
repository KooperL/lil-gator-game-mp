using System;
using UnityEngine;
using UnityEngine.Events;

public class MultiQuest : MonoBehaviour
{
	// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0000CA9A File Offset: 0x0000AC9A
	// (set) Token: 0x06000E3E RID: 3646 RVA: 0x0000CAAD File Offset: 0x0000ACAD
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

	// Token: 0x06000E3F RID: 3647 RVA: 0x0004CDEC File Offset: 0x0004AFEC
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
