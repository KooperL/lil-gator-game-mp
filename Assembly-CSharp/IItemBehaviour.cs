using System;

// Token: 0x02000278 RID: 632
public interface IItemBehaviour
{
	// Token: 0x06000C2E RID: 3118
	void Input(bool isDown, bool isHeld);

	// Token: 0x06000C2F RID: 3119
	void Cancel();

	// Token: 0x06000C30 RID: 3120
	void SetEquipped(bool isEquipped);

	// Token: 0x06000C31 RID: 3121
	void OnRemove();

	// Token: 0x06000C32 RID: 3122
	void SetIndex(int index);
}
