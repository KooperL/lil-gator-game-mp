using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000185 RID: 389
public class ActorHeldObject : MonoBehaviour
{
	// Token: 0x060007FA RID: 2042 RVA: 0x0002688C File Offset: 0x00024A8C
	[ContextMenu("Get Held Transform")]
	public void GetHeldTransform()
	{
		if (this.heldAnchor == base.transform.parent)
		{
			this.heldPosition = base.transform.localPosition;
			this.heldRotation = base.transform.localRotation;
			return;
		}
		this.heldPosition = this.heldAnchor.InverseTransformPoint(base.transform.position);
		this.heldRotation = this.heldAnchor.rotation.Inverse() * base.transform.rotation;
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00026916 File Offset: 0x00024B16
	[ContextMenu("Get Dropped Transform")]
	public void GetDroppedTransform()
	{
		this.droppedPosition = base.transform.position;
		this.droppedRotation = base.transform.rotation;
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0002693A File Offset: 0x00024B3A
	[ContextMenu("Pick Up")]
	public void PickUp()
	{
		if (!this.isHeld)
		{
			this.isHeld = true;
			CoroutineUtil.Start(this.PickUpCoroutine());
		}
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x00026957 File Offset: 0x00024B57
	private IEnumerator PickUpCoroutine()
	{
		float t = 0f;
		float speed = 5f;
		base.transform.SetParent(this.heldAnchor, true);
		Vector3 oldPosition = base.transform.localPosition;
		Quaternion oldRotation = base.transform.localRotation;
		while (t < 1f)
		{
			t += speed * Time.deltaTime;
			base.transform.localPosition = Vector3.Lerp(oldPosition, this.heldPosition, t);
			base.transform.localRotation = Quaternion.Slerp(oldRotation, this.heldRotation, t);
			yield return null;
		}
		yield break;
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x00026966 File Offset: 0x00024B66
	[ContextMenu("Drop")]
	public void Drop()
	{
		if (this.isHeld)
		{
			this.isHeld = false;
			CoroutineUtil.Start(this.DropCoroutine());
		}
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00026984 File Offset: 0x00024B84
	public static float SolveQuadratic(float a, float b, float c)
	{
		if (b * b - 4f * a * c < 0f)
		{
			return 0f;
		}
		float num = (-b + Mathf.Sqrt(b * b - 4f * a * c)) / (2f * a);
		float num2 = (-b - Mathf.Sqrt(b * b - 4f * a * c)) / (2f * a);
		if (num >= 0f && (num < num2 || num2 < 0f))
		{
			return num;
		}
		if (num2 >= 0f)
		{
			return num2;
		}
		return 0f;
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x00026A0C File Offset: 0x00024C0C
	[ContextMenu("Debug Drop")]
	public void DebugDrop()
	{
		float num = 0f;
		Vector3 position = base.transform.position;
		Vector3 position2 = base.transform.position;
		Quaternion rotation = base.transform.rotation;
		float num2 = (position2.y - position.y) / Time.deltaTime;
		float num3 = position2.y - this.droppedPosition.y;
		float num4 = ActorHeldObject.SolveQuadratic(0.5f * Physics.gravity.y, num2, num3);
		float num5 = 1f / num4;
		float num6 = position2.y;
		float num7 = 0.01f;
		num += num5 * num7;
		num2 += Physics.gravity.y * Time.deltaTime;
		num6 += num2 * Time.deltaTime;
		Vector3 vector = Vector3.Lerp(position2, this.droppedPosition, num);
		if (num < 1f)
		{
			vector.y = num6;
		}
		Debug.Log("velocity = " + ((vector - position2) / num7).ToString());
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x00026B1B File Offset: 0x00024D1B
	private IEnumerator DropCoroutine()
	{
		float t = 0f;
		float speed = 5f;
		Vector3 velRefPosition = base.transform.position;
		yield return null;
		Vector3 oldPosition = base.transform.position;
		Quaternion oldRotation = base.transform.rotation;
		base.transform.SetParent(this.droppedAnchor, true);
		float velocity = (oldPosition.y - velRefPosition.y) / Time.deltaTime;
		float num = oldPosition.y - this.droppedPosition.y;
		float num2 = ActorHeldObject.SolveQuadratic(0.5f * Physics.gravity.y, velocity, num);
		if (num2 > 0f)
		{
			speed = 1f / num2;
			float fallingHeight = oldPosition.y;
			while (t < 1f)
			{
				t += speed * Time.deltaTime;
				velocity += Physics.gravity.y * Time.deltaTime;
				fallingHeight += velocity * Time.deltaTime;
				Vector3 vector = Vector3.Lerp(oldPosition, this.droppedPosition, t);
				if (t < 1f)
				{
					vector.y = fallingHeight;
				}
				base.transform.position = vector;
				base.transform.rotation = Quaternion.Slerp(oldRotation, this.droppedRotation, t);
				yield return null;
			}
		}
		else
		{
			while (t < 1f)
			{
				t += speed * Time.deltaTime;
				base.transform.position = Vector3.Lerp(oldPosition, this.droppedPosition, t);
				base.transform.rotation = Quaternion.Slerp(oldRotation, this.droppedRotation, t);
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x04000A2B RID: 2603
	public bool isHeld;

	// Token: 0x04000A2C RID: 2604
	public Transform heldAnchor;

	// Token: 0x04000A2D RID: 2605
	public Vector3 heldPosition;

	// Token: 0x04000A2E RID: 2606
	public Quaternion heldRotation;

	// Token: 0x04000A2F RID: 2607
	public Transform droppedAnchor;

	// Token: 0x04000A30 RID: 2608
	public Vector3 droppedPosition;

	// Token: 0x04000A31 RID: 2609
	public Quaternion droppedRotation;
}
