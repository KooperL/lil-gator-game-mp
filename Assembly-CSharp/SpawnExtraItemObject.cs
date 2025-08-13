using System;
using UnityEngine;

// Token: 0x020001C4 RID: 452
public class SpawnExtraItemObject : MonoBehaviour
{
	// Token: 0x06000951 RID: 2385 RVA: 0x0002C478 File Offset: 0x0002A678
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

	// Token: 0x06000952 RID: 2386 RVA: 0x0002C4DC File Offset: 0x0002A6DC
	private void OnDestroy()
	{
		if (this.instance != null)
		{
			Object.Destroy(this.instance);
		}
	}

	// Token: 0x04000BB6 RID: 2998
	public bool isOnRight;

	// Token: 0x04000BB7 RID: 2999
	public GameObject prefab;

	// Token: 0x04000BB8 RID: 3000
	public SpawnExtraItemObject.PlayerAttachment attachment;

	// Token: 0x04000BB9 RID: 3001
	private GameObject instance;

	// Token: 0x020003DF RID: 991
	public enum PlayerAttachment
	{
		// Token: 0x04001C4F RID: 7247
		None,
		// Token: 0x04001C50 RID: 7248
		Hip
	}
}
