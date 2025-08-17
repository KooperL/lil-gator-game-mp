using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

public class LoadScene : MonoBehaviour
{
	// Token: 0x06000177 RID: 375 RVA: 0x00003425 File Offset: 0x00001625
	private void Start()
	{
		if (this.automatic)
		{
			this.DoLoadScene();
		}
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0001C928 File Offset: 0x0001AB28
	public void DoLoadScene()
	{
		if (this.triggerSave && Game.AllowedToSave)
		{
			GameData.g.WriteToDisk();
		}
		this.beforeLoad.Invoke();
		if (this.overrideLoadType)
		{
			if (this.isSceneAsset)
			{
				LoadSceneSequence.LoadScene(this.sceneToLoad, this.loadType);
				return;
			}
			LoadSceneSequence.LoadScene(this.preloadedSceneIndex, this.loadType);
			return;
		}
		else
		{
			if (this.isSceneAsset)
			{
				LoadSceneSequence.LoadScene(this.sceneToLoad, LoadSceneSequence.LoadType.LoadingScreen);
				return;
			}
			LoadSceneSequence.LoadScene(this.preloadedSceneIndex, LoadSceneSequence.LoadType.LoadingScreen);
			return;
		}
	}

	public bool triggerSave;

	public bool automatic;

	public bool isSceneAsset = true;

	public AssetReference sceneToLoad;

	public int preloadedSceneIndex = -1;

	public UnityEvent beforeLoad;

	public bool overrideLoadType;

	public LoadSceneSequence.LoadType loadType;
}
