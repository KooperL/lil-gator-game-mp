using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003C8 RID: 968
public class UIScrollRepeat : MonoBehaviour
{
	// Token: 0x06001289 RID: 4745 RVA: 0x0005B5CC File Offset: 0x000597CC
	[ContextMenu("Fit to size")]
	public void FitToSize()
	{
		this.tileSize = base.GetComponent<Image>().sprite.rect.size;
		RectTransform component = base.GetComponent<RectTransform>();
		component.offsetMax = 0.5f * this.tileSize;
		component.offsetMin = -0.5f * this.tileSize;
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x0005B628 File Offset: 0x00059828
	private void Start()
	{
		Transform transform = base.transform;
		while (this.selectableRoot == null && transform != null)
		{
			IEventSystemHandler eventSystemHandler;
			if (transform.TryGetComponent<IEventSystemHandler>(ref eventSystemHandler))
			{
				this.selectableRoot = transform.gameObject;
			}
			else
			{
				transform = transform.parent;
			}
		}
		this.tileSize = base.GetComponent<Image>().sprite.rect.size;
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x0005B694 File Offset: 0x00059894
	private void Update()
	{
		if (this.selectableRoot != null && EventSystem.current.currentSelectedGameObject != this.selectableRoot)
		{
			return;
		}
		this.position += Time.deltaTime * this.scrollSpeed;
		for (int i = 0; i < 2; i++)
		{
			if (this.position[i] * Mathf.Sign(this.scrollSpeed[i]) > 0.5f * this.tileSize[i])
			{
				ref Vector2 ptr = ref this.position;
				int num = i;
				ptr[num] -= Mathf.Sign(this.scrollSpeed[i]) * this.tileSize[i];
			}
		}
		Vector2 vector = this.position;
		if (this.pixelPerfect)
		{
			vector..ctor(Mathf.Round(vector.x), Mathf.Round(vector.y));
		}
		base.transform.localPosition = this.position;
	}

	// Token: 0x040017E8 RID: 6120
	public Vector2 scrollSpeed;

	// Token: 0x040017E9 RID: 6121
	public Vector2 tileSize = Vector2.one;

	// Token: 0x040017EA RID: 6122
	public bool pixelPerfect;

	// Token: 0x040017EB RID: 6123
	private Vector2 position;

	// Token: 0x040017EC RID: 6124
	private GameObject selectableRoot;
}
