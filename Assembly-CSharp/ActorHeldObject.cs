using System;
using System.Collections;
using UnityEngine;

public class ActorHeldObject : MonoBehaviour
{
	// Token: 0x0600099E RID: 2462 RVA: 0x0003AEAC File Offset: 0x000390AC
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

	// Token: 0x0600099F RID: 2463 RVA: 0x000094EC File Offset: 0x000076EC
	[ContextMenu("Get Dropped Transform")]
	public void GetDroppedTransform()
	{
		this.droppedPosition = base.transform.position;
		this.droppedRotation = base.transform.rotation;
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x00009510 File Offset: 0x00007710
	[ContextMenu("Pick Up")]
	public void PickUp()
	{
		if (!this.isHeld)
		{
			this.isHeld = true;
			CoroutineUtil.Start(this.PickUpCoroutine());
		}
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0000952D File Offset: 0x0000772D
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

	// Token: 0x060009A2 RID: 2466 RVA: 0x0000953C File Offset: 0x0000773C
	[ContextMenu("Drop")]
	public void Drop()
	{
		if (this.isHeld)
		{
			this.isHeld = false;
			CoroutineUtil.Start(this.DropCoroutine());
		}
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0003AF38 File Offset: 0x00039138
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

	// Token: 0x060009A4 RID: 2468 RVA: 0x0003AFC0 File Offset: 0x000391C0
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

	// Token: 0x060009A5 RID: 2469 RVA: 0x00009559 File Offset: 0x00007759
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

	public bool isHeld;

	public Transform heldAnchor;

	public Vector3 heldPosition;

	public Quaternion heldRotation;

	public Transform droppedAnchor;

	public Vector3 droppedPosition;

	public Quaternion droppedRotation;
}
