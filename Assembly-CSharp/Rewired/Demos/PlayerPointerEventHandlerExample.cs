using System;
using System.Collections.Generic;
using System.Text;
using Rewired.Integration.UnityUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000340 RID: 832
	[AddComponentMenu("")]
	public sealed class PlayerPointerEventHandlerExample : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IScrollHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x0600177A RID: 6010 RVA: 0x00063D46 File Offset: 0x00061F46
		private void Log(string o)
		{
			this.log.Add(o);
			if (this.log.Count > 10)
			{
				this.log.RemoveAt(0);
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00063D70 File Offset: 0x00061F70
		private void Update()
		{
			if (this.text != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this.log)
				{
					stringBuilder.AppendLine(text);
				}
				this.text.text = stringBuilder.ToString();
			}
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00063DEC File Offset: 0x00061FEC
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnPointerEnter:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00063E5C File Offset: 0x0006205C
		public void OnPointerExit(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnPointerExit:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00063ECC File Offset: 0x000620CC
		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnPointerUp:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00063F58 File Offset: 0x00062158
		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnPointerDown:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00063FE4 File Offset: 0x000621E4
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnPointerClick:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00064070 File Offset: 0x00062270
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnScroll:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000640E0 File Offset: 0x000622E0
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnBeginDrag:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0006416C File Offset: 0x0006236C
		public void OnDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnDrag:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000641F8 File Offset: 0x000623F8
		public void OnEndDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnEndDrag:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00064284 File Offset: 0x00062484
		private static string GetSourceName(PlayerPointerEventData playerEventData)
		{
			if (playerEventData.sourceType == PointerEventType.Mouse)
			{
				if (playerEventData.mouseSource is Behaviour)
				{
					return (playerEventData.mouseSource as Behaviour).name;
				}
			}
			else if (playerEventData.sourceType == PointerEventType.Touch && playerEventData.touchSource is Behaviour)
			{
				return (playerEventData.touchSource as Behaviour).name;
			}
			return null;
		}

		// Token: 0x04001955 RID: 6485
		public Text text;

		// Token: 0x04001956 RID: 6486
		private const int logLength = 10;

		// Token: 0x04001957 RID: 6487
		private List<string> log = new List<string>();
	}
}
