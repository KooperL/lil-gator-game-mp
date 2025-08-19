using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScrollRepeat : MonoBehaviour
{
	// Token: 0x060012E9 RID: 4841 RVA: 0x0005D558 File Offset: 0x0005B758
	[ContextMenu("Fit to size")]
	public void FitToSize()
	{
		this.tileSize = base.GetComponent<Image>().sprite.rect.size;
		RectTransform component = base.GetComponent<RectTransform>();
		component.offsetMax = 0.5f * this.tileSize;
		component.offsetMin = -0.5f * this.tileSize;
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x0005D5B4 File Offset: 0x0005B7B4
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

	// Token: 0x060012EB RID: 4843 RVA: 0x0005D620 File Offset: 0x0005B820
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

	public Vector2 scrollSpeed;

	public Vector2 tileSize = Vector2.one;

	public bool pixelPerfect;

	private Vector2 position;

	private GameObject selectableRoot;
}
