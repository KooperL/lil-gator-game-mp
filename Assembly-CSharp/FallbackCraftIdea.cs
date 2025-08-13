using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002C0 RID: 704
public class FallbackCraftIdea : MonoBehaviour
{
	// Token: 0x06000DC8 RID: 3528 RVA: 0x0000C642 File Offset: 0x0000A842
	public void Awake()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.CheckState));
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x0000C660 File Offset: 0x0000A860
	private void Start()
	{
		this.CheckState(this.stateMachine.StateID);
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x0004AD30 File Offset: 0x00048F30
	private void CheckState(int stateID)
	{
		if (stateID >= this.minState && !this.craft.IsUnlocked && !this.craft.IsShopUnlocked)
		{
			UIMenus.craftNotification.LoadItem(this.craft);
			this.craft.IsShopUnlocked = true;
		}
	}

	// Token: 0x040011FF RID: 4607
	public QuestStates stateMachine;

	// Token: 0x04001200 RID: 4608
	public int minState;

	// Token: 0x04001201 RID: 4609
	public ItemObject craft;
}
