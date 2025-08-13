using System;
using Rewired;
using UnityEngine;

// Token: 0x020001AF RID: 431
public class JunkContainer : MonoBehaviour
{
	// Token: 0x060007F9 RID: 2041 RVA: 0x00007E80 File Offset: 0x00006080
	private void Awake()
	{
		this.mainCamera = Camera.main;
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00007E8D File Offset: 0x0000608D
	private void OnEnable()
	{
		this.interactionCollider.enabled = false;
		this.camera.SetActive(true);
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x00007EC0 File Offset: 0x000060C0
	private void Start()
	{
		this.ActivateChildren();
		this.SetSelected(this.contents.GetChild(Mathf.FloorToInt(Random.value * (float)this.contents.childCount)));
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x00035B60 File Offset: 0x00033D60
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

	// Token: 0x060007FE RID: 2046 RVA: 0x00035C58 File Offset: 0x00033E58
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

	// Token: 0x060007FF RID: 2047 RVA: 0x00007EF0 File Offset: 0x000060F0
	private void SetSelected(Transform newSelection)
	{
		this.selected = newSelection;
		if (newSelection != null)
		{
			HighlightsFX.h.SetRenderer(new Renderer[] { newSelection.GetComponent<Renderer>() }, Color.white, HighlightsFX.SortingType.Overlay, true);
		}
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x00035CD8 File Offset: 0x00033ED8
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

	// Token: 0x06000801 RID: 2049 RVA: 0x00035D58 File Offset: 0x00033F58
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

	// Token: 0x06000802 RID: 2050 RVA: 0x00007F22 File Offset: 0x00006122
	public void Deactivate()
	{
		base.enabled = false;
		this.camera.SetActive(false);
	}

	// Token: 0x04000A9B RID: 2715
	public Collider interactionCollider;

	// Token: 0x04000A9C RID: 2716
	public GameObject camera;

	// Token: 0x04000A9D RID: 2717
	public Transform contents;

	// Token: 0x04000A9E RID: 2718
	public LayerMask contentLayerMask;

	// Token: 0x04000A9F RID: 2719
	private Transform selected;

	// Token: 0x04000AA0 RID: 2720
	private Vector3 lastPosition;

	// Token: 0x04000AA1 RID: 2721
	private GameObject player;

	// Token: 0x04000AA2 RID: 2722
	private Player rePlayer;

	// Token: 0x04000AA3 RID: 2723
	private Camera mainCamera;

	// Token: 0x04000AA4 RID: 2724
	public Bounds bounds;

	// Token: 0x04000AA5 RID: 2725
	[Header("Grab Velocity")]
	public float randomness = 10f;

	// Token: 0x04000AA6 RID: 2726
	public Vector3 cameraVelocity = Vector3.back;

	// Token: 0x04000AA7 RID: 2727
	public Vector3 worldVelocity = Vector3.up;

	// Token: 0x04000AA8 RID: 2728
	public float angularVelocity = 360f;

	// Token: 0x04000AA9 RID: 2729
	public ItemResource itemResource;

	// Token: 0x04000AAA RID: 2730
	public GameObject junkItemPrefab;
}
