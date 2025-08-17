using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Rewired.Utils.Libraries.TinyJson;
using UnityEngine;

namespace Rewired.Data
{
	public class InputDataStore : UserDataStore
	{
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00011A7E File Offset: 0x0000FC7E
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x00011A86 File Offset: 0x0000FC86
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

		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00011A8F File Offset: 0x0000FC8F
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x00011A97 File Offset: 0x0000FC97
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

		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x00011AA8 File Offset: 0x0000FCA8
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

		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00011AB1 File Offset: 0x0000FCB1
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x00011AB9 File Offset: 0x0000FCB9
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

		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00011AC2 File Offset: 0x0000FCC2
		// (set) Token: 0x060016EF RID: 5871 RVA: 0x00011ACA File Offset: 0x0000FCCA
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

		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x00011AD3 File Offset: 0x0000FCD3
		// (set) Token: 0x060016F1 RID: 5873 RVA: 0x00011ADB File Offset: 0x0000FCDB
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

		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		private string playerPrefsKey_controllerAssignments
		{
			get
			{
				return string.Format("{0}_{1}", this.playerPrefsKeyPrefix, "ControllerAssignments");
			}
		}

		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00011AFB File Offset: 0x0000FCFB
		private bool loadControllerAssignments
		{
			get
			{
				return this.loadKeyboardAssignments || this.loadMouseAssignments || this.loadJoystickAssignments;
			}
		}

		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x000629B8 File Offset: 0x00060BB8
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

		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00062A10 File Offset: 0x00060C10
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

