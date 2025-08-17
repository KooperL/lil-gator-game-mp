using System;
using UnityEngine;
using UnityEngine.Events;

public class FallbackCraftIdea : MonoBehaviour
{
	// Token: 0x06000E14 RID: 3604 RVA: 0x0000C94A File Offset: 0x0000AB4A
	public void Awake()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.CheckState));
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x0000C968 File Offset: 0x0000AB68
	private void Start()
	{
		this.CheckState(this.stateMachine.StateID);
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0004C8B8 File Offset: 0x0004AAB8
	private void CheckState(int stateID)
	{
		if (stateID >= this.minState && !this.craft.IsUnlocked && !this.craft.IsShopUnlocked)
		{
			UIMenus.craftNotification.LoadItem(this.craft);
			this.craft.IsShopUnlocked = true;
		}
	}

	public QuestStates stateMachine;

	public int minState;

	public ItemObject craft;
}
