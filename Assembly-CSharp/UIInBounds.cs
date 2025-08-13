using System;
using UnityEngine;

// Token: 0x020003AA RID: 938
public class UIInBounds : MonoBehaviour
{
	// Token: 0x060011CD RID: 4557 RVA: 0x0000F34C File Offset: 0x0000D54C
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x0000F35A File Offset: 0x0000D55A
	private void Start()
	{
		this.anchor = this.rectTransform.anchoredPosition;
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x00058FA8 File Offset: 0x000571A8
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

	// Token: 0x04001705 RID: 5893
	public RectTransform boundsReference;

	// Token: 0x04001706 RID: 5894
	public RectTransform canvas;

	// Token: 0x04001707 RID: 5895
	private RectTransform rectTransform;

	// Token: 0x04001708 RID: 5896
	private Vector2 anchor;

	// Token: 0x04001709 RID: 5897
	public GameObject outOfBoundsObject;
}
