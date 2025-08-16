using System;
using Rewired;
using UnityEngine;

public class JunkContainer : MonoBehaviour
{
	// Token: 0x06000839 RID: 2105 RVA: 0x0000817A File Offset: 0x0000637A
	private void Awake()
	{
		this.mainCamera = Camera.main;
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00008187 File Offset: 0x00006387
	private void OnEnable()
	{
		this.interactionCollider.enabled = false;
		this.camera.SetActive(true);
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x000081BA File Offset: 0x000063BA
	private void Start()
	{
		this.ActivateChildren();
		this.SetSelected(this.contents.GetChild(Mathf.FloorToInt(global::UnityEngine.Random.value * (float)this.contents.childCount)));
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x000372E8 File Offset: 0x000354E8
	private void Update()
	{
		foreach (object obj in this.contents)
		{
			Transform transform = (Transform)obj;
			if (!this.bounds.Contains(transform.localPosition))
			{
				transform.parent = base.transform.parent;
				global::UnityEngine.Object.Instantiate<GameObject>(this.junkItemPrefab, transform).GetComponent<JunkItem>().resource = this.itemResource;
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

	// Token: 0x0600083E RID: 2110 RVA: 0x000373E0 File Offset: 0x000355E0
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

	// Token: 0x0600083F RID: 2111 RVA: 0x000081EA File Offset: 0x000063EA
	private void SetSelected(Transform newSelection)
	{
		this.selected = newSelection;
		if (newSelection != null)
		{
			HighlightsFX.h.SetRenderer(new Renderer[] { newSelection.GetComponent<Renderer>() }, Color.white, HighlightsFX.SortingType.Overlay, true);
		}
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00037460 File Offset: 0x00035660
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

	// Token: 0x06000841 RID: 2113 RVA: 0x000374E0 File Offset: 0x000356E0
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

	// Token: 0x06000842 RID: 2114 RVA: 0x0000821C File Offset: 0x0000641C
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
