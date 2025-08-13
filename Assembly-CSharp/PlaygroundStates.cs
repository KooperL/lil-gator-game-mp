using System;
using UnityEngine;

// Token: 0x02000207 RID: 519
public class PlaygroundStates : MonoBehaviour
{
	// Token: 0x170000EB RID: 235
	// (get) Token: 0x0600099A RID: 2458 RVA: 0x00009537 File Offset: 0x00007737
	private string SaveID
	{
		get
		{
			return "PlaygroundState";
		}
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0000953E File Offset: 0x0000773E
	private void Start()
	{
		this.UpdateState(GameData.g.ReadInt(this.SaveID, 0));
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x00009557 File Offset: 0x00007757
	public void UpdateState(int newState)
	{
		this.state = newState;
		GameData.g.Write(this.SaveID, newState);
		this.Refresh();
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0003A02C File Offset: 0x0003822C
	public void Refresh()
	{
		for (int i = 0; i < this.exclusiveStates.Length; i++)
		{
			if (this.exclusiveStates[i] != null)
			{
				this.exclusiveStates[i].SetActive(i == this.state);
			}
		}
		for (int j = 0; j < this.additiveStates.Length; j++)
		{
			if (this.additiveStates[j] != null)
			{
				this.additiveStates[j].SetActive(j <= this.state);
			}
		}
		this.forestArea.SetActive(GameData.g.ReadBool("Hat_Hero", false) && this.state > 0);
		this.waterArea.SetActive(GameData.g.ReadBool("Triangle_Hero", false) && this.state > 0);
		this.mountainArea.SetActive(GameData.g.ReadBool("Sword_Hero", false) && this.state > 0);
	}

	// Token: 0x04000C41 RID: 3137
	public GameObject[] exclusiveStates;

	// Token: 0x04000C42 RID: 3138
	public GameObject[] additiveStates;

	// Token: 0x04000C43 RID: 3139
	public GameObject forestArea;

	// Token: 0x04000C44 RID: 3140
	public GameObject waterArea;

	// Token: 0x04000C45 RID: 3141
	public GameObject mountainArea;

	// Token: 0x04000C46 RID: 3142
	public int state;
}
