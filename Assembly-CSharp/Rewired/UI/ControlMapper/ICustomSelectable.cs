using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000321 RID: 801
	public interface ICustomSelectable : ICancelHandler, IEventSystemHandler
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600158A RID: 5514
		// (set) Token: 0x0600158B RID: 5515
		Sprite disabledHighlightedSprite { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600158C RID: 5516
		// (set) Token: 0x0600158D RID: 5517
		Color disabledHighlightedColor { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600158E RID: 5518
		// (set) Token: 0x0600158F RID: 5519
		string disabledHighlightedTrigger { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001590 RID: 5520
		// (set) Token: 0x06001591 RID: 5521
		bool autoNavUp { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001592 RID: 5522
		// (set) Token: 0x06001593 RID: 5523
		bool autoNavDown { get; set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001594 RID: 5524
		// (set) Token: 0x06001595 RID: 5525
		bool autoNavLeft { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001596 RID: 5526
		// (set) Token: 0x06001597 RID: 5527
		bool autoNavRight { get; set; }

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06001598 RID: 5528
		// (remove) Token: 0x06001599 RID: 5529
		event UnityAction CancelEvent;
	}
}
