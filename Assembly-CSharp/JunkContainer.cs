using System;
using Rewired;
using UnityEngine;

// Token: 0x0200014B RID: 331
public class JunkContainer : MonoBehaviour
{
	// Token: 0x060006BB RID: 1723 RVA: 0x000222A3 File Offset: 0x000204A3
	private void Awake()
	{
		this.mainCamera = Camera.main;
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x000222B0 File Offset: 0x000204B0
	private void OnEnable()
	{
		this.interactionCollider.enabled = false;
		this.camera.SetActive(true);
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x000222E3 File Offset: 0x000204E3
	private void OnDisable()
	{
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x000222E5 File Offset: 0x000204E5
	private void Start()
	{
		this.ActivateChildren();
		this.SetSelected(this.contents.GetChild(Mathf.FloorToInt(Random.value * (float)this.contents.childCount)));
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00022318 File Offset: 0x00020518
	private void Update()
	{
		foreach (object obj in this.contents)
		{
			Transform transform = (Transform)obj;
			if (!this.bounds.Contains(transform.localPosition))
			{
				transform.parent = base.transform.parent;
				Object.Instantiate<GameObject>(this.junkItemPrefab, transform).GetComponent<JunkItem>().resource = this.itemResource;
				if (transform == this.selected)
				{
					this.selected = null;
					HighlightsFX.h.ClearRenderer();
				}
			}
		}
		if (this.contents.childCount == 0)
		{
			this.Deactivate();
		}
		if (this.selected == null)
		{
			this.SetSelected(this.FindClosest(this.lastPosition));
			return;
		}
		this.lastPosition = this.selected.position;
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00022410 File Offset: 0x00020610
	private void ActivateChildren()
	{
		foreach (object obj in this.contents)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.SetActive(false);
			transform.GetComponent<Rigidbody>().isKinematic = false;
			transform.gameObject.isStatic = false;
			transform.gameObject.SetActive(true);
		}
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00022490 File Offset: 0x00020690
	private void SetSelected(Transform newSelection)
	{
		this.selected = newSelection;
		if (newSelection != null)
		{
			HighlightsFX.h.SetRenderer(new Renderer[] { newSelection.GetComponent<Renderer>() }, Color.white, HighlightsFX.SortingType.Overlay, true);
		}
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x000224C4 File Offset: 0x000206C4
	private Transform FindClosest(Vector3 position)
	{
		Transform transform = null;
		float num = float.PositiveInfinity;
		foreach (object obj in this.contents)
		{
			Transform transform2 = (Transform)obj;
			float sqrMagnitude = (transform2.position - position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				transform = transform2;
			}
		}
		return transform;
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00022544 File Offset: 0x00020744
	private Transform FindClosestDirectional(Vector3 position, Vector3 direction)
	{
		Transform transform = null;
		float num = float.PositiveInfinity;
		Transform transform2 = null;
		float num2 = float.NegativeInfinity;
		foreach (object obj in this.contents)
		{
			Transform transform3 = (Transform)obj;
			if (!(transform3 == this.selected))
			{
				Vector3 vector = transform3.position - position;
				float num3 = Vector3.Dot(vector, direction);
				float magnitude = (vector - num3 * direction).magnitude;
				if (num3 > 0f)
				{
					float num4 = num3 + 3f * magnitude;
					if (num4 < num)
					{
						num = num4;
						transform = transform3;
					}
				}
				else
				{
					float num5 = -num3 - 3f * magnitude;
					if (num5 > num2)
					{
						num2 = num5;
						transform2 = transform3;
					}
				}
			}
		}
		if (transform != null)
		{
			return transform;
		}
		return transform2;
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0002263C File Offset: 0x0002083C
	public void Deactivate()
	{
		base.enabled = false;
		this.camera.SetActive(false);
	}

	// Token: 0x04000913 RID: 2323
	public Collider interactionCollider;

	// Token: 0x04000914 RID: 2324
	public GameObject camera;

	// Token: 0x04000915 RID: 2325
	public Transform contents;

	// Token: 0x04000916 RID: 2326
	public LayerMask contentLayerMask;

	// Token: 0x04000917 RID: 2327
	private Transform selected;

	// Token: 0x04000918 RID: 2328
	private Vector3 lastPosition;

	// Token: 0x04000919 RID: 2329
	private GameObject player;

	// Token: 0x0400091A RID: 2330
	private global::Rewired.Player rePlayer;

	// Token: 0x0400091B RID: 2331
	private Camera mainCamera;

	// Token: 0x0400091C RID: 2332
	public Bounds bounds;

	// Token: 0x0400091D RID: 2333
	[Header("Grab Velocity")]
	public float randomness = 10f;

	// Token: 0x0400091E RID: 2334
	public Vector3 cameraVelocity = Vector3.back;

	// Token: 0x0400091F RID: 2335
	public Vector3 worldVelocity = Vector3.up;

	// Token: 0x04000920 RID: 2336
	public float angularVelocity = 360f;

	// Token: 0x04000921 RID: 2337
	public ItemResource itemResource;

	// Token: 0x04000922 RID: 2338
	public GameObject junkItemPrefab;
}
