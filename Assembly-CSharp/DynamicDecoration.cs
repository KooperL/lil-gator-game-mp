using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class DynamicDecoration : MonoBehaviour
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005DF6 File Offset: 0x00003FF6
	// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005DFE File Offset: 0x00003FFE
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

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005E16 File Offset: 0x00004016
	// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005E1E File Offset: 0x0000401E
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

	// Token: 0x060000D4 RID: 212 RVA: 0x00005E36 File Offset: 0x00004036
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00005E40 File Offset: 0x00004040
	[ContextMenu("Snap to ground")]
	public void SnapToGround()
	{
		this.staticRoot.SetActive(false);
		RaycastHit raycastHit;
		if (this.physics.rigidbody.SweepTest(Vector3.down, out raycastHit, 1f))
		{
			base.transform.position += raycastHit.distance * Vector3.down;
		}
		this.staticRoot.SetActive(true);
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00005EAC File Offset: 0x000040AC
	public void Settle()
	{
		this.staticRoot.SetActive(false);
		for (int i = 0; i < 1; i++)
		{
			RaycastHit[] array = this.physics.rigidbody.SweepTestAll(Vector3.down, 1f, QueryTriggerInteraction.Ignore);
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
				if (this.physics.rigidbody.SweepTest(Vector3.down, out raycastHit5, 1f))
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

	// Token: 0x060000D7 RID: 215 RVA: 0x000061BC File Offset: 0x000043BC
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

	// Token: 0x04000120 RID: 288
	public DynamicDecoration.Importance importance = DynamicDecoration.Importance.Medium;

	// Token: 0x04000121 RID: 289
	[SerializeField]
	private bool isStatic;

	// Token: 0x04000122 RID: 290
	[SerializeField]
	[ReadOnly]
	private bool isAwake;

	// Token: 0x04000123 RID: 291
	[Space]
	public GameObject physicsRoot;

	// Token: 0x04000124 RID: 292
	public Renderer physicsRenderer;

	// Token: 0x04000125 RID: 293
	public DynamicDecorationPhysics physics;

	// Token: 0x04000126 RID: 294
	[Space]
	public GameObject staticRoot;

	// Token: 0x04000127 RID: 295
	public Renderer staticRenderer;

	// Token: 0x04000128 RID: 296
	public Collider[] staticColliders;

	// Token: 0x04000129 RID: 297
	[Space]
	[ReadOnly]
	public DynamicDecoration.DynamicDecorationState state;

	// Token: 0x0400012A RID: 298
	private const int settleIterations = 1;

	// Token: 0x0200035D RID: 861
	public enum Importance
	{
		// Token: 0x04001A0B RID: 6667
		High = 2,
		// Token: 0x04001A0C RID: 6668
		Medium = 1,
		// Token: 0x04001A0D RID: 6669
		Low = 0
	}

	// Token: 0x0200035E RID: 862
	public enum DynamicDecorationState
	{
		// Token: 0x04001A0F RID: 6671
		Static,
		// Token: 0x04001A10 RID: 6672
		Asleep,
		// Token: 0x04001A11 RID: 6673
		Awake
	}
}
