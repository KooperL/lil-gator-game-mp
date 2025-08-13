using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Rewired.Utils.Libraries.TinyJson;
using UnityEngine;

namespace Rewired.Data
{
	// Token: 0x02000416 RID: 1046
	public class InputDataStore : UserDataStore
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00011681 File Offset: 0x0000F881
		// (set) Token: 0x06001687 RID: 5767 RVA: 0x00011689 File Offset: 0x0000F889
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.isEnabled = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00011692 File Offset: 0x0000F892
		// (set) Token: 0x06001689 RID: 5769 RVA: 0x0001169A File Offset: 0x0000F89A
		public bool LoadDataOnStart
		{
			get
			{
				return this.loadDataOnStart;
			}
			set
			{
				this.loadDataOnStart = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x000116A3 File Offset: 0x0000F8A3
		// (set) Token: 0x0600168B RID: 5771 RVA: 0x000116AB File Offset: 0x0000F8AB
		public bool LoadJoystickAssignments
		{
			get
			{
				return this.loadJoystickAssignments;
			}
			set
			{
				this.loadJoystickAssignments = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x000116B4 File Offset: 0x0000F8B4
		// (set) Token: 0x0600168D RID: 5773 RVA: 0x000116BC File Offset: 0x0000F8BC
		public bool LoadKeyboardAssignments
		{
			get
			{
				return this.loadKeyboardAssignments;
			}
			set
			{
				this.loadKeyboardAssignments = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x000116C5 File Offset: 0x0000F8C5
		// (set) Token: 0x0600168F RID: 5775 RVA: 0x000116CD File Offset: 0x0000F8CD
		public bool LoadMouseAssignments
		{
			get
			{
				return this.loadMouseAssignments;
			}
			set
			{
				this.loadMouseAssignments = value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x000116D6 File Offset: 0x0000F8D6
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x000116DE File Offset: 0x0000F8DE
		public string PlayerPrefsKeyPrefix
		{
			get
			{
				return this.playerPrefsKeyPrefix;
			}
			set
			{
				this.playerPrefsKeyPrefix = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x000116E7 File Offset: 0x0000F8E7
		private string playerPrefsKey_controllerAssignments
		{
			get
			{
				return string.Format("{0}_{1}", this.playerPrefsKeyPrefix, "ControllerAssignments");
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x000116FE File Offset: 0x0000F8FE
		private bool loadControllerAssignments
		{
			get
			{
				return this.loadKeyboardAssignments || this.loadMouseAssignments || this.loadJoystickAssignments;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x00060990 File Offset: 0x0005EB90
		private List<int> allActionIds
		{
			get
			{
				if (this.__allActionIds != null)
				{
					return this.__allActionIds;
				}
				List<int> list = new List<int>();
				IList<InputAction> actions = ReInput.mapping.Actions;
				for (int i = 0; i < actions.Count; i++)
				{
					list.Add(actions[i].id);
				}
				this.__allActionIds = list;
				return list;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x000609E8 File Offset: 0x0005EBE8
		private string allActionIdsString
		{
			get
			{
				if (!string.IsNullOrEmpty(this.__allActionIdsString))
				{
					return this.__allActionIdsString;
				}
				StringBuilder stringBuilder = new StringBuilder();
				List<int> allActionIds = this.allActionIds;
				for (int i = 0; i < allActionIds.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(allActionIds[i]);
				}
				this.__allActionIdsString = stringBuilder.ToString();
				return this.__allActionIdsString;
			}
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00011718 File Offset: 0x0000F918
		public override void Save()
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not save any data.", this);
				return;
			}
			this.SaveAll();
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00011734 File Offset: 0x0000F934
		public override void SaveControllerData(int playerId, ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not save any data.", this);
				return;
			}
			this.SaveControllerDataNow(playerId, controllerType, controllerId);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00011753 File Offset: 0x0000F953
		public override void SaveControllerData(ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not save any data.", this);
				return;
			}
			this.SaveControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00011771 File Offset: 0x0000F971
		public override void SavePlayerData(int playerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not save any data.", this);
				return;
			}
			this.SavePlayerDataNow(playerId);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0001178E File Offset: 0x0000F98E
		public override void SaveInputBehavior(int playerId, int behaviorId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not save any data.", this);
				return;
			}
			this.SaveInputBehaviorNow(playerId, behaviorId);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000117AC File Offset: 0x0000F9AC
		public override void Load()
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not load any data.", this);
				return;
			}
			this.LoadAll();
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x000117C9 File Offset: 0x0000F9C9
		public override void LoadControllerData(int playerId, ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not load any data.", this);
				return;
			}
			this.LoadControllerDataNow(playerId, controllerType, controllerId);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000117E9 File Offset: 0x0000F9E9
		public override void LoadControllerData(ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not load any data.", this);
				return;
			}
			this.LoadControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00011808 File Offset: 0x0000FA08
		public override void LoadPlayerData(int playerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not load any data.", this);
				return;
			}
			this.LoadPlayerDataNow(playerId);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00011826 File Offset: 0x0000FA26
		public override void LoadInputBehavior(int playerId, int behaviorId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not load any data.", this);
				return;
			}
			this.LoadInputBehaviorNow(playerId, behaviorId);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00011845 File Offset: 0x0000FA45
		protected override void OnInitialize()
		{
			if (this.loadDataOnStart)
			{
				this.Load();
				if (this.loadControllerAssignments && ReInput.controllers.joystickCount > 0)
				{
					this.wasJoystickEverDetected = true;
					this.SaveControllerAssignments();
				}
			}
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00060A58 File Offset: 0x0005EC58
		protected override void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (args.controllerType == 2)
			{
				this.LoadJoystickData(args.controllerId);
				if (this.loadDataOnStart && this.loadJoystickAssignments && !this.wasJoystickEverDetected)
				{
					base.StartCoroutine(this.LoadJoystickAssignmentsDeferred());
				}
				if (this.loadJoystickAssignments && !this.deferredJoystickAssignmentLoadPending)
				{
					this.SaveControllerAssignments();
				}
				this.wasJoystickEverDetected = true;
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00011878 File Offset: 0x0000FA78
		protected override void OnControllerPreDisconnect(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (args.controllerType == 2)
			{
				this.SaveJoystickData(args.controllerId);
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00011898 File Offset: 0x0000FA98
		protected override void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (this.loadControllerAssignments)
			{
				this.SaveControllerAssignments();
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00060AC8 File Offset: 0x0005ECC8
		public override void SaveControllerMap(int playerId, ControllerMap controllerMap)
		{
			if (controllerMap == null)
			{
				return;
			}
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			this.SaveControllerMap(player, controllerMap);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00060AF4 File Offset: 0x0005ECF4
		public override ControllerMap LoadControllerMap(int playerId, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return null;
			}
			return this.LoadControllerMap(player, controllerIdentifier, categoryId, layoutId);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00060B20 File Offset: 0x0005ED20
		private int LoadAll()
		{
			int num = 0;
			if (this.loadControllerAssignments && this.LoadControllerAssignmentsNow())
			{
				num++;
			}
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				num += this.LoadPlayerDataNow(allPlayers[i]);
			}
			return num + this.LoadAllJoystickCalibrationData();
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000118B2 File Offset: 0x0000FAB2
		private int LoadPlayerDataNow(int playerId)
		{
			return this.LoadPlayerDataNow(ReInput.players.GetPlayer(playerId));
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00060B7C File Offset: 0x0005ED7C
		private int LoadPlayerDataNow(Player player)
		{
			if (player == null)
			{
				return 0;
			}
			int num = 0;
			num += this.LoadInputBehaviors(player.id);
			num += this.LoadControllerMaps(player.id, 0, 0);
			num += this.LoadControllerMaps(player.id, 1, 0);
			foreach (Joystick joystick in player.controllers.Joysticks)
			{
				num += this.LoadControllerMaps(player.id, 2, joystick.id);
			}
			this.RefreshLayoutManager(player.id);
			return num;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00060C24 File Offset: 0x0005EE24
		private int LoadAllJoystickCalibrationData()
		{
			int num = 0;
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				num += this.LoadJoystickCalibrationData(joysticks[i]);
			}
			return num;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x000118C5 File Offset: 0x0000FAC5
		private int LoadJoystickCalibrationData(Joystick joystick)
		{
			if (joystick == null)
			{
				return 0;
			}
			if (!joystick.ImportCalibrationMapFromXmlString(this.GetJoystickCalibrationMapXml(joystick)))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000118DE File Offset: 0x0000FADE
		private int LoadJoystickCalibrationData(int joystickId)
		{
			return this.LoadJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00060C60 File Offset: 0x0005EE60
		private int LoadJoystickData(int joystickId)
		{
			int num = 0;
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (player.controllers.ContainsController(2, joystickId))
				{
					num += this.LoadControllerMaps(player.id, 2, joystickId);
					this.RefreshLayoutManager(player.id);
				}
			}
			return num + this.LoadJoystickCalibrationData(joystickId);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000118F1 File Offset: 0x0000FAF1
		private int LoadControllerDataNow(int playerId, ControllerType controllerType, int controllerId)
		{
			int num = 0 + this.LoadControllerMaps(playerId, controllerType, controllerId);
			this.RefreshLayoutManager(playerId);
			return num + this.LoadControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00060CCC File Offset: 0x0005EECC
		private int LoadControllerDataNow(ControllerType controllerType, int controllerId)
		{
			int num = 0;
			if (controllerType == 2)
			{
				num += this.LoadJoystickCalibrationData(controllerId);
			}
			return num;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00060CEC File Offset: 0x0005EEEC
		private int LoadControllerMaps(int playerId, ControllerType controllerType, int controllerId)
		{
			int num = 0;
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return num;
			}
			Controller controller = ReInput.controllers.GetController(controllerType, controllerId);
			if (controller == null)
			{
				return num;
			}
			IList<InputMapCategory> mapCategories = ReInput.mapping.MapCategories;
			for (int i = 0; i < mapCategories.Count; i++)
			{
				InputMapCategory inputMapCategory = mapCategories[i];
				if (inputMapCategory.userAssignable)
				{
					IList<InputLayout> list = ReInput.mapping.MapLayouts(controller.type);
					for (int j = 0; j < list.Count; j++)
					{
						InputLayout inputLayout = list[j];
						ControllerMap controllerMap = this.LoadControllerMap(player, controller.identifier, inputMapCategory.id, inputLayout.id);
						if (controllerMap != null)
						{
							player.controllers.maps.AddMap(controller, controllerMap);
							num++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00060DC4 File Offset: 0x0005EFC4
		private ControllerMap LoadControllerMap(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId)
		{
			if (player == null)
			{
				return null;
			}
			string controllerMapXml = this.GetControllerMapXml(player, controllerIdentifier, categoryId, layoutId);
			if (string.IsNullOrEmpty(controllerMapXml))
			{
				return null;
			}
			ControllerMap controllerMap = ControllerMap.CreateFromXml(controllerIdentifier.controllerType, controllerMapXml);
			if (controllerMap == null)
			{
				return null;
			}
			List<int> controllerMapKnownActionIds = this.GetControllerMapKnownActionIds(player, controllerIdentifier, categoryId, layoutId);
			this.AddDefaultMappingsForNewActions(controllerIdentifier, controllerMap, controllerMapKnownActionIds);
			return controllerMap;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00060E18 File Offset: 0x0005F018
		private int LoadInputBehaviors(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return 0;
			}
			int num = 0;
			IList<InputBehavior> inputBehaviors = ReInput.mapping.GetInputBehaviors(player.id);
			for (int i = 0; i < inputBehaviors.Count; i++)
			{
				num += this.LoadInputBehaviorNow(player, inputBehaviors[i]);
			}
			return num;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00060E6C File Offset: 0x0005F06C
		private int LoadInputBehaviorNow(int playerId, int behaviorId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return 0;
			}
			InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
			if (inputBehavior == null)
			{
				return 0;
			}
			return this.LoadInputBehaviorNow(player, inputBehavior);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00060EA4 File Offset: 0x0005F0A4
		private int LoadInputBehaviorNow(Player player, InputBehavior inputBehavior)
		{
			if (player == null || inputBehavior == null)
			{
				return 0;
			}
			string inputBehaviorXml = this.GetInputBehaviorXml(player, inputBehavior.id);
			if (inputBehaviorXml == null || inputBehaviorXml == string.Empty)
			{
				return 0;
			}
			if (!inputBehavior.ImportXmlString(inputBehaviorXml))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00060EE8 File Offset: 0x0005F0E8
		private bool LoadControllerAssignmentsNow()
		{
			try
			{
				InputDataStore.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo = this.LoadControllerAssignmentData();
				if (controllerAssignmentSaveInfo == null)
				{
					return false;
				}
				if (this.loadKeyboardAssignments || this.loadMouseAssignments)
				{
					this.LoadKeyboardAndMouseAssignmentsNow(controllerAssignmentSaveInfo);
				}
				if (this.loadJoystickAssignments)
				{
					this.LoadJoystickAssignmentsNow(controllerAssignmentSaveInfo);
				}
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00060F44 File Offset: 0x0005F144
		private bool LoadKeyboardAndMouseAssignmentsNow(InputDataStore.ControllerAssignmentSaveInfo data)
		{
			try
			{
				if (data == null && (data = this.LoadControllerAssignmentData()) == null)
				{
					return false;
				}
				foreach (Player player in ReInput.players.AllPlayers)
				{
					if (data.ContainsPlayer(player.id))
					{
						InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = data.players[data.IndexOfPlayer(player.id)];
						if (this.loadKeyboardAssignments)
						{
							player.controllers.hasKeyboard = playerInfo.hasKeyboard;
						}
						if (this.loadMouseAssignments)
						{
							player.controllers.hasMouse = playerInfo.hasMouse;
						}
					}
				}
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0006100C File Offset: 0x0005F20C
		private bool LoadJoystickAssignmentsNow(InputDataStore.ControllerAssignmentSaveInfo data)
		{
			try
			{
				if (ReInput.controllers.joystickCount == 0)
				{
					return false;
				}
				if (data == null && (data = this.LoadControllerAssignmentData()) == null)
				{
					return false;
				}
				foreach (Player player in ReInput.players.AllPlayers)
				{
					player.controllers.ClearControllersOfType(2);
				}
				List<InputDataStore.JoystickAssignmentHistoryInfo> list = (this.loadJoystickAssignments ? new List<InputDataStore.JoystickAssignmentHistoryInfo>() : null);
				foreach (Player player2 in ReInput.players.AllPlayers)
				{
					if (data.ContainsPlayer(player2.id))
					{
						InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = data.players[data.IndexOfPlayer(player2.id)];
						for (int i = 0; i < playerInfo.joystickCount; i++)
						{
							InputDataStore.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo2 = playerInfo.joysticks[i];
							if (joystickInfo2 != null)
							{
								Joystick joystick = this.FindJoystickPrecise(joystickInfo2);
								if (joystick != null)
								{
									if (list.Find((InputDataStore.JoystickAssignmentHistoryInfo x) => x.joystick == joystick) == null)
									{
										list.Add(new InputDataStore.JoystickAssignmentHistoryInfo(joystick, joystickInfo2.id));
									}
									player2.controllers.AddController(joystick, false);
								}
							}
						}
					}
				}
				if (this.allowImpreciseJoystickAssignmentMatching)
				{
					foreach (Player player3 in ReInput.players.AllPlayers)
					{
						if (data.ContainsPlayer(player3.id))
						{
							InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo playerInfo2 = data.players[data.IndexOfPlayer(player3.id)];
							for (int j = 0; j < playerInfo2.joystickCount; j++)
							{
								InputDataStore.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo = playerInfo2.joysticks[j];
								if (joystickInfo != null)
								{
									Joystick joystick2 = null;
									int num = list.FindIndex((InputDataStore.JoystickAssignmentHistoryInfo x) => x.oldJoystickId == joystickInfo.id);
									if (num >= 0)
									{
										joystick2 = list[num].joystick;
									}
									else
									{
										List<Joystick> list2;
										if (!this.TryFindJoysticksImprecise(joystickInfo, out list2))
										{
											goto IL_0298;
										}
										using (List<Joystick>.Enumerator enumerator2 = list2.GetEnumerator())
										{
											while (enumerator2.MoveNext())
											{
												Joystick match = enumerator2.Current;
												if (list.Find((InputDataStore.JoystickAssignmentHistoryInfo x) => x.joystick == match) == null)
												{
													joystick2 = match;
													break;
												}
											}
										}
										if (joystick2 == null)
										{
											goto IL_0298;
										}
										list.Add(new InputDataStore.JoystickAssignmentHistoryInfo(joystick2, joystickInfo.id));
									}
									player3.controllers.AddController(joystick2, false);
								}
								IL_0298:;
							}
						}
					}
				}
			}
			catch
			{
			}
			if (ReInput.configuration.autoAssignJoysticks)
			{
				ReInput.controllers.AutoAssignJoysticks();
			}
			return true;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00061378 File Offset: 0x0005F578
		private InputDataStore.ControllerAssignmentSaveInfo LoadControllerAssignmentData()
		{
			InputDataStore.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo;
			try
			{
				if (!Settings.s.HasString(this.playerPrefsKey_controllerAssignments))
				{
					controllerAssignmentSaveInfo = null;
				}
				else
				{
					string text = Settings.s.ReadString(this.playerPrefsKey_controllerAssignments);
					if (string.IsNullOrEmpty(text))
					{
						controllerAssignmentSaveInfo = null;
					}
					else
					{
						InputDataStore.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo2 = JsonParser.FromJson<InputDataStore.ControllerAssignmentSaveInfo>(text);
						if (controllerAssignmentSaveInfo2 == null || controllerAssignmentSaveInfo2.playerCount == 0)
						{
							controllerAssignmentSaveInfo = null;
						}
						else
						{
							controllerAssignmentSaveInfo = controllerAssignmentSaveInfo2;
						}
					}
				}
			}
			catch
			{
				controllerAssignmentSaveInfo = null;
			}
			return controllerAssignmentSaveInfo;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0001190E File Offset: 0x0000FB0E
		private IEnumerator LoadJoystickAssignmentsDeferred()
		{
			this.deferredJoystickAssignmentLoadPending = true;
			yield return new WaitForEndOfFrame();
			if (!ReInput.isReady)
			{
				yield break;
			}
			this.LoadJoystickAssignmentsNow(null);
			this.SaveControllerAssignments();
			this.deferredJoystickAssignmentLoadPending = false;
			yield break;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000613E8 File Offset: 0x0005F5E8
		private void SaveAll()
		{
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				this.SavePlayerDataNow(allPlayers[i]);
			}
			this.SaveAllJoystickCalibrationData();
			if (this.loadControllerAssignments)
			{
				this.SaveControllerAssignments();
			}
			Settings.s.WriteToDisk();
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0001191D File Offset: 0x0000FB1D
		private void SavePlayerDataNow(int playerId)
		{
			this.SavePlayerDataNow(ReInput.players.GetPlayer(playerId));
			Settings.s.WriteToDisk();
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00061440 File Offset: 0x0005F640
		private void SavePlayerDataNow(Player player)
		{
			if (player == null)
			{
				return;
			}
			PlayerSaveData saveData = player.GetSaveData(true);
			this.SaveInputBehaviors(player, saveData);
			this.SaveControllerMaps(player, saveData);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0006146C File Offset: 0x0005F66C
		private void SaveAllJoystickCalibrationData()
		{
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				this.SaveJoystickCalibrationData(joysticks[i]);
			}
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0001193A File Offset: 0x0000FB3A
		private void SaveJoystickCalibrationData(int joystickId)
		{
			this.SaveJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000614A4 File Offset: 0x0005F6A4
		private void SaveJoystickCalibrationData(Joystick joystick)
		{
			if (joystick == null)
			{
				return;
			}
			JoystickCalibrationMapSaveData calibrationMapSaveData = joystick.GetCalibrationMapSaveData();
			string joystickCalibrationMapPlayerPrefsKey = this.GetJoystickCalibrationMapPlayerPrefsKey(joystick);
			Settings.s.Write(joystickCalibrationMapPlayerPrefsKey, calibrationMapSaveData.map.ToXmlString());
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000614DC File Offset: 0x0005F6DC
		private void SaveJoystickData(int joystickId)
		{
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (player.controllers.ContainsController(2, joystickId))
				{
					this.SaveControllerMaps(player.id, 2, joystickId);
				}
			}
			this.SaveJoystickCalibrationData(joystickId);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0001194D File Offset: 0x0000FB4D
		private void SaveControllerDataNow(int playerId, ControllerType controllerType, int controllerId)
		{
			this.SaveControllerMaps(playerId, controllerType, controllerId);
			this.SaveControllerDataNow(controllerType, controllerId);
			Settings.s.WriteToDisk();
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0001196A File Offset: 0x0000FB6A
		private void SaveControllerDataNow(ControllerType controllerType, int controllerId)
		{
			if (controllerType == 2)
			{
				this.SaveJoystickCalibrationData(controllerId);
			}
			Settings.s.WriteToDisk();
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00061534 File Offset: 0x0005F734
		private void SaveControllerMaps(Player player, PlayerSaveData playerSaveData)
		{
			foreach (ControllerMapSaveData controllerMapSaveData in playerSaveData.AllControllerMapSaveData)
			{
				this.SaveControllerMap(player, controllerMapSaveData.map);
			}
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00061588 File Offset: 0x0005F788
		private void SaveControllerMaps(int playerId, ControllerType controllerType, int controllerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			if (!player.controllers.ContainsController(controllerType, controllerId))
			{
				return;
			}
			ControllerMapSaveData[] mapSaveData = player.controllers.maps.GetMapSaveData(controllerType, controllerId, true);
			if (mapSaveData == null)
			{
				return;
			}
			for (int i = 0; i < mapSaveData.Length; i++)
			{
				this.SaveControllerMap(player, mapSaveData[i].map);
			}
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000615EC File Offset: 0x0005F7EC
		private void SaveControllerMap(Player player, ControllerMap controllerMap)
		{
			string text = this.GetControllerMapPlayerPrefsKey(player, controllerMap.controller.identifier, controllerMap.categoryId, controllerMap.layoutId, 2);
			Settings.s.Write(text, controllerMap.ToXmlString());
			text = this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, controllerMap.controller.identifier, controllerMap.categoryId, controllerMap.layoutId, 2);
			Settings.s.Write(text, this.allActionIdsString);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0006165C File Offset: 0x0005F85C
		private void SaveInputBehaviors(Player player, PlayerSaveData playerSaveData)
		{
			if (player == null)
			{
				return;
			}
			InputBehavior[] inputBehaviors = playerSaveData.inputBehaviors;
			for (int i = 0; i < inputBehaviors.Length; i++)
			{
				this.SaveInputBehaviorNow(player, inputBehaviors[i]);
			}
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00061690 File Offset: 0x0005F890
		private void SaveInputBehaviorNow(int playerId, int behaviorId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
			if (inputBehavior == null)
			{
				return;
			}
			this.SaveInputBehaviorNow(player, inputBehavior);
			Settings.s.WriteToDisk();
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000616D0 File Offset: 0x0005F8D0
		private void SaveInputBehaviorNow(Player player, InputBehavior inputBehavior)
		{
			if (player == null || inputBehavior == null)
			{
				return;
			}
			string inputBehaviorPlayerPrefsKey = this.GetInputBehaviorPlayerPrefsKey(player, inputBehavior.id);
			Settings.s.Write(inputBehaviorPlayerPrefsKey, inputBehavior.ToXmlString());
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00061704 File Offset: 0x0005F904
		private bool SaveControllerAssignments()
		{
			try
			{
				InputDataStore.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo = new InputDataStore.ControllerAssignmentSaveInfo(ReInput.players.allPlayerCount);
				for (int i = 0; i < ReInput.players.allPlayerCount; i++)
				{
					Player player = ReInput.players.AllPlayers[i];
					InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = new InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo();
					controllerAssignmentSaveInfo.players[i] = playerInfo;
					playerInfo.id = player.id;
					playerInfo.hasKeyboard = player.controllers.hasKeyboard;
					playerInfo.hasMouse = player.controllers.hasMouse;
					InputDataStore.ControllerAssignmentSaveInfo.JoystickInfo[] array = new InputDataStore.ControllerAssignmentSaveInfo.JoystickInfo[player.controllers.joystickCount];
					playerInfo.joysticks = array;
					for (int j = 0; j < player.controllers.joystickCount; j++)
					{
						Joystick joystick = player.controllers.Joysticks[j];
						array[j] = new InputDataStore.ControllerAssignmentSaveInfo.JoystickInfo
						{
							instanceGuid = joystick.deviceInstanceGuid,
							id = joystick.id,
							hardwareIdentifier = joystick.hardwareIdentifier
						};
					}
				}
				Settings.s.Write(this.playerPrefsKey_controllerAssignments, JsonWriter.ToJson(controllerAssignmentSaveInfo));
				Settings.s.WriteToDisk();
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00011981 File Offset: 0x0000FB81
		private bool ControllerAssignmentSaveDataExists()
		{
			return Settings.s.HasString(this.playerPrefsKey_controllerAssignments) && !string.IsNullOrEmpty(Settings.s.ReadString(this.playerPrefsKey_controllerAssignments));
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x000119B1 File Offset: 0x0000FBB1
		private string GetBasePlayerPrefsKey(Player player)
		{
			return this.playerPrefsKeyPrefix + "|playerName=" + player.name;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000119C9 File Offset: 0x0000FBC9
		private string GetControllerMapPlayerPrefsKey(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId, int ppKeyVersion)
		{
			return this.GetBasePlayerPrefsKey(player) + "|dataType=ControllerMap" + InputDataStore.GetControllerMapPlayerPrefsKeyCommonSuffix(player, controllerIdentifier, categoryId, layoutId, ppKeyVersion);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x000119ED File Offset: 0x0000FBED
		private string GetControllerMapKnownActionIdsPlayerPrefsKey(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId, int ppKeyVersion)
		{
			return this.GetBasePlayerPrefsKey(player) + "|dataType=ControllerMap_KnownActionIds" + InputDataStore.GetControllerMapPlayerPrefsKeyCommonSuffix(player, controllerIdentifier, categoryId, layoutId, ppKeyVersion);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0006184C File Offset: 0x0005FA4C
		private static string GetControllerMapPlayerPrefsKeyCommonSuffix(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId, int ppKeyVersion)
		{
			string text = "";
			if (ppKeyVersion >= 2)
			{
				text = text + "|kv=" + ppKeyVersion.ToString();
			}
			text = text + "|controllerMapType=" + InputDataStore.GetControllerMapType(controllerIdentifier.controllerType).Name;
			text = string.Concat(new string[]
			{
				text,
				"|categoryId=",
				categoryId.ToString(),
				"|layoutId=",
				layoutId.ToString()
			});
			if (ppKeyVersion >= 2)
			{
				text = text + "|hardwareGuid=" + controllerIdentifier.hardwareTypeGuid.ToString();
				if (controllerIdentifier.hardwareTypeGuid == Guid.Empty)
				{
					text = text + "|hardwareIdentifier=" + controllerIdentifier.hardwareIdentifier;
				}
				if (controllerIdentifier.controllerType == 2)
				{
					text = text + "|duplicate=" + InputDataStore.GetDuplicateIndex(player, controllerIdentifier).ToString();
				}
			}
			else
			{
				text = text + "|hardwareIdentifier=" + controllerIdentifier.hardwareIdentifier;
				if (controllerIdentifier.controllerType == 2)
				{
					text = text + "|hardwareGuid=" + controllerIdentifier.hardwareTypeGuid.ToString();
					if (ppKeyVersion >= 1)
					{
						text = text + "|duplicate=" + InputDataStore.GetDuplicateIndex(player, controllerIdentifier).ToString();
					}
				}
			}
			return text;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0006199C File Offset: 0x0005FB9C
		private string GetJoystickCalibrationMapPlayerPrefsKey(Joystick joystick)
		{
			return this.playerPrefsKeyPrefix + "|dataType=CalibrationMap" + "|controllerType=" + joystick.type.ToString() + "|hardwareIdentifier=" + joystick.hardwareIdentifier + "|hardwareGuid=" + joystick.hardwareTypeGuid.ToString();
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00011A11 File Offset: 0x0000FC11
		private string GetInputBehaviorPlayerPrefsKey(Player player, int inputBehaviorId)
		{
			return this.GetBasePlayerPrefsKey(player) + "|dataType=InputBehavior" + "|id=" + inputBehaviorId.ToString();
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00061A08 File Offset: 0x0005FC08
		private string GetControllerMapXml(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId)
		{
			for (int i = 2; i >= 0; i--)
			{
				string controllerMapPlayerPrefsKey = this.GetControllerMapPlayerPrefsKey(player, controllerIdentifier, categoryId, layoutId, i);
				if (Settings.s.HasString(controllerMapPlayerPrefsKey))
				{
					return Settings.s.ReadString(controllerMapPlayerPrefsKey);
				}
			}
			return null;
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00061A48 File Offset: 0x0005FC48
		private List<int> GetControllerMapKnownActionIds(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId)
		{
			List<int> list = new List<int>();
			string text = null;
			bool flag = false;
			for (int i = 2; i >= 0; i--)
			{
				text = this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, controllerIdentifier, categoryId, layoutId, i);
				if (Settings.s.HasString(text))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return list;
			}
			string text2 = Settings.s.ReadString(text);
			if (string.IsNullOrEmpty(text2))
			{
				return list;
			}
			string[] array = text2.Split(new char[] { ',' });
			for (int j = 0; j < array.Length; j++)
			{
				int num;
				if (!string.IsNullOrEmpty(array[j]) && int.TryParse(array[j], out num))
				{
					list.Add(num);
				}
			}
			return list;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00061AF4 File Offset: 0x0005FCF4
		private string GetJoystickCalibrationMapXml(Joystick joystick)
		{
			string joystickCalibrationMapPlayerPrefsKey = this.GetJoystickCalibrationMapPlayerPrefsKey(joystick);
			if (!Settings.s.HasString(joystickCalibrationMapPlayerPrefsKey))
			{
				return string.Empty;
			}
			return Settings.s.ReadString(joystickCalibrationMapPlayerPrefsKey);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00061B28 File Offset: 0x0005FD28
		private string GetInputBehaviorXml(Player player, int id)
		{
			string inputBehaviorPlayerPrefsKey = this.GetInputBehaviorPlayerPrefsKey(player, id);
			if (!Settings.s.HasString(inputBehaviorPlayerPrefsKey))
			{
				return string.Empty;
			}
			return Settings.s.ReadString(inputBehaviorPlayerPrefsKey);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00061B5C File Offset: 0x0005FD5C
		private void AddDefaultMappingsForNewActions(ControllerIdentifier controllerIdentifier, ControllerMap controllerMap, List<int> knownActionIds)
		{
			if (controllerMap == null || knownActionIds == null)
			{
				return;
			}
			if (knownActionIds == null || knownActionIds.Count == 0)
			{
				return;
			}
			ControllerMap controllerMapInstance = ReInput.mapping.GetControllerMapInstance(controllerIdentifier, controllerMap.categoryId, controllerMap.layoutId);
			if (controllerMapInstance == null)
			{
				return;
			}
			List<int> list = new List<int>();
			foreach (int num in this.allActionIds)
			{
				if (!knownActionIds.Contains(num))
				{
					list.Add(num);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			foreach (ActionElementMap actionElementMap in controllerMapInstance.AllMaps)
			{
				if (list.Contains(actionElementMap.actionId) && !controllerMap.DoesElementAssignmentConflict(actionElementMap))
				{
					ElementAssignment elementAssignment;
					elementAssignment..ctor(controllerMap.controllerType, actionElementMap.elementType, actionElementMap.elementIdentifierId, actionElementMap.axisRange, actionElementMap.keyCode, actionElementMap.modifierKeyFlags, actionElementMap.actionId, actionElementMap.axisContribution, actionElementMap.invert);
					controllerMap.CreateElementMap(elementAssignment);
				}
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00061C98 File Offset: 0x0005FE98
		private Joystick FindJoystickPrecise(InputDataStore.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo)
		{
			if (joystickInfo == null)
			{
				return null;
			}
			if (joystickInfo.instanceGuid == Guid.Empty)
			{
				return null;
			}
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				if (joysticks[i].deviceInstanceGuid == joystickInfo.instanceGuid)
				{
					return joysticks[i];
				}
			}
			return null;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00061CFC File Offset: 0x0005FEFC
		private bool TryFindJoysticksImprecise(InputDataStore.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo, out List<Joystick> matches)
		{
			matches = null;
			if (joystickInfo == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(joystickInfo.hardwareIdentifier))
			{
				return false;
			}
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				if (string.Equals(joysticks[i].hardwareIdentifier, joystickInfo.hardwareIdentifier, StringComparison.OrdinalIgnoreCase))
				{
					if (matches == null)
					{
						matches = new List<Joystick>();
					}
					matches.Add(joysticks[i]);
				}
			}
			return matches != null;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000606FC File Offset: 0x0005E8FC
		private static int GetDuplicateIndex(Player player, ControllerIdentifier controllerIdentifier)
		{
			Controller controller = ReInput.controllers.GetController(controllerIdentifier);
			if (controller == null)
			{
				return 0;
			}
			int num = 0;
			foreach (Controller controller2 in player.controllers.Controllers)
			{
				if (controller2.type == controller.type)
				{
					bool flag = false;
					if (controller.type == 2)
					{
						if ((controller2 as Joystick).hardwareTypeGuid != controller.hardwareTypeGuid)
						{
							continue;
						}
						if (controller.hardwareTypeGuid != Guid.Empty)
						{
							flag = true;
						}
					}
					if (flag || !(controller2.hardwareIdentifier != controller.hardwareIdentifier))
					{
						if (controller2 == controller)
						{
							return num;
						}
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000607CC File Offset: 0x0005E9CC
		private void RefreshLayoutManager(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			player.controllers.maps.layoutManager.Apply();
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00060800 File Offset: 0x0005EA00
		private static Type GetControllerMapType(ControllerType controllerType)
		{
			switch (controllerType)
			{
			case 0:
				return typeof(KeyboardMap);
			case 1:
				return typeof(MouseMap);
			case 2:
				return typeof(JoystickMap);
			default:
				if (controllerType == 20)
				{
					return typeof(CustomControllerMap);
				}
				Debug.LogWarning("Rewired: Unknown ControllerType " + controllerType.ToString());
				return null;
			}
		}

		// Token: 0x04001B22 RID: 6946
		private const string thisScriptName = "UserDataStore_Settings";

		// Token: 0x04001B23 RID: 6947
		private const string logPrefix = "Rewired: ";

		// Token: 0x04001B24 RID: 6948
		private const string editorLoadedMessage = "\n***IMPORTANT:*** Changes made to the Rewired Input Manager configuration after the last time XML data was saved WILL NOT be used because the loaded old saved data has overwritten these values. If you change something in the Rewired Input Manager such as a Joystick Map or Input Behavior settings, you will not see these changes reflected in the current configuration. Clear PlayerPrefs using the inspector option on the UserDataStore_PlayerPrefs component.";

		// Token: 0x04001B25 RID: 6949
		private const string playerPrefsKeySuffix_controllerAssignments = "ControllerAssignments";

		// Token: 0x04001B26 RID: 6950
		private const int controllerMapPPKeyVersion_original = 0;

		// Token: 0x04001B27 RID: 6951
		private const int controllerMapPPKeyVersion_includeDuplicateJoystickIndex = 1;

		// Token: 0x04001B28 RID: 6952
		private const int controllerMapPPKeyVersion_supportDisconnectedControllers = 2;

		// Token: 0x04001B29 RID: 6953
		private const int controllerMapPPKeyVersion_includeFormatVersion = 2;

		// Token: 0x04001B2A RID: 6954
		private const int controllerMapPPKeyVersion = 2;

		// Token: 0x04001B2B RID: 6955
		[Tooltip("Should this script be used? If disabled, nothing will be saved or loaded.")]
		[SerializeField]
		private bool isEnabled = true;

		// Token: 0x04001B2C RID: 6956
		[Tooltip("Should saved data be loaded on start?")]
		[SerializeField]
		private bool loadDataOnStart = true;

		// Token: 0x04001B2D RID: 6957
		[Tooltip("Should Player Joystick assignments be saved and loaded? This is not totally reliable for all Joysticks on all platforms. Some platforms/input sources do not provide enough information to reliably save assignments from session to session and reboot to reboot.")]
		[SerializeField]
		private bool loadJoystickAssignments = true;

		// Token: 0x04001B2E RID: 6958
		[Tooltip("Should Player Keyboard assignments be saved and loaded?")]
		[SerializeField]
		private bool loadKeyboardAssignments = true;

		// Token: 0x04001B2F RID: 6959
		[Tooltip("Should Player Mouse assignments be saved and loaded?")]
		[SerializeField]
		private bool loadMouseAssignments = true;

		// Token: 0x04001B30 RID: 6960
		[Tooltip("The PlayerPrefs key prefix. Change this to change how keys are stored in PlayerPrefs. Changing this will make saved data already stored with the old key no longer accessible.")]
		[SerializeField]
		private string playerPrefsKeyPrefix = "RewiredSaveData";

		// Token: 0x04001B31 RID: 6961
		public int saveDataVersion;

		// Token: 0x04001B32 RID: 6962
		[NonSerialized]
		private bool allowImpreciseJoystickAssignmentMatching = true;

		// Token: 0x04001B33 RID: 6963
		[NonSerialized]
		private bool deferredJoystickAssignmentLoadPending;

		// Token: 0x04001B34 RID: 6964
		[NonSerialized]
		private bool wasJoystickEverDetected;

		// Token: 0x04001B35 RID: 6965
		[NonSerialized]
		private List<int> __allActionIds;

		// Token: 0x04001B36 RID: 6966
		[NonSerialized]
		private string __allActionIdsString;

		// Token: 0x02000417 RID: 1047
		private class ControllerAssignmentSaveInfo
		{
			// Token: 0x17000425 RID: 1061
			// (get) Token: 0x060016DB RID: 5851 RVA: 0x00011A72 File Offset: 0x0000FC72
			public int playerCount
			{
				get
				{
					if (this.players == null)
					{
						return 0;
					}
					return this.players.Length;
				}
			}

			// Token: 0x060016DC RID: 5852 RVA: 0x0000227F File Offset: 0x0000047F
			public ControllerAssignmentSaveInfo()
			{
			}

			// Token: 0x060016DD RID: 5853 RVA: 0x00061D74 File Offset: 0x0005FF74
			public ControllerAssignmentSaveInfo(int playerCount)
			{
				this.players = new InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo[playerCount];
				for (int i = 0; i < playerCount; i++)
				{
					this.players[i] = new InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo();
				}
			}

			// Token: 0x060016DE RID: 5854 RVA: 0x00061DAC File Offset: 0x0005FFAC
			public int IndexOfPlayer(int playerId)
			{
				for (int i = 0; i < this.playerCount; i++)
				{
					if (this.players[i] != null && this.players[i].id == playerId)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060016DF RID: 5855 RVA: 0x00011A86 File Offset: 0x0000FC86
			public bool ContainsPlayer(int playerId)
			{
				return this.IndexOfPlayer(playerId) >= 0;
			}

			// Token: 0x04001B37 RID: 6967
			public InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo[] players;

			// Token: 0x02000418 RID: 1048
			public class PlayerInfo
			{
				// Token: 0x17000426 RID: 1062
				// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00011A95 File Offset: 0x0000FC95
				public int joystickCount
				{
					get
					{
						if (this.joysticks == null)
						{
							return 0;
						}
						return this.joysticks.Length;
					}
				}

				// Token: 0x060016E1 RID: 5857 RVA: 0x00061DE8 File Offset: 0x0005FFE8
				public int IndexOfJoystick(int joystickId)
				{
					for (int i = 0; i < this.joystickCount; i++)
					{
						if (this.joysticks[i] != null && this.joysticks[i].id == joystickId)
						{
							return i;
						}
					}
					return -1;
				}

				// Token: 0x060016E2 RID: 5858 RVA: 0x00011AA9 File Offset: 0x0000FCA9
				public bool ContainsJoystick(int joystickId)
				{
					return this.IndexOfJoystick(joystickId) >= 0;
				}

				// Token: 0x04001B38 RID: 6968
				public int id;

				// Token: 0x04001B39 RID: 6969
				public bool hasKeyboard;

				// Token: 0x04001B3A RID: 6970
				public bool hasMouse;

				// Token: 0x04001B3B RID: 6971
				public InputDataStore.ControllerAssignmentSaveInfo.JoystickInfo[] joysticks;
			}

			// Token: 0x02000419 RID: 1049
			public class JoystickInfo
			{
				// Token: 0x04001B3C RID: 6972
				public Guid instanceGuid;

				// Token: 0x04001B3D RID: 6973
				public string hardwareIdentifier;

				// Token: 0x04001B3E RID: 6974
				public int id;
			}
		}

		// Token: 0x0200041A RID: 1050
		private class JoystickAssignmentHistoryInfo
		{
			// Token: 0x060016E5 RID: 5861 RVA: 0x00011AB8 File Offset: 0x0000FCB8
			public JoystickAssignmentHistoryInfo(Joystick joystick, int oldJoystickId)
			{
				if (joystick == null)
				{
					throw new ArgumentNullException("joystick");
				}
				this.joystick = joystick;
				this.oldJoystickId = oldJoystickId;
			}

			// Token: 0x04001B3F RID: 6975
			public readonly Joystick joystick;

			// Token: 0x04001B40 RID: 6976
			public readonly int oldJoystickId;
		}
	}
}
