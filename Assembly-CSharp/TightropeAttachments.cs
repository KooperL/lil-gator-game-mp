using System;
using UnityEngine;
using UnityEngine.Events;

public class TightropeAttachments : MonoBehaviour
{
	// (get) Token: 0x06000322 RID: 802 RVA: 0x000250CC File Offset: 0x000232CC
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

	// (get) Token: 0x06000323 RID: 803 RVA: 0x0002517C File Offset: 0x0002337C
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

	// Token: 0x06000324 RID: 804 RVA: 0x00004703 File Offset: 0x00002903
	private void Awake()
	{
		if (this.attachments.Length != 0)
		{
			this.balanceBeam.onEnable.AddListener(new UnityAction(this.OnBalanceBeamEnabled));
		}
		base.enabled = false;
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0002522C File Offset: 0x0002342C
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
				this.dynamicObjects[i] = global::UnityEngine.Object.Instantiate<GameObject>(this.prefabs[this.attachments[i].index].dynamicPrefab);
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

	// Token: 0x06000326 RID: 806 RVA: 0x00025394 File Offset: 0x00023594
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

	// Token: 0x06000327 RID: 807 RVA: 0x000253F0 File Offset: 0x000235F0
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

	// Token: 0x06000328 RID: 808 RVA: 0x00025464 File Offset: 0x00023664
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

	public BalanceBeam balanceBeam;

	public Tightrope tightrope;

	public float disableDelay = 2f;

	private float disableTime = -1f;

	public GameObject staticRoot;

	public GameObject dynamicRoot;

	[ReadOnly]
	public MeshRenderer staticRenderer;

	[ReadOnly]
	public MeshFilter staticFilter;

	public TightropeAttachments.AttachmentPrefab[] prefabs;

	[Space]
	public TightropeAttachments.Attachment[] attachments;

	public GameObject[] staticObjects;

	private Renderer[] staticRenderers;

	private GameObject[] dynamicObjects;

	private Renderer[] dynamicRenderers;

	private WobbleBrain wobbleBrain;

	public Vector3 offset = new Vector3(0f, -0.05f, 0f);

	[Serializable]
	public struct AttachmentPrefab
	{
		public GameObject staticPrefab;

		public GameObject dynamicPrefab;
	}

	[Serializable]
	public struct Attachment
	{
		[Range(0f, 1f)]
		public float t;

		public int index;

		[HideInInspector]
		public int oldIndex;
	}
}
