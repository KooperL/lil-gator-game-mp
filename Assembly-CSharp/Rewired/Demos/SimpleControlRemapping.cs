using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x020004AC RID: 1196
	[AddComponentMenu("")]
	public class SimpleControlRemapping : MonoBehaviour
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x00016A0A File Offset: 0x00014C0A
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(0);
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0007441C File Offset: 0x0007261C
		private ControllerMap controllerMap
		{
			get
			{
				if (this.controller == null)
				{
					return null;
				}
				return this.player.controllers.maps.GetMap(this.controller.type, this.controller.id, "Default", "Default");
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x00016AF8 File Offset: 0x00014CF8
		private Controller controller
		{
			get
			{
				return this.player.controllers.GetController(this.selectedControllerType, this.selectedControllerId);
			}
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x00074468 File Offset: 0x00072668
		private void OnEnable()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.inputMapper.options.timeout = 5f;
			this.inputMapper.options.ignoreMouseXAxis = true;
			this.inputMapper.options.ignoreMouseYAxis = true;
			ReInput.ControllerConnectedEvent += this.OnControllerChanged;
			ReInput.ControllerDisconnectedEvent += this.OnControllerChanged;
			this.inputMapper.InputMappedEvent += this.OnInputMapped;
			this.inputMapper.StoppedEvent += this.OnStopped;
			this.InitializeUI();
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00016B16 File Offset: 0x00014D16
		private void OnDisable()
		{
			this.inputMapper.Stop();
			this.inputMapper.RemoveAllEventListeners();
			ReInput.ControllerConnectedEvent -= this.OnControllerChanged;
			ReInput.ControllerDisconnectedEvent -= this.OnControllerChanged;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0007450C File Offset: 0x0007270C
		private void RedrawUI()
		{
			if (this.controller == null)
			{
				this.ClearUI();
				return;
			}
			this.controllerNameUIText.text = this.controller.name;
			for (int i = 0; i < this.rows.Count; i++)
			{
				SimpleControlRemapping.Row row = this.rows[i];
				InputAction action = this.rows[i].action;
				string text = string.Empty;
				int actionElementMapId = -1;
				foreach (ActionElementMap actionElementMap in this.controllerMap.ElementMapsWithAction(action.id))
				{
					if (actionElementMap.ShowInField(row.actionRange))
					{
						text = actionElementMap.elementIdentifierName;
						actionElementMapId = actionElementMap.id;
						break;
					}
				}
				row.text.text = text;
				row.button.onClick.RemoveAllListeners();
				int index = i;
				row.button.onClick.AddListener(delegate
				{
					this.OnInputFieldClicked(index, actionElementMapId);
				});
			}
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00074648 File Offset: 0x00072848
		private void ClearUI()
		{
			if (this.selectedControllerType == 2)
			{
				this.controllerNameUIText.text = "No joysticks attached";
			}
			else
			{
				this.controllerNameUIText.text = string.Empty;
			}
			for (int i = 0; i < this.rows.Count; i++)
			{
				this.rows[i].text.text = string.Empty;
			}
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x000746B4 File Offset: 0x000728B4
		private void InitializeUI()
		{
			foreach (object obj in this.actionGroupTransform)
			{
				Object.Destroy(((Transform)obj).gameObject);
			}
			foreach (object obj2 in this.fieldGroupTransform)
			{
				Object.Destroy(((Transform)obj2).gameObject);
			}
			foreach (InputAction inputAction in ReInput.mapping.ActionsInCategory("Default"))
			{
				if (inputAction.type == null)
				{
					this.CreateUIRow(inputAction, 0, inputAction.descriptiveName);
					this.CreateUIRow(inputAction, 1, (!string.IsNullOrEmpty(inputAction.positiveDescriptiveName)) ? inputAction.positiveDescriptiveName : (inputAction.descriptiveName + " +"));
					this.CreateUIRow(inputAction, 2, (!string.IsNullOrEmpty(inputAction.negativeDescriptiveName)) ? inputAction.negativeDescriptiveName : (inputAction.descriptiveName + " -"));
				}
				else if (inputAction.type == 1)
				{
					this.CreateUIRow(inputAction, 1, inputAction.descriptiveName);
				}
			}
			this.RedrawUI();
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0007482C File Offset: 0x00072A2C
		private void CreateUIRow(InputAction action, AxisRange actionRange, string label)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.textPrefab);
			gameObject.transform.SetParent(this.actionGroupTransform);
			gameObject.transform.SetAsLastSibling();
			gameObject.GetComponent<Text>().text = label;
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.buttonPrefab);
			gameObject2.transform.SetParent(this.fieldGroupTransform);
			gameObject2.transform.SetAsLastSibling();
			this.rows.Add(new SimpleControlRemapping.Row
			{
				action = action,
				actionRange = actionRange,
				button = gameObject2.GetComponent<Button>(),
				text = gameObject2.GetComponentInChildren<Text>()
			});
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000748CC File Offset: 0x00072ACC
		private void SetSelectedController(ControllerType controllerType)
		{
			bool flag = false;
			if (controllerType != this.selectedControllerType)
			{
				this.selectedControllerType = controllerType;
				flag = true;
			}
			int num = this.selectedControllerId;
			if (this.selectedControllerType == 2)
			{
				if (this.player.controllers.joystickCount > 0)
				{
					this.selectedControllerId = this.player.controllers.Joysticks[0].id;
				}
				else
				{
					this.selectedControllerId = -1;
				}
			}
			else
			{
				this.selectedControllerId = 0;
			}
			if (this.selectedControllerId != num)
			{
				flag = true;
			}
			if (flag)
			{
				this.inputMapper.Stop();
				this.RedrawUI();
			}
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00016B50 File Offset: 0x00014D50
		public void OnControllerSelected(int controllerType)
		{
			this.SetSelectedController(controllerType);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00016B59 File Offset: 0x00014D59
		private void OnInputFieldClicked(int index, int actionElementMapToReplaceId)
		{
			if (index < 0 || index >= this.rows.Count)
			{
				return;
			}
			if (this.controller == null)
			{
				return;
			}
			base.StartCoroutine(this.StartListeningDelayed(index, actionElementMapToReplaceId));
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00016B86 File Offset: 0x00014D86
		private IEnumerator StartListeningDelayed(int index, int actionElementMapToReplaceId)
		{
			yield return new WaitForSeconds(0.1f);
			this.inputMapper.Start(new InputMapper.Context
			{
				actionId = this.rows[index].action.id,
				controllerMap = this.controllerMap,
				actionRange = this.rows[index].actionRange,
				actionElementMapToReplace = this.controllerMap.GetElementMap(actionElementMapToReplaceId)
			});
			this.player.controllers.maps.SetMapsEnabled(false, "UI");
			this.statusUIText.text = "Listening...";
			yield break;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00016BA3 File Offset: 0x00014DA3
		private void OnControllerChanged(ControllerStatusChangedEventArgs args)
		{
			this.SetSelectedController(this.selectedControllerType);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00016BB1 File Offset: 0x00014DB1
		private void OnInputMapped(InputMapper.InputMappedEventData data)
		{
			this.RedrawUI();
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00016BB9 File Offset: 0x00014DB9
		private void OnStopped(InputMapper.StoppedEventData data)
		{
			this.statusUIText.text = string.Empty;
			this.player.controllers.maps.SetMapsEnabled(true, "UI");
		}

		// Token: 0x04001F04 RID: 7940
		private const string category = "Default";

		// Token: 0x04001F05 RID: 7941
		private const string layout = "Default";

		// Token: 0x04001F06 RID: 7942
		private const string uiCategory = "UI";

		// Token: 0x04001F07 RID: 7943
		private InputMapper inputMapper = new InputMapper();

		// Token: 0x04001F08 RID: 7944
		public GameObject buttonPrefab;

		// Token: 0x04001F09 RID: 7945
		public GameObject textPrefab;

		// Token: 0x04001F0A RID: 7946
		public RectTransform fieldGroupTransform;

		// Token: 0x04001F0B RID: 7947
		public RectTransform actionGroupTransform;

		// Token: 0x04001F0C RID: 7948
		public Text controllerNameUIText;

		// Token: 0x04001F0D RID: 7949
		public Text statusUIText;

		// Token: 0x04001F0E RID: 7950
		private ControllerType selectedControllerType;

		// Token: 0x04001F0F RID: 7951
		private int selectedControllerId;

		// Token: 0x04001F10 RID: 7952
		private List<SimpleControlRemapping.Row> rows = new List<SimpleControlRemapping.Row>();

		// Token: 0x020004AD RID: 1197
		private class Row
		{
			// Token: 0x04001F11 RID: 7953
			public InputAction action;

			// Token: 0x04001F12 RID: 7954
			public AxisRange actionRange;

			// Token: 0x04001F13 RID: 7955
			public Button button;

			// Token: 0x04001F14 RID: 7956
			public Text text;
		}
	}
}
