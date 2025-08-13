using System;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class PlaygroundStates : MonoBehaviour
{
	// Token: 0x17000071 RID: 113
	// (get) Token: 0x0600082B RID: 2091 RVA: 0x0002718E File Offset: 0x0002538E
	private string SaveID
	{
		get
		{
			return "PlaygroundState";
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00027195 File Offset: 0x00025395
	private void Start()
	{
		this.UpdateState(GameData.g.ReadInt(this.SaveID, 0));
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x000271AE File Offset: 0x000253AE
	public void UpdateState(int newState)
	{
		this.state = newState;
		GameData.g.Write(this.SaveID, newState);
		this.Refresh();
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000271D0 File Offset: 0x000253D0
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

	// Token: 0x04000A55 RID: 2645
	public GameObject[] exclusiveStates;

	// Token: 0x04000A56 RID: 2646
	public GameObject[] additiveStates;

	// Token: 0x04000A57 RID: 2647
	public GameObject forestArea;

	// Token: 0x04000A58 RID: 2648
	public GameObject waterArea;

	// Token: 0x04000A59 RID: 2649
	public GameObject mountainArea;

	// Token: 0x04000A5A RID: 2650
	public int state;
}
