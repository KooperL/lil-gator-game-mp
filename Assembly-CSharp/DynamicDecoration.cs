using System;
using UnityEngine;

public class DynamicDecoration : MonoBehaviour
{
	// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002C49 File Offset: 0x00000E49
	// (set) Token: 0x060000F2 RID: 242 RVA: 0x00002C51 File Offset: 0x00000E51
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

	// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002C69 File Offset: 0x00000E69
	// (set) Token: 0x060000F4 RID: 244 RVA: 0x00002C71 File Offset: 0x00000E71
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

	// Token: 0x060000F5 RID: 245 RVA: 0x00002C89 File Offset: 0x00000E89
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x0001B084 File Offset: 0x00019284
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

	// Token: 0x060000F7 RID: 247 RVA: 0x0001B0F0 File Offset: 0x000192F0
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

	// Token: 0x060000F8 RID: 248 RVA: 0x0001B400 File Offset: 0x00019600
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

	public DynamicDecoration.Importance importance = DynamicDecoration.Importance.Medium;

	[SerializeField]
	private bool isStatic;

	[SerializeField]
	[ReadOnly]
	private bool isAwake;

	[Space]
	public GameObject physicsRoot;

	public Renderer physicsRenderer;

	public DynamicDecorationPhysics physics;

	[Space]
	public GameObject staticRoot;

	public Renderer staticRenderer;

	public Collider[] staticColliders;

	[Space]
	[ReadOnly]
	public DynamicDecoration.DynamicDecorationState state;

	private const int settleIterations = 1;

	public enum Importance
	{
		High = 2,
		Medium = 1,
		Low = 0
	}

	public enum DynamicDecorationState
	{
		Static,
		Asleep,
		Awake
	}
}
