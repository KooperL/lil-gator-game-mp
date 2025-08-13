using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000BD RID: 189
public class TightropeAttachments : MonoBehaviour
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000315 RID: 789 RVA: 0x00024674 File Offset: 0x00022874
	public GameObject StaticRoot
	{
		get
		{
			if (this.staticRoot == null)
			{
				this.staticRoot = new GameObject("Attachments (Static)");
				this.staticRoot.transform.position = base.transform.position;
				this.staticRoot.transform.parent = base.transform;
				this.staticRoot.transform.localRotation = Quaternion.identity;
				this.staticRoot.transform.localScale = new Vector3(1f / base.transform.localScale.x, 1f, 1f);
			}
			return this.staticRoot;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000316 RID: 790 RVA: 0x00024724 File Offset: 0x00022924
	public GameObject DynamicRoot
	{
		get
		{
			if (this.dynamicRoot == null)
			{
				this.dynamicRoot = new GameObject("Attachments (Dynamic)");
				this.dynamicRoot.transform.position = base.transform.position;
				this.dynamicRoot.transform.parent = base.transform;
				this.dynamicRoot.transform.localRotation = Quaternion.identity;
				this.dynamicRoot.transform.localScale = new Vector3(1f / base.transform.localScale.x, 1f, 1f);
			}
			return this.dynamicRoot;
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00004617 File Offset: 0x00002817
	private void Awake()
	{
		if (this.attachments.Length != 0)
		{
			this.balanceBeam.onEnable.AddListener(new UnityAction(this.OnBalanceBeamEnabled));
		}
		base.enabled = false;
	}

	// Token: 0x06000318 RID: 792 RVA: 0x000247D4 File Offset: 0x000229D4
	private void OnBalanceBeamEnabled()
	{
		if (this.staticRenderers == null)
		{
			this.staticRenderers = this.StaticRoot.GetComponentsInChildren<Renderer>();
		}
		if (this.dynamicObjects == null || this.dynamicObjects.Length == 0)
		{
			this.dynamicObjects = new GameObject[this.attachments.Length];
			for (int i = 0; i < this.dynamicObjects.Length; i++)
			{
				this.dynamicObjects[i] = Object.Instantiate<GameObject>(this.prefabs[this.attachments[i].index].dynamicPrefab);
				this.dynamicObjects[i].transform.parent = this.DynamicRoot.transform;
				this.dynamicObjects[i].transform.position = this.staticObjects[i].transform.position;
				this.dynamicObjects[i].transform.rotation = this.staticObjects[i].transform.rotation;
			}
		}
		if (this.wobbleBrain == null && this.DynamicRoot.GetComponentInChildren<WobbleBoneBase>() != null)
		{
			this.wobbleBrain = this.DynamicRoot.AddComponent<WobbleBrain>();
		}
		if (this.wobbleBrain != null)
		{
			this.wobbleBrain.enabled = true;
		}
		if (this.dynamicRenderers == null)
		{
			this.dynamicRenderers = this.DynamicRoot.GetComponentsInChildren<Renderer>();
		}
		base.enabled = true;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0002493C File Offset: 0x00022B3C
	private void OnEnable()
	{
		if (this.staticRenderers != null)
		{
			Renderer[] array = this.staticRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}
		if (this.dynamicRenderers != null)
		{
			Renderer[] array = this.dynamicRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00024998 File Offset: 0x00022B98
	private void OnDisable()
	{
		if (this.staticRenderers != null)
		{
			Renderer[] array = this.staticRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}
		if (this.dynamicRenderers != null)
		{
			Renderer[] array = this.dynamicRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}
		if (this.wobbleBrain != null)
		{
			this.wobbleBrain.enabled = false;
		}
	}

	// Token: 0x0600031B RID: 795 RVA: 0x00024A0C File Offset: 0x00022C0C
	private void LateUpdate()
	{
		if (this.tightrope.enabled)
		{
			this.disableTime = Time.time + this.disableDelay;
		}
		for (int i = 0; i < this.attachments.Length; i++)
		{
			this.dynamicObjects[i].transform.position = this.tightrope.GetPointOnTightrope(this.attachments[i].t) + this.offset;
		}
		if (Time.time > this.disableTime)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0400047A RID: 1146
	public BalanceBeam balanceBeam;

	// Token: 0x0400047B RID: 1147
	public Tightrope tightrope;

	// Token: 0x0400047C RID: 1148
	public float disableDelay = 2f;

	// Token: 0x0400047D RID: 1149
	private float disableTime = -1f;

	// Token: 0x0400047E RID: 1150
	public GameObject staticRoot;

	// Token: 0x0400047F RID: 1151
	public GameObject dynamicRoot;

	// Token: 0x04000480 RID: 1152
	[ReadOnly]
	public MeshRenderer staticRenderer;

	// Token: 0x04000481 RID: 1153
	[ReadOnly]
	public MeshFilter staticFilter;

	// Token: 0x04000482 RID: 1154
	public TightropeAttachments.AttachmentPrefab[] prefabs;

	// Token: 0x04000483 RID: 1155
	[Space]
	public TightropeAttachments.Attachment[] attachments;

	// Token: 0x04000484 RID: 1156
	public GameObject[] staticObjects;

	// Token: 0x04000485 RID: 1157
	private Renderer[] staticRenderers;

	// Token: 0x04000486 RID: 1158
	private GameObject[] dynamicObjects;

	// Token: 0x04000487 RID: 1159
	private Renderer[] dynamicRenderers;

	// Token: 0x04000488 RID: 1160
	private WobbleBrain wobbleBrain;

	// Token: 0x04000489 RID: 1161
	public Vector3 offset = new Vector3(0f, -0.05f, 0f);

	// Token: 0x020000BE RID: 190
	[Serializable]
	public struct AttachmentPrefab
	{
		// Token: 0x0400048A RID: 1162
		public GameObject staticPrefab;

		// Token: 0x0400048B RID: 1163
		public GameObject dynamicPrefab;
	}

	// Token: 0x020000BF RID: 191
	[Serializable]
	public struct Attachment
	{
		// Token: 0x0400048C RID: 1164
		[Range(0f, 1f)]
		public float t;

		// Token: 0x0400048D RID: 1165
		public int index;

		// Token: 0x0400048E RID: 1166
		[HideInInspector]
		public int oldIndex;
	}
}
