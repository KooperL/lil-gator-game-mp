using System;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class SpawnExtraItemObject : MonoBehaviour
{
	// Token: 0x06000AE8 RID: 2792 RVA: 0x0003E5F4 File Offset: 0x0003C7F4
	private void Start()
	{
		Transform transform = null;
		if (this.attachment == SpawnExtraItemObject.PlayerAttachment.Hip)
		{
			transform = (this.isOnRight ? Player.itemManager.hipAnchor_r : Player.itemManager.hipAnchor);
		}
		if (transform != null)
		{
			this.instance = Object.Instantiate<GameObject>(this.prefab, transform);
			return;
		}
		this.instance = Object.Instantiate<GameObject>(this.prefab);
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x0000A5DA File Offset: 0x000087DA
	private void OnDestroy()
	{
		if (this.instance != null)
		{
			Object.Destroy(this.instance);
		}
	}

	// Token: 0x04000DD9 RID: 3545
	public bool isOnRight;

	// Token: 0x04000DDA RID: 3546
	public GameObject prefab;

	// Token: 0x04000DDB RID: 3547
	public SpawnExtraItemObject.PlayerAttachment attachment;

	// Token: 0x04000DDC RID: 3548
	private GameObject instance;

	// Token: 0x02000247 RID: 583
	public enum PlayerAttachment
	{
		// Token: 0x04000DDE RID: 3550
		None,
		// Token: 0x04000DDF RID: 3551
		Hip
	}
}
