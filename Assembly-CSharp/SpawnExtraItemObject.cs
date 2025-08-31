using System;
using UnityEngine;

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

	public bool isOnRight;

	public GameObject prefab;

	public SpawnExtraItemObject.PlayerAttachment attachment;

	private GameObject instance;

	public enum PlayerAttachment
	{
		None,
		Hip
	}
}
