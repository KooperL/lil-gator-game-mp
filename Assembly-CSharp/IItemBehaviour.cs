using System;

// Token: 0x020001EC RID: 492
public interface IItemBehaviour
{
	// Token: 0x06000A79 RID: 2681
	void Input(bool isDown, bool isHeld);

	// Token: 0x06000A7A RID: 2682
	void Cancel();

	// Token: 0x06000A7B RID: 2683
	void SetEquipped(bool isEquipped);

	// Token: 0x06000A7C RID: 2684
	void OnRemove();

	// Token: 0x06000A7D RID: 2685
	void SetIndex(int index);
}
