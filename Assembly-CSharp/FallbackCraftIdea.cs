using System;
using UnityEngine;
using UnityEngine.Events;

public class FallbackCraftIdea : MonoBehaviour
{
	// Token: 0x06000B85 RID: 2949 RVA: 0x0003871D File Offset: 0x0003691D
	public void Awake()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.CheckState));
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x0003873B File Offset: 0x0003693B
	private void Start()
	{
		this.CheckState(this.stateMachine.StateID);
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00038750 File Offset: 0x00036950
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
