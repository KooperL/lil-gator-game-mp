using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200018E RID: 398
public class BreakableObject : PersistentObject, IOnTimeout
{
	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x0600077A RID: 1914 RVA: 0x00007811 File Offset: 0x00005A11
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

	// Token: 0x0600077B RID: 1915 RVA: 0x00007834 File Offset: 0x00005A34
	public override void OnValidate()
	{
		base.OnValidate();
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x00034204 File Offset: 0x00032404
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

	// Token: 0x0600077D RID: 1917 RVA: 0x0000783C File Offset: 0x00005A3C
	public virtual void OnEnable()
	{
		this.Load(this.isBroken);
		if (this.breakingObject != null)
		{
			this.breakingObject = null;
		}
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00034298 File Offset: 0x00032498
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

	// Token: 0x0600077F RID: 1919 RVA: 0x000342E4 File Offset: 0x000324E4
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

	// Token: 0x06000780 RID: 1920 RVA: 0x0003435C File Offset: 0x0003255C
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

	// Token: 0x06000781 RID: 1921 RVA: 0x0000785F File Offset: 0x00005A5F
	public virtual void Break()
	{
		this.Break(false, Vector3.zero, false);
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x000343EC File Offset: 0x000325EC
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

	// Token: 0x06000783 RID: 1923 RVA: 0x000346AC File Offset: 0x000328AC
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

	// Token: 0x06000784 RID: 1924 RVA: 0x00034744 File Offset: 0x00032944
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

	// Token: 0x040009EE RID: 2542
	public GameObject intactObject;

	// Token: 0x040009EF RID: 2543
	public GameObject brokenObject;

	// Token: 0x040009F0 RID: 2544
	public GameObject breakingPrefab;

	// Token: 0x040009F1 RID: 2545
	private GameObject breakingObject;

	// Token: 0x040009F2 RID: 2546
	[ReadOnly]
	public bool isBroken;

	// Token: 0x040009F3 RID: 2547
	public UnityEvent onBreak;

	// Token: 0x040009F4 RID: 2548
	public UnityEvent onHit;

	// Token: 0x040009F5 RID: 2549
	public UnityEvent onRangedHit;

	// Token: 0x040009F6 RID: 2550
	public UnityEvent onNearbyHit;

	// Token: 0x040009F7 RID: 2551
	[Space]
	public bool isAttached;

	// Token: 0x040009F8 RID: 2552
	[ConditionalHide("isAttached", true)]
	public BreakableObject parentBreakable;

	// Token: 0x040009F9 RID: 2553
	[ConditionalHide("isAttached", true)]
	public bool breakParent;

	// Token: 0x040009FA RID: 2554
	public BreakableObject[] childBreakables;

	// Token: 0x040009FB RID: 2555
	[Space]
	public bool isSturdy;

	// Token: 0x040009FC RID: 2556
	[ConditionalHide("isSturdy", true)]
	public int sturdyHits = 3;

	// Token: 0x040009FD RID: 2557
	[ConditionalHide("isSturdy", true)]
	public GameObject crackedObject;

	// Token: 0x040009FE RID: 2558
	public bool isInvincible;

	// Token: 0x040009FF RID: 2559
	public bool isNonRanged;

	// Token: 0x04000A00 RID: 2560
	[ConditionalHide("isSturdy", true, ConditionalSourceField2 = "isInvincible", UseOrLogic = true)]
	public AudioSourceVariance bonkSound;

	// Token: 0x04000A01 RID: 2561
	public BoxCollider bonkBoxReference;

	// Token: 0x04000A02 RID: 2562
	[ConditionalHide("isPersistent", true, true)]
	public bool respawn;

	// Token: 0x04000A03 RID: 2563
	[HideInInspector]
	public bool waitForValidation;

	// Token: 0x04000A04 RID: 2564
	private float lastBreakTime = -1f;
}
