using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000133 RID: 307
public class BreakableObject : PersistentObject, IOnTimeout
{
	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000655 RID: 1621 RVA: 0x00020992 File Offset: 0x0001EB92
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

	// Token: 0x06000656 RID: 1622 RVA: 0x000209B5 File Offset: 0x0001EBB5
	public override void OnValidate()
	{
		base.OnValidate();
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x000209C0 File Offset: 0x0001EBC0
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
				this.brokenObject.transform.rotation = Quaternion.Euler(0f, Random.value * 360f, 0f);
			}
		}
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00020A53 File Offset: 0x0001EC53
	public virtual void OnEnable()
	{
		this.Load(this.isBroken);
		if (this.breakingObject != null)
		{
			this.breakingObject = null;
		}
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00020A78 File Offset: 0x0001EC78
	public virtual void OnDisable()
	{
		if (this.isBroken && this.breakingObject != null)
		{
			if (this.waitForValidation)
			{
				Object.Destroy(this.breakingObject);
			}
			this.breakingObject = null;
		}
		if (this.waitForValidation)
		{
			this.isBroken = false;
		}
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00020AC4 File Offset: 0x0001ECC4
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

	// Token: 0x0600065B RID: 1627 RVA: 0x00020B3C File Offset: 0x0001ED3C
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

	// Token: 0x0600065C RID: 1628 RVA: 0x00020BCA File Offset: 0x0001EDCA
	public virtual void Break()
	{
		this.Break(false, Vector3.zero, false);
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x00020BDC File Offset: 0x0001EDDC
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
				this.breakingObject = Object.Instantiate<GameObject>(this.breakingPrefab, base.transform.position, base.transform.rotation);
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
						componentsInChildren2[i].velocity += Random.Range(0.75f, 1f) * velocity;
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

	// Token: 0x0600065E RID: 1630 RVA: 0x00020E9C File Offset: 0x0001F09C
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
			this.brokenObject.transform.rotation = Quaternion.Euler(0f, Random.value * 360f, 0f);
		}
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x00020F34 File Offset: 0x0001F134
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

	// Token: 0x04000884 RID: 2180
	public GameObject intactObject;

	// Token: 0x04000885 RID: 2181
	public GameObject brokenObject;

	// Token: 0x04000886 RID: 2182
	public GameObject breakingPrefab;

	// Token: 0x04000887 RID: 2183
	private GameObject breakingObject;

	// Token: 0x04000888 RID: 2184
	[ReadOnly]
	public bool isBroken;

	// Token: 0x04000889 RID: 2185
	public UnityEvent onBreak;

	// Token: 0x0400088A RID: 2186
	public UnityEvent onHit;

	// Token: 0x0400088B RID: 2187
	public UnityEvent onRangedHit;

	// Token: 0x0400088C RID: 2188
	public UnityEvent onNearbyHit;

	// Token: 0x0400088D RID: 2189
	[Space]
	public bool isAttached;

	// Token: 0x0400088E RID: 2190
	[ConditionalHide("isAttached", true)]
	public BreakableObject parentBreakable;

	// Token: 0x0400088F RID: 2191
	[ConditionalHide("isAttached", true)]
	public bool breakParent;

	// Token: 0x04000890 RID: 2192
	public BreakableObject[] childBreakables;

	// Token: 0x04000891 RID: 2193
	[Space]
	public bool isSturdy;

	// Token: 0x04000892 RID: 2194
	[ConditionalHide("isSturdy", true)]
	public int sturdyHits = 3;

	// Token: 0x04000893 RID: 2195
	[ConditionalHide("isSturdy", true)]
	public GameObject crackedObject;

	// Token: 0x04000894 RID: 2196
	public bool isInvincible;

	// Token: 0x04000895 RID: 2197
	public bool isNonRanged;

	// Token: 0x04000896 RID: 2198
	[ConditionalHide("isSturdy", true, ConditionalSourceField2 = "isInvincible", UseOrLogic = true)]
	public AudioSourceVariance bonkSound;

	// Token: 0x04000897 RID: 2199
	public BoxCollider bonkBoxReference;

	// Token: 0x04000898 RID: 2200
	[ConditionalHide("isPersistent", true, true)]
	public bool respawn;

	// Token: 0x04000899 RID: 2201
	[HideInInspector]
	public bool waitForValidation;

	// Token: 0x0400089A RID: 2202
	private float lastBreakTime = -1f;
}