		// Token: 0x060016F6 RID: 5878 RVA: 0x00011B15 File Offset: 0x0000FD15
		public override void Save()
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not save any data.", this);
				return;
			}
			this.SaveAll();
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00011B31 File Offset: 0x0000FD31
		public override void SaveControllerData(int playerId, ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not save any data.", this);
				return;
			}
			this.SaveControllerDataNow(playerId, controllerType, controllerId);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00011B50 File Offset: 0x0000FD50
		public override void SaveControllerData(ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not save any data.", this);
				return;
			}
			this.SaveControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00011B6E File Offset: 0x0000FD6E
		public override void SavePlayerData(int playerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not save any data.", this);
				return;
			}
			this.SavePlayerDataNow(playerId);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00011B8B File Offset: 0x0000FD8B
		public override void SaveInputBehavior(int playerId, int behaviorId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not save any data.", this);
				return;
			}
			this.SaveInputBehaviorNow(playerId, behaviorId);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00011BA9 File Offset: 0x0000FDA9
		public override void Load()
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not load any data.", this);
				return;
			}
			this.LoadAll();
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00011BC6 File Offset: 0x0000FDC6
		public override void LoadControllerData(int playerId, ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not load any data.", this);
				return;
			}
			this.LoadControllerDataNow(playerId, controllerType, controllerId);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00011BE6 File Offset: 0x0000FDE6
		public override void LoadControllerData(ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not load any data.", this);
				return;
			}
			this.LoadControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00011C05 File Offset: 0x0000FE05
		public override void LoadPlayerData(int playerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not load any data.", this);
				return;
			}
			this.LoadPlayerDataNow(playerId);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00011C23 File Offset: 0x0000FE23
		public override void LoadInputBehavior(int playerId, int behaviorId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_Settings is disabled and will not load any data.", this);
				return;
			}
			this.LoadInputBehaviorNow(playerId, behaviorId);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00011C42 File Offset: 0x0000FE42
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

		// Token: 0x06001701 RID: 5889 RVA: 0x00062A80 File Offset: 0x00060C80
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

		// Token: 0x06001702 RID: 5890 RVA: 0x00011C75 File Offset: 0x0000FE75
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

		// Token: 0x06001703 RID: 5891 RVA: 0x00011C95 File Offset: 0x0000FE95
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

		// Token: 0x06001704 RID: 5892 RVA: 0x00062AF0 File Offset: 0x00060CF0
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

		// Token: 0x06001705 RID: 5893 RVA: 0x00062B1C File Offset: 0x00060D1C
		public override ControllerMap LoadControllerMap(int playerId, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return null;
			}
			return this.LoadControllerMap(player, controllerIdentifier, categoryId, layoutId);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00062B48 File Offset: 0x00060D48
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

		// Token: 0x06001707 RID: 5895 RVA: 0x00011CAF File Offset: 0x0000FEAF
		private int LoadPlayerDataNow(int playerId)
		{
			return this.LoadPlayerDataNow(ReInput.players.GetPlayer(playerId));
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00062BA4 File Offset: 0x00060DA4
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

		// Token: 0x06001709 RID: 5897 RVA: 0x00062C4C File Offset: 0x00060E4C
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

		// Token: 0x0600170A RID: 5898 RVA: 0x00011CC2 File Offset: 0x0000FEC2
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

		// Token: 0x0600170B RID: 5899 RVA: 0x00011CDB File Offset: 0x0000FEDB
		private int LoadJoystickCalibrationData(int joystickId)
		{
			return this.LoadJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00062C88 File Offset: 0x00060E88
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

		// Token: 0x0600170D RID: 5901 RVA: 0x00011CEE File Offset: 0x0000FEEE
		private int LoadControllerDataNow(int playerId, ControllerType controllerType, int controllerId)
		{
			int num = 0 + this.LoadControllerMaps(playerId, controllerType, controllerId);
			this.RefreshLayoutManager(playerId);
			return num + this.LoadControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00062CF4 File Offset: 0x00060EF4
		private int LoadControllerDataNow(ControllerType controllerType, int controllerId)
		{
			int num = 0;
			if (controllerType == 2)
			{
				num += this.LoadJoystickCalibrationData(controllerId);
			}
			return num;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00062D14 File Offset: 0x00060F14
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

		// Token: 0x06001710 RID: 5904 RVA: 0x00062DEC File Offset: 0x00060FEC
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

		// Token: 0x06001711 RID: 5905 RVA: 0x00062E40 File Offset: 0x00061040
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

		// Token: 0x06001712 RID: 5906 RVA: 0x00062E94 File Offset: 0x00061094
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

		// Token: 0x06001713 RID: 5907 RVA: 0x00062ECC File Offset: 0x000610CC
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

		// Token: 0x06001714 RID: 5908 RVA: 0x00062F10 File Offset: 0x00061110
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

		// Token: 0x06001715 RID: 5909 RVA: 0x00062F6C File Offset: 0x0006116C
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

		// Token: 0x06001716 RID: 5910 RVA: 0x00063034 File Offset: 0x00061234
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

		// Token: 0x06001717 RID: 5911 RVA: 0x000633A0 File Offset: 0x000615A0
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

		// Token: 0x06001718 RID: 5912 RVA: 0x00011D0B File Offset: 0x0000FF0B
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

		// Token: 0x06001719 RID: 5913 RVA: 0x00063410 File Offset: 0x00061610
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

		// Token: 0x0600171A RID: 5914 RVA: 0x00011D1A File Offset: 0x0000FF1A
		private void SavePlayerDataNow(int playerId)
		{
			this.SavePlayerDataNow(ReInput.players.GetPlayer(playerId));
			Settings.s.WriteToDisk();
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00063468 File Offset: 0x00061668
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

		// Token: 0x0600171C RID: 5916 RVA: 0x00063494 File Offset: 0x00061694
		private void SaveAllJoystickCalibrationData()
		{
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				this.SaveJoystickCalibrationData(joysticks[i]);
			}
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00011D37 File Offset: 0x0000FF37
		private void SaveJoystickCalibrationData(int joystickId)
		{
			this.SaveJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000634CC File Offset: 0x000616CC
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

		// Token: 0x0600171F RID: 5919 RVA: 0x00063504 File Offset: 0x00061704
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

		// Token: 0x06001720 RID: 5920 RVA: 0x00011D4A File Offset: 0x0000FF4A
		private void SaveControllerDataNow(int playerId, ControllerType controllerType, int controllerId)
		{
			this.SaveControllerMaps(playerId, controllerType, controllerId);
			this.SaveControllerDataNow(controllerType, controllerId);
			Settings.s.WriteToDisk();
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00011D67 File Offset: 0x0000FF67
		private void SaveControllerDataNow(ControllerType controllerType, int controllerId)
		{
			if (controllerType == 2)
			{
				this.SaveJoystickCalibrationData(controllerId);
			}
			Settings.s.WriteToDisk();
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0006355C File Offset: 0x0006175C
		private void SaveControllerMaps(Player player, PlayerSaveData playerSaveData)
		{
			foreach (ControllerMapSaveData controllerMapSaveData in playerSaveData.AllControllerMapSaveData)
			{
				this.SaveControllerMap(player, controllerMapSaveData.map);
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000635B0 File Offset: 0x000617B0
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

		// Token: 0x06001724 RID: 5924 RVA: 0x00063614 File Offset: 0x00061814
		private void SaveControllerMap(Player player, ControllerMap controllerMap)
		{
			string text = this.GetControllerMapPlayerPrefsKey(player, controllerMap.controller.identifier, controllerMap.categoryId, controllerMap.layoutId, 2);
			Settings.s.Write(text, controllerMap.ToXmlString());
			text = this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, controllerMap.controller.identifier, controllerMap.categoryId, controllerMap.layoutId, 2);
			Settings.s.Write(text, this.allActionIdsString);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00063684 File Offset: 0x00061884
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

		// Token: 0x06001726 RID: 5926 RVA: 0x000636B8 File Offset: 0x000618B8
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

		// Token: 0x06001727 RID: 5927 RVA: 0x000636F8 File Offset: 0x000618F8
		private void SaveInputBehaviorNow(Player player, InputBehavior inputBehavior)
		{
			if (player == null || inputBehavior == null)
			{
				return;
			}
			string inputBehaviorPlayerPrefsKey = this.GetInputBehaviorPlayerPrefsKey(player, inputBehavior.id);
			Settings.s.Write(inputBehaviorPlayerPrefsKey, inputBehavior.ToXmlString());
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0006372C File Offset: 0x0006192C
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

		// Token: 0x06001729 RID: 5929 RVA: 0x00011D7E File Offset: 0x0000FF7E
		private bool ControllerAssignmentSaveDataExists()
		{
			return Settings.s.HasString(this.playerPrefsKey_controllerAssignments) && !string.IsNullOrEmpty(Settings.s.ReadString(this.playerPrefsKey_controllerAssignments));
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00011DAE File Offset: 0x0000FFAE
		private string GetBasePlayerPrefsKey(Player player)
		{
			return this.playerPrefsKeyPrefix + "|playerName=" + player.name;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00011DC6 File Offset: 0x0000FFC6
		private string GetControllerMapPlayerPrefsKey(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId, int ppKeyVersion)
		{
			return this.GetBasePlayerPrefsKey(player) + "|dataType=ControllerMap" + InputDataStore.GetControllerMapPlayerPrefsKeyCommonSuffix(player, controllerIdentifier, categoryId, layoutId, ppKeyVersion);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00011DEA File Offset: 0x0000FFEA
		private string GetControllerMapKnownActionIdsPlayerPrefsKey(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId, int ppKeyVersion)
		{
			return this.GetBasePlayerPrefsKey(player) + "|dataType=ControllerMap_KnownActionIds" + InputDataStore.GetControllerMapPlayerPrefsKeyCommonSuffix(player, controllerIdentifier, categoryId, layoutId, ppKeyVersion);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00063874 File Offset: 0x00061A74
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

		// Token: 0x0600172E RID: 5934 RVA: 0x000639C4 File Offset: 0x00061BC4
		private string GetJoystickCalibrationMapPlayerPrefsKey(Joystick joystick)
		{
			return this.playerPrefsKeyPrefix + "|dataType=CalibrationMap" + "|controllerType=" + joystick.type.ToString() + "|hardwareIdentifier=" + joystick.hardwareIdentifier + "|hardwareGuid=" + joystick.hardwareTypeGuid.ToString();
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00011E0E File Offset: 0x0001000E
		private string GetInputBehaviorPlayerPrefsKey(Player player, int inputBehaviorId)
		{
			return this.GetBasePlayerPrefsKey(player) + "|dataType=InputBehavior" + "|id=" + inputBehaviorId.ToString();
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00063A30 File Offset: 0x00061C30
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

		// Token: 0x06001731 RID: 5937 RVA: 0x00063A70 File Offset: 0x00061C70
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

		// Token: 0x06001732 RID: 5938 RVA: 0x00063B1C File Offset: 0x00061D1C
		private string GetJoystickCalibrationMapXml(Joystick joystick)
		{
			string joystickCalibrationMapPlayerPrefsKey = this.GetJoystickCalibrationMapPlayerPrefsKey(joystick);
			if (!Settings.s.HasString(joystickCalibrationMapPlayerPrefsKey))
			{
				return string.Empty;
			}
			return Settings.s.ReadString(joystickCalibrationMapPlayerPrefsKey);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00063B50 File Offset: 0x00061D50
		private string GetInputBehaviorXml(Player player, int id)
		{
			string inputBehaviorPlayerPrefsKey = this.GetInputBehaviorPlayerPrefsKey(player, id);
			if (!Settings.s.HasString(inputBehaviorPlayerPrefsKey))
			{
				return string.Empty;
			}
			return Settings.s.ReadString(inputBehaviorPlayerPrefsKey);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00063B84 File Offset: 0x00061D84
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

		// Token: 0x06001735 RID: 5941 RVA: 0x00063CC0 File Offset: 0x00061EC0
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

		// Token: 0x06001736 RID: 5942 RVA: 0x00063D24 File Offset: 0x00061F24
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

		// Token: 0x06001737 RID: 5943 RVA: 0x00062724 File Offset: 0x00060924
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

		// Token: 0x06001738 RID: 5944 RVA: 0x000627F4 File Offset: 0x000609F4
		private void RefreshLayoutManager(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			player.controllers.maps.layoutManager.Apply();
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00062828 File Offset: 0x00060A28
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

		private const string thisScriptName = "UserDataStore_Settings";

		private const string logPrefix = "Rewired: ";

		private const string editorLoadedMessage = "\n***IMPORTANT:*** Changes made to the Rewired Input Manager configuration after the last time XML data was saved WILL NOT be used because the loaded old saved data has overwritten these values. If you change something in the Rewired Input Manager such as a Joystick Map or Input Behavior settings, you will not see these changes reflected in the current configuration. Clear PlayerPrefs using the inspector option on the UserDataStore_PlayerPrefs component.";

		private const string playerPrefsKeySuffix_controllerAssignments = "ControllerAssignments";

		private const int controllerMapPPKeyVersion_original = 0;

		private const int controllerMapPPKeyVersion_includeDuplicateJoystickIndex = 1;

		private const int controllerMapPPKeyVersion_supportDisconnectedControllers = 2;

		private const int controllerMapPPKeyVersion_includeFormatVersion = 2;

		private const int controllerMapPPKeyVersion = 2;

		[Tooltip("Should this script be used? If disabled, nothing will be saved or loaded.")]
		[SerializeField]
		private bool isEnabled = true;

		[Tooltip("Should saved data be loaded on start?")]
		[SerializeField]
		private bool loadDataOnStart = true;

		[Tooltip("Should Player Joystick assignments be saved and loaded? This is not totally reliable for all Joysticks on all platforms. Some platforms/input sources do not provide enough information to reliably save assignments from session to session and reboot to reboot.")]
		[SerializeField]
		private bool loadJoystickAssignments = true;

		[Tooltip("Should Player Keyboard assignments be saved and loaded?")]
		[SerializeField]
		private bool loadKeyboardAssignments = true;

		[Tooltip("Should Player Mouse assignments be saved and loaded?")]
		[SerializeField]
		private bool loadMouseAssignments = true;

		[Tooltip("The PlayerPrefs key prefix. Change this to change how keys are stored in PlayerPrefs. Changing this will make saved data already stored with the old key no longer accessible.")]
		[SerializeField]
		private string playerPrefsKeyPrefix = "RewiredSaveData";

		public int saveDataVersion;

		[NonSerialized]
		private bool allowImpreciseJoystickAssignmentMatching = true;

		[NonSerialized]
		private bool deferredJoystickAssignmentLoadPending;

		[NonSerialized]
		private bool wasJoystickEverDetected;

		[NonSerialized]
		private List<int> __allActionIds;

		[NonSerialized]
		private string __allActionIdsString;

		private class ControllerAssignmentSaveInfo
		{
			// (get) Token: 0x0600173B RID: 5947 RVA: 0x00011E6F File Offset: 0x0001006F
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

			// Token: 0x0600173C RID: 5948 RVA: 0x000022AD File Offset: 0x000004AD
			public ControllerAssignmentSaveInfo()
			{
			}

			// Token: 0x0600173D RID: 5949 RVA: 0x00063D9C File Offset: 0x00061F9C
			public ControllerAssignmentSaveInfo(int playerCount)
			{
				this.players = new InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo[playerCount];
				for (int i = 0; i < playerCount; i++)
				{
					this.players[i] = new InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo();
				}
			}

			// Token: 0x0600173E RID: 5950 RVA: 0x00063DD4 File Offset: 0x00061FD4
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

			// Token: 0x0600173F RID: 5951 RVA: 0x00011E83 File Offset: 0x00010083
			public bool ContainsPlayer(int playerId)
			{
				return this.IndexOfPlayer(playerId) >= 0;
			}

			public InputDataStore.ControllerAssignmentSaveInfo.PlayerInfo[] players;

			public class PlayerInfo
			{
				// (get) Token: 0x06001740 RID: 5952 RVA: 0x00011E92 File Offset: 0x00010092
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

				// Token: 0x06001741 RID: 5953 RVA: 0x00063E10 File Offset: 0x00062010
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

				// Token: 0x06001742 RID: 5954 RVA: 0x00011EA6 File Offset: 0x000100A6
				public bool ContainsJoystick(int joystickId)
				{
					return this.IndexOfJoystick(joystickId) >= 0;
				}

				public int id;

				public bool hasKeyboard;

				public bool hasMouse;

				public InputDataStore.ControllerAssignmentSaveInfo.JoystickInfo[] joysticks;
			}

			public class JoystickInfo
			{
				public Guid instanceGuid;

				public string hardwareIdentifier;

				public int id;
			}
		}

		private class JoystickAssignmentHistoryInfo
		{
			// Token: 0x06001745 RID: 5957 RVA: 0x00011EB5 File Offset: 0x000100B5
			public JoystickAssignmentHistoryInfo(Joystick joystick, int oldJoystickId)
			{
				if (joystick == null)
				{
					throw new ArgumentNullException("joystick");
				}
				this.joystick = joystick;
				this.oldJoystickId = oldJoystickId;
			}

			public readonly Joystick joystick;

			public readonly int oldJoystickId;
		}
	}
}
