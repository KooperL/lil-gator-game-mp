using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class DynamicDecoration : MonoBehaviour
{
	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002BE5 File Offset: 0x00000DE5
	// (set) Token: 0x060000EA RID: 234 RVA: 0x00002BED File Offset: 0x00000DED
	public bool IsStatic
	{
		get
		{
			return this.isStatic;
		}
		set
		{
			if (this.isStatic != value)
			{
				this.isStatic = value;
				this.UpdateState();
			}
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060000EB RID: 235 RVA: 0x00002C05 File Offset: 0x00000E05
	// (set) Token: 0x060000EC RID: 236 RVA: 0x00002C0D File Offset: 0x00000E0D
	public bool IsAwake
	{
		get
		{
			return this.isAwake;
		}
		set
		{
			if (this.isAwake != value)
			{
				this.isAwake = value;
				this.UpdateState();
			}
		}
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00002C25 File Offset: 0x00000E25
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x060000EE RID: 238 RVA: 0x0001A844 File Offset: 0x00018A44
	[ContextMenu("Snap to ground")]
	public void SnapToGround()
	{
		this.staticRoot.SetActive(false);
		RaycastHit raycastHit;
		if (this.physics.rigidbody.SweepTest(Vector3.down, ref raycastHit, 1f))
		{
			base.transform.position += raycastHit.distance * Vector3.down;
		}
		this.staticRoot.SetActive(true);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x0001A8B0 File Offset: 0x00018AB0
	public void Settle()
	{
		this.staticRoot.SetActive(false);
		for (int i = 0; i < 1; i++)
		{
			RaycastHit[] array = this.physics.rigidbody.SweepTestAll(Vector3.down, 1f, 1);
			if (array.Length == 0)
			{
				return;
			}
			RaycastHit raycastHit2;
			RaycastHit raycastHit = (raycastHit2 = array[0]);
			float num = 100f;
			foreach (RaycastHit raycastHit3 in array)
			{
				if (raycastHit3.distance < num)
				{
					raycastHit = raycastHit3;
					num = raycastHit3.distance;
				}
			}
			Vector3 worldCenterOfMass = this.physics.rigidbody.worldCenterOfMass;
			Vector3 vector = raycastHit.point - worldCenterOfMass;
			vector.y = 0f;
			float num2;
			if (vector.magnitude < 0.05f)
			{
				string text = "Nearest: ";
				Vector3 vector2 = vector;
				Debug.Log(text + vector2.ToString() + ", distance: " + raycastHit.distance.ToString());
				num2 = raycastHit.distance;
			}
			else
			{
				num = 100f;
				Vector3 vector3 = Vector3.zero;
				foreach (RaycastHit raycastHit4 in array)
				{
					Vector3 vector4 = raycastHit4.point - worldCenterOfMass;
					vector4.y = 0f;
					if (raycastHit4.distance < num && Vector3.Dot(vector4, vector) < -0.1f)
					{
						raycastHit2 = raycastHit4;
						vector3 = vector4;
						num = raycastHit4.distance;
					}
				}
				string[] array3 = new string[8];
				array3[0] = "Nearest: ";
				int num3 = 1;
				Vector3 vector2 = vector;
				array3[num3] = vector2.ToString();
				array3[2] = ", distance: ";
				array3[3] = raycastHit.distance.ToString();
				array3[4] = "\nOpposite: ";
				int num4 = 5;
				vector2 = vector3;
				array3[num4] = vector2.ToString();
				array3[6] = ", distance: ";
				array3[7] = raycastHit2.distance.ToString();
				Debug.Log(string.Concat(array3));
				vector.y = -raycastHit.distance;
				vector3.y = -raycastHit2.distance;
				Vector3 normalized = (vector - vector3).normalized;
				Vector3 vector5 = normalized;
				vector5.y = 0f;
				base.transform.rotation *= Quaternion.FromToRotation(vector5, normalized);
				this.physics.rigidbody.rotation = base.transform.rotation;
				RaycastHit raycastHit5;
				if (this.physics.rigidbody.SweepTest(Vector3.down, ref raycastHit5, 1f))
				{
					num2 = raycastHit5.distance;
				}
				else
				{
					num2 = Mathf.Lerp(raycastHit.distance, raycastHit2.distance, 0.5f);
				}
			}
			base.transform.position += Vector3.down * num2;
			this.physics.rigidbody.position = base.transform.position;
		}
		this.staticRoot.SetActive(true);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0001ABC0 File Offset: 0x00018DC0
	[ContextMenu("Update State")]
	private void UpdateState()
	{
		if (this == null || !Application.IsPlaying(this))
		{
			return;
		}
		if (this.isStatic)
		{
			this.state = DynamicDecoration.DynamicDecorationState.Static;
		}
		else if (this.isAwake)
		{
			this.state = DynamicDecoration.DynamicDecorationState.Awake;
		}
		else
		{
			this.state = DynamicDecoration.DynamicDecorationState.Asleep;
		}
		bool flag = this.state != DynamicDecoration.DynamicDecorationState.Awake;
		this.staticRenderer.enabled = flag;
		this.physicsRenderer.enabled = !flag;
		bool flag2 = this.state == DynamicDecoration.DynamicDecorationState.Static;
		Collider[] array = this.staticColliders;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = flag2;
		}
		this.physicsRoot.SetActive(!flag2);
	}

	// Token: 0x04000158 RID: 344
	public DynamicDecoration.Importance importance = DynamicDecoration.Importance.Medium;

	// Token: 0x04000159 RID: 345
	[SerializeField]
	private bool isStatic;

	// Token: 0x0400015A RID: 346
	[SerializeField]
	[ReadOnly]
	private bool isAwake;

	// Token: 0x0400015B RID: 347
	[Space]
	public GameObject physicsRoot;

	// Token: 0x0400015C RID: 348
	public Renderer physicsRenderer;

	// Token: 0x0400015D RID: 349
	public DynamicDecorationPhysics physics;

	// Token: 0x0400015E RID: 350
	[Space]
	public GameObject staticRoot;

	// Token: 0x0400015F RID: 351
	public Renderer staticRenderer;

	// Token: 0x04000160 RID: 352
	public Collider[] staticColliders;

	// Token: 0x04000161 RID: 353
	[Space]
	[ReadOnly]
	public DynamicDecoration.DynamicDecorationState state;

	// Token: 0x04000162 RID: 354
	private const int settleIterations = 1;

	// Token: 0x02000044 RID: 68
	public enum Importance
	{
		// Token: 0x04000164 RID: 356
		High = 2,
		// Token: 0x04000165 RID: 357
		Medium = 1,
		// Token: 0x04000166 RID: 358
		Low = 0
	}

	// Token: 0x02000045 RID: 69
	public enum DynamicDecorationState
	{
		// Token: 0x04000168 RID: 360
		Static,
		// Token: 0x04000169 RID: 361
		Asleep,
		// Token: 0x0400016A RID: 362
		Awake
	}
}
