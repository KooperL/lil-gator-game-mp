using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002DC RID: 732
public class UIScrollRepeat : MonoBehaviour
{
	// Token: 0x06000F75 RID: 3957 RVA: 0x0004A210 File Offset: 0x00048410
	[ContextMenu("Fit to size")]
	public void FitToSize()
	{
		this.tileSize = base.GetComponent<Image>().sprite.rect.size;
		RectTransform component = base.GetComponent<RectTransform>();
		component.offsetMax = 0.5f * this.tileSize;
		component.offsetMin = -0.5f * this.tileSize;
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x0004A26C File Offset: 0x0004846C
	private void Start()
	{
		Transform transform = base.transform;
		while (this.selectableRoot == null && transform != null)
		{
			IEventSystemHandler eventSystemHandler;
			if (transform.TryGetComponent<IEventSystemHandler>(out eventSystemHandler))
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

	// Token: 0x06000F77 RID: 3959 RVA: 0x0004A2D8 File Offset: 0x000484D8
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
			vector = new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
		}
		base.transform.localPosition = this.position;
	}

	// Token: 0x04001443 RID: 5187
	public Vector2 scrollSpeed;

	// Token: 0x04001444 RID: 5188
	public Vector2 tileSize = Vector2.one;

	// Token: 0x04001445 RID: 5189
	public bool pixelPerfect;

	// Token: 0x04001446 RID: 5190
	private Vector2 position;

	// Token: 0x04001447 RID: 5191
	private GameObject selectableRoot;
}
