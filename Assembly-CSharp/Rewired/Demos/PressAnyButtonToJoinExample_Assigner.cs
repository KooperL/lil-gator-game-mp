using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x020004A1 RID: 1185
	[AddComponentMenu("")]
	public class PressAnyButtonToJoinExample_Assigner : MonoBehaviour
	{
		// Token: 0x06001D7B RID: 7547 RVA: 0x0001689C File Offset: 0x00014A9C
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.AssignJoysticksToPlayers();
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x00073878 File Offset: 0x00071A78
		private void AssignJoysticksToPlayers()
		{
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				Joystick joystick = joysticks[i];
				if (!ReInput.controllers.IsControllerAssigned(joystick.type, joystick.id) && joystick.GetAnyButtonDown())
				{
					Player player = this.FindPlayerWithoutJoystick();
					if (player == null)
					{
						return;
					}
					player.controllers.AddController(joystick, false);
				}
			}
			if (this.DoAllPlayersHaveJoysticks())
			{
				ReInput.configuration.autoAssignJoysticks = true;
				base.enabled = false;
			}
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000738FC File Offset: 0x00071AFC
		private Player FindPlayerWithoutJoystick()
		{
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i].controllers.joystickCount <= 0)
				{
					return players[i];
				}
			}
			return null;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000168AC File Offset: 0x00014AAC
		private bool DoAllPlayersHaveJoysticks()
		{
			return this.FindPlayerWithoutJoystick() == null;
		}
	}
}
