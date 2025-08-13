using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000459 RID: 1113
	public interface ICustomSelectable : ICancelHandler, IEventSystemHandler
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001AAC RID: 6828
		// (set) Token: 0x06001AAD RID: 6829
		Sprite disabledHighlightedSprite { get; set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001AAE RID: 6830
		// (set) Token: 0x06001AAF RID: 6831
		Color disabledHighlightedColor { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001AB0 RID: 6832
		// (set) Token: 0x06001AB1 RID: 6833
		string disabledHighlightedTrigger { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001AB2 RID: 6834
		// (set) Token: 0x06001AB3 RID: 6835
		bool autoNavUp { get; set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001AB4 RID: 6836
		// (set) Token: 0x06001AB5 RID: 6837
		bool autoNavDown { get; set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001AB6 RID: 6838
		// (set) Token: 0x06001AB7 RID: 6839
		bool autoNavLeft { get; set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001AB8 RID: 6840
		// (set) Token: 0x06001AB9 RID: 6841
		bool autoNavRight { get; set; }

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06001ABA RID: 6842
		// (remove) Token: 0x06001ABB RID: 6843
		event UnityAction CancelEvent;
	}
}
