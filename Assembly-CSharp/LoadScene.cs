using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

public class LoadScene : MonoBehaviour
{
	// Token: 0x06000144 RID: 324 RVA: 0x00007BE4 File Offset: 0x00005DE4
	private void Start()
	{
		if (this.automatic)
		{
			this.DoLoadScene();
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00007BF4 File Offset: 0x00005DF4
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
