using System;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonPrompt : MonoBehaviour
{
	// Token: 0x060011ED RID: 4589 RVA: 0x00059B3C File Offset: 0x00057D3C
	private void Awake()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		if (this.allowSkip)
		{
			this.waitUntilTriggered = new WaitUntil(() => this.triggered || DebugButtons.IsSkipHeld);
			return;
		}
		this.waitUntilTriggered = new WaitUntil(() => this.triggered);
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x0000F401 File Offset: 0x0000D601
	private void OnEnable()
	{
		this.triggered = false;
		this.startTime = Time.time;
		PlayerInteract.interactButtonPriority = base.gameObject;
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x000050F2 File Offset: 0x000032F2
	private void OnDisable()
	{
		if (PlayerInteract.interactButtonPriority == base.gameObject)
		{
			PlayerInteract.interactButtonPriority = null;
		}
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x00059B94 File Offset: 0x00057D94
	private void Update()
	{
		if (this.rePlayer != null && (this.rePlayer.GetButtonDown("Interact") || (Game.State == GameState.Dialogue && (this.rePlayer.GetButtonDown("Interact Dialogue") || this.rePlayer.GetButtonDown("UISubmit"))) || (PlayerInput.interactMapping == PlayerInput.InteractMapping.Jump && this.rePlayer.GetButtonDown("Jump")) || (PlayerInput.interactMapping == PlayerInput.InteractMapping.Primary && this.rePlayer.GetButtonDown("Primary"))))
		{
			this.triggered = true;
		}
	}

	public float timeLimit;

	private float startTime;

	public Image backgroundImage;

	public Sprite[] backgroundSprites;

	public WaitUntil waitUntilTriggered;

	public bool triggered;

	public bool allowAlternateInputs = true;

	public bool allowSkip = true;

	private global::Rewired.Player rePlayer;
}
