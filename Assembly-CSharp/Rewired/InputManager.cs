using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Rewired.Platforms;
using Rewired.Utils;
using Rewired.Utils.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rewired
{
	// Token: 0x0200030E RID: 782
	[AddComponentMenu("Rewired/Input Manager")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class InputManager : InputManager_Base
	{
		// Token: 0x0600123C RID: 4668 RVA: 0x0004E8AF File Offset: 0x0004CAAF
		protected override void OnInitialized()
		{
			this.SubscribeEvents();
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0004E8B7 File Offset: 0x0004CAB7
		protected override void OnDeinitialized()
		{
			this.UnsubscribeEvents();
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0004E8C0 File Offset: 0x0004CAC0
		protected override void DetectPlatform()
		{
			this.scriptingBackend = ScriptingBackend.Mono;
			this.scriptingAPILevel = ScriptingAPILevel.Net20;
			this.editorPlatform = EditorPlatform.None;
			this.platform = Platform.Unknown;
			this.webplayerPlatform = WebplayerPlatform.None;
			this.isEditor = false;
			if (SystemInfo.deviceName == null)
			{
				string empty = string.Empty;
			}
			if (SystemInfo.deviceModel == null)
			{
				string empty2 = string.Empty;
			}
			this.platform = Platform.Windows;
			this.scriptingBackend = ScriptingBackend.Mono;
			this.scriptingAPILevel = ScriptingAPILevel.NetStandard20;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0004E926 File Offset: 0x0004CB26
		protected override void CheckRecompile()
		{
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0004E928 File Offset: 0x0004CB28
		protected override IExternalTools GetExternalTools()
		{
			return new ExternalTools();
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0004E92F File Offset: 0x0004CB2F
		private bool CheckDeviceName(string searchPattern, string deviceName, string deviceModel)
		{
			return Regex.IsMatch(deviceName, searchPattern, RegexOptions.IgnoreCase) || Regex.IsMatch(deviceModel, searchPattern, RegexOptions.IgnoreCase);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0004E945 File Offset: 0x0004CB45
		private void SubscribeEvents()
		{
			this.UnsubscribeEvents();
			SceneManager.sceneLoaded += this.OnSceneLoaded;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0004E95E File Offset: 0x0004CB5E
		private void UnsubscribeEvents()
		{
			SceneManager.sceneLoaded -= this.OnSceneLoaded;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0004E971 File Offset: 0x0004CB71
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			base.OnSceneLoaded();
		}

		// Token: 0x04001713 RID: 5907
		private bool ignoreRecompile;
	}
}
