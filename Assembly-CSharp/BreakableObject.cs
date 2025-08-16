using System;
using UnityEngine;
using UnityEngine.Events;

public class BreakableObject : PersistentObject, IOnTimeout
{
	// (get) Token: 0x060007BA RID: 1978 RVA: 0x00007B0B File Offset: 0x00005D0B
	public bool IsBroken
	{
		get
		{
			if (!this.isLoaded)
			{
				this.Load(base.PersistentState);
				this.isLoaded = true;
			}
			return this.isBroken;
		}
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00007B2E File Offset: 0x00005D2E
	public override void OnValidate()
	{
		base.OnValidate();
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x0003598C File Offset: 0x00033B8C
	public override void Load(bool state)
	{
		this.isBroken = state;
		this.intactObject.SetActive(!this.isBroken);
		if (this.crackedObject != null)
		{
			this.crackedObject.SetActive(false);
		}
		if (this.brokenObject != null)
		{
			this.brokenObject.SetActive(this.isBroken);
			if (this.isBroken)
			{
				this.brokenObject.transform.rotation = Quaternion.Euler(0f, global::UnityEngine.Random.value * 360f, 0f);
			}
		}
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x00007B36 File Offset: 0x00005D36
	public virtual void OnEnable()
	{
		this.Load(this.isBroken);
		if (this.breakingObject != null)
		{
			this.breakingObject = null;
		}
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x00035A20 File Offset: 0x00033C20
	public virtual void OnDisable()
	{
		if (this.isBroken && this.breakingObject != null)
		{
			if (this.waitForValidation)
			{
				global::UnityEngine.Object.Destroy(this.breakingObject);
			}
			this.breakingObject = null;
		}
		if (this.waitForValidation)
		{
			this.isBroken = false;
		}
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00035A6C File Offset: 0x00033C6C
	private bool IsRanged(bool isHeavy = false)
	{
		if (isHeavy)
		{
			return false;
		}
		if (Time.time - Weapon.lastWeaponHitTime < 0.03f)
		{
			return false;
		}
		if (this.bonkBoxReference != null)
		{
			if (Vector3.Distance(this.bonkBoxReference.ClosestPoint(Player.Position), Player.Position) < 1.5f)
			{
				return false;
			}
		}
		else if (Vector3.Distance(base.transform.position, Player.Position) < 1.5f)
		{
			return false;
		}
		return true;
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00035AE4 File Offset: 0x00033CE4
	private bool CanBeDestroyed(bool isHeavy = false, bool isRanged = false)
	{
		if (this.isNonRanged && isRanged)
		{
			return false;
		}
		if (this.isInvincible)
		{
			return false;
		}
		if (!this.isSturdy)
		{
			return true;
		}
		if (isHeavy)
		{
			return true;
		}
		if (Player.movement.isSledding)
		{
			return true;
		}
		if (Player.movement.isRagdolling)
		{
			return true;
		}
		this.sturdyHits--;
		if (this.sturdyHits <= 0)
		{
			return true;
		}
		if (this.crackedObject != null)
		{
			this.crackedObject.SetActive(true);
			this.intactObject.SetActive(false);
		}
		return false;
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x00007B59 File Offset: 0x00005D59
	public virtual void Break()
	{
		this.Break(false, Vector3.zero, false);
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00035B74 File Offset: 0x00033D74
	public virtual void Break(bool fromAttachment, Vector3 velocity, bool isHeavy = false)
	{
		if (Time.time - this.lastBreakTime < 0.1f)
		{
			return;
		}
		this.lastBreakTime = Time.time;
		if (this.isBroken)
		{
			return;
		}
		bool flag = this.IsRanged(isHeavy);
		if (this.CanBeDestroyed(isHeavy, flag))
		{
			this.isBroken = true;
			if (this.childBreakables != null && this.childBreakables.Length != 0)
			{
				foreach (BreakableObject breakableObject in this.childBreakables)
				{
					if (breakableObject != null && !breakableObject.isBroken)
					{
						breakableObject.Break(true, velocity, false);
					}
				}
			}
			if (this.isAttached && this.parentBreakable != null && this.breakParent && !this.parentBreakable.isBroken)
			{
				this.parentBreakable.Break(true, velocity, false);
			}
			this.intactObject.SetActive(false);
			if (this.crackedObject != null)
			{
				this.crackedObject.SetActive(false);
			}
			if (this.breakingPrefab != null)
			{
				this.breakingObject = global::UnityEngine.Object.Instantiate<GameObject>(this.breakingPrefab, base.transform.position, base.transform.rotation);
				this.breakingObject.transform.localScale = base.transform.lossyScale.x * Vector3.one;
				if (fromAttachment)
				{
					RandomizePhysics[] componentsInChildren = this.breakingObject.GetComponentsInChildren<RandomizePhysics>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].enabled = false;
					}
				}
				if (!fromAttachment && velocity != Vector3.zero)
				{
					Rigidbody[] componentsInChildren2 = this.breakingObject.GetComponentsInChildren<Rigidbody>();
					for (int i = 0; i < componentsInChildren2.Length; i++)
					{
						componentsInChildren2[i].velocity += global::UnityEngine.Random.Range(0.75f, 1f) * velocity;
					}
				}
				DistanceTimeout component = this.breakingObject.GetComponent<DistanceTimeout>();
				if (component != null)
				{
					component.callback = this;
				}
			}
			if (this.onBreak != null)
			{
				this.onBreak.Invoke();
			}
			if (this.waitForValidation)
			{
				if (this.breakingObject != null)
				{
					foreach (TriggerPickup triggerPickup in this.breakingObject.GetComponentsInChildren<TriggerPickup>())
					{
						triggerPickup.Initialize();
						triggerPickup.enabled = false;
					}
					return;
				}
			}
			else
			{
				this.SaveTrue();
			}
			return;
		}
		if (this.bonkBoxReference != null)
		{
			EffectsManager.e.Bonk(HitTrigger.currentHitPoint, this.bonkBoxReference);
		}
		else
		{
			EffectsManager.e.Bonk(HitTrigger.currentHitPoint);
		}
		if (this.bonkSound != null)
		{
			this.bonkSound.Play();
		}
		this.onHit.Invoke();
		if (flag)
		{
			this.onRangedHit.Invoke();
			return;
		}
		this.onNearbyHit.Invoke();
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00035E34 File Offset: 0x00034034
	public void OnTimeout()
	{
		this.breakingObject = null;
		if (!this.isPersistent && this.respawn)
		{
			this.isBroken = false;
			this.intactObject.SetActive(true);
			if (this.crackedObject != null)
			{
				this.crackedObject.SetActive(false);
				return;
			}
		}
		else if (this.brokenObject != null)
		{
			this.brokenObject.SetActive(true);
			this.brokenObject.transform.rotation = Quaternion.Euler(0f, global::UnityEngine.Random.value * 360f, 0f);
		}
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x00035ECC File Offset: 0x000340CC
	public void ValidateBreak()
	{
		this.waitForValidation = false;
		if (this.isBroken)
		{
			this.SaveTrue();
			if (this.breakingObject != null)
			{
				TriggerPickup[] componentsInChildren = this.breakingObject.GetComponentsInChildren<TriggerPickup>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = true;
				}
			}
		}
	}

	public GameObject intactObject;

	public GameObject brokenObject;

	public GameObject breakingPrefab;

	private GameObject breakingObject;

	[ReadOnly]
	public bool isBroken;

	public UnityEvent onBreak;

	public UnityEvent onHit;

	public UnityEvent onRangedHit;

	public UnityEvent onNearbyHit;

	[Space]
	public bool isAttached;

	[ConditionalHide("isAttached", true)]
	public BreakableObject parentBreakable;

	[ConditionalHide("isAttached", true)]
	public bool breakParent;

	public BreakableObject[] childBreakables;

	[Space]
	public bool isSturdy;

	[ConditionalHide("isSturdy", true)]
	public int sturdyHits = 3;

	[ConditionalHide("isSturdy", true)]
	public GameObject crackedObject;

	public bool isInvincible;

	public bool isNonRanged;

	[ConditionalHide("isSturdy", true, ConditionalSourceField2 = "isInvincible", UseOrLogic = true)]
	public AudioSourceVariance bonkSound;

	public BoxCollider bonkBoxReference;

	[ConditionalHide("isPersistent", true, true)]
	public bool respawn;

	[HideInInspector]
	public bool waitForValidation;

	private float lastBreakTime = -1f;
}
