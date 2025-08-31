using System;
using Rewired;
using UnityEngine;

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

	public Collider interactionCollider;

	public GameObject camera;

	public Transform contents;

	public LayerMask contentLayerMask;

	private Transform selected;

	private Vector3 lastPosition;

	private GameObject player;

	private global::Rewired.Player rePlayer;

	private Camera mainCamera;

	public Bounds bounds;

	[Header("Grab Velocity")]
	public float randomness = 10f;

	public Vector3 cameraVelocity = Vector3.back;

	public Vector3 worldVelocity = Vector3.up;

	public float angularVelocity = 360f;

	public ItemResource itemResource;

	public GameObject junkItemPrefab;
}
