using System;
using UnityEngine;

public class SpawnExtraItemObject : MonoBehaviour
{
	// Token: 0x06000B34 RID: 2868 RVA: 0x000400D8 File Offset: 0x0003E2D8
	private void Start()
	{
		Transform transform = null;
		if (this.attachment == SpawnExtraItemObject.PlayerAttachment.Hip)
		{
			transform = (this.isOnRight ? Player.itemManager.hipAnchor_r : Player.itemManager.hipAnchor);
		}
		if (transform != null)
		{
			this.instance = global::UnityEngine.Object.Instantiate<GameObject>(this.prefab, transform);
			return;
		}
		this.instance = global::UnityEngine.Object.Instantiate<GameObject>(this.prefab);
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x0000A918 File Offset: 0x00008B18
	private void OnDestroy()
	{
		if (this.instance != null)
		{
			global::UnityEngine.Object.Destroy(this.instance);
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
