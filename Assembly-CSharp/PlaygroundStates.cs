using System;
using UnityEngine;

public class PlaygroundStates : MonoBehaviour
{
	// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0000986B File Offset: 0x00007A6B
	private string SaveID
	{
		get
		{
			return "PlaygroundState";
		}
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x00009872 File Offset: 0x00007A72
	private void Start()
	{
		this.UpdateState(GameData.g.ReadInt(this.SaveID, 0));
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0000988B File Offset: 0x00007A8B
	public void UpdateState(int newState)
	{
		this.state = newState;
		GameData.g.Write(this.SaveID, newState);
		this.Refresh();
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0003BA9C File Offset: 0x00039C9C
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

	public GameObject[] exclusiveStates;

	public GameObject[] additiveStates;

	public GameObject forestArea;

	public GameObject waterArea;

	public GameObject mountainArea;

	public int state;
}
