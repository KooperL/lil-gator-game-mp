using System;
using UnityEngine;

public class UIInBounds : MonoBehaviour
{
	// Token: 0x0600122E RID: 4654 RVA: 0x0000F73F File Offset: 0x0000D93F
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x0000F74D File Offset: 0x0000D94D
	private void Start()
	{
		this.anchor = this.rectTransform.anchoredPosition;
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x0005B234 File Offset: 0x00059434
	private void LateUpdate()
	{
		this.rectTransform.anchoredPosition = this.anchor;
		this.rectTransform.ForceUpdateRectTransforms();
		Vector3 vector = this.rectTransform.parent.TransformPoint(this.anchor);
		Vector3 vector2 = this.boundsReference.TransformPoint(this.boundsReference.rect.min);
		Vector3 vector3 = this.boundsReference.TransformPoint(this.boundsReference.rect.max);
		Vector2 vector4 = this.canvas.TransformPoint(this.canvas.rect.max);
		Debug.DrawLine(vector2, vector3);
		Debug.DrawLine(Vector3.zero, vector4);
		bool flag = false;
		if (vector2.x < 0f)
		{
			vector.x += -vector2.x;
			flag = true;
		}
		else if (vector3.x > vector4.x)
		{
			vector.x -= vector3.x - vector4.x;
			flag = true;
		}
		if (vector2.y < 0f)
		{
			vector.y += -vector2.y;
			flag = true;
		}
		else if (vector3.y > vector4.y)
		{
			vector.y -= vector3.y - vector4.y;
			flag = true;
		}
		if (flag)
		{
			this.rectTransform.anchoredPosition = this.rectTransform.parent.InverseTransformPoint(vector);
			if (this.outOfBoundsObject != null)
			{
				this.outOfBoundsObject.SetActive(true);
				return;
			}
		}
		else
		{
			this.rectTransform.anchoredPosition = this.anchor;
			if (this.outOfBoundsObject != null)
			{
				this.outOfBoundsObject.SetActive(false);
			}
		}
	}

	public RectTransform boundsReference;

	public RectTransform canvas;

	private RectTransform rectTransform;

	private Vector2 anchor;

	public GameObject outOfBoundsObject;
}
