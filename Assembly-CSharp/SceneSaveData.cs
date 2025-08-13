using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000232 RID: 562
public class SceneSaveData : MonoBehaviour
{
	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0003ABB5 File Offset: 0x00038DB5
	// (set) Token: 0x06000C2D RID: 3117 RVA: 0x0003ABD3 File Offset: 0x00038DD3
	public static SceneSaveData s
	{
		get
		{
			if (SceneSaveData.instance == null)
			{
				SceneSaveData.instance = Object.FindObjectOfType<SceneSaveData>();
			}
			return SceneSaveData.instance;
		}
		set
		{
			SceneSaveData.instance = value;
		}
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x0003ABDB File Offset: 0x00038DDB
	private void OnEnable()
	{
		SceneSaveData.s = this;
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x0003ABE3 File Offset: 0x00038DE3
	private void OnDisable()
	{
		if (SceneSaveData.s == this)
		{
			SceneSaveData.s = null;
		}
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x0003ABF8 File Offset: 0x00038DF8
	private void Start()
	{
		this.AssignIDs();
		this.Load();
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0003AC08 File Offset: 0x00038E08
	private void Load()
	{
		if (GameData.g.gameSaveData == null || this.isLoaded)
		{
			return;
		}
		this.objectStates = GameData.g.gameSaveData.objectStates;
		if (this.objectStates.Length != this.persistentObjects.Length)
		{
			bool[] array = new bool[this.persistentObjects.Length];
			Array.ConstrainedCopy(this.objectStates, 0, array, 0, Mathf.Min(this.objectStates.Length, array.Length));
			this.objectStates = (GameData.g.gameSaveData.objectStates = array);
		}
		this.LoadPersistentObjects();
		this.isLoaded = true;
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x0003ACA4 File Offset: 0x00038EA4
	public void LoadPersistentObjects()
	{
		for (int i = 0; i < this.persistentObjects.Length; i++)
		{
			if (this.persistentObjects[i] != null)
			{
				if (this.objectStates.Length > i)
				{
					this.persistentObjects[i].Load(this.objectStates[i]);
					this.persistentObjects[i].isLoaded = true;
				}
				else
				{
					this.persistentObjects[i].Load(false);
				}
			}
		}
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x0003AD14 File Offset: 0x00038F14
	public void SavePersistentObject(int id, bool state)
	{
		if (this.objectStates.Length <= id)
		{
			bool[] array = new bool[id + 1];
			this.objectStates.CopyTo(array, 0);
			this.objectStates = (GameData.g.gameSaveData.objectStates = array);
		}
		this.objectStates[id] = state;
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x0003AD64 File Offset: 0x00038F64
	public bool GetPersistentObject(int id)
	{
		if (!this.isLoaded)
		{
			this.Load();
		}
		return this.objectStates != null && this.objectStates.Length > id && this.objectStates[id];
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0003AD94 File Offset: 0x00038F94
	public bool HasPersistentObject(PersistentObject o)
	{
		if (this.persistentObjects != null)
		{
			for (int i = 0; i < this.persistentObjects.Length; i++)
			{
				if (this.persistentObjects[i] == o)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x0003ADD0 File Offset: 0x00038FD0
	public void RegisterPersistentObject(PersistentObject o)
	{
		if (Application.isPlaying)
		{
			Debug.LogError("Do not invoke in play mode!");
			return;
		}
		if (this.HasPersistentObject(o))
		{
			return;
		}
		if (this.persistentObjects == null)
		{
			this.persistentObjects = new PersistentObject[] { o };
			return;
		}
		Array.Resize<PersistentObject>(ref this.persistentObjects, this.persistentObjects.Length + 1);
		this.persistentObjects[this.persistentObjects.Length - 1] = o;
		o.id = this.persistentObjects.Length - 1;
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x0003AE4C File Offset: 0x0003904C
	[ContextMenu("Remove Null Objects")]
	public void RemoveNullObjects()
	{
		List<PersistentObject> list = new List<PersistentObject>(this.persistentObjects);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == null)
			{
				list.RemoveAt(i);
				i--;
			}
		}
		this.persistentObjects = list.ToArray();
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x0003AE9C File Offset: 0x0003909C
	[ContextMenu("Find Persistent Objects")]
	public void FindPersistentObjects()
	{
		this.AssignIDs();
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x0003AEA4 File Offset: 0x000390A4
	[ContextMenu("Assign IDs")]
	public void AssignIDs()
	{
		for (int i = 0; i < this.persistentObjects.Length; i++)
		{
			if (this.persistentObjects[i] != null)
			{
				this.persistentObjects[i].id = i;
			}
		}
	}

	// Token: 0x04000FF4 RID: 4084
	private static SceneSaveData instance;

	// Token: 0x04000FF5 RID: 4085
	private bool isLoaded;

	// Token: 0x04000FF6 RID: 4086
	public PersistentObject[] persistentObjects;

	// Token: 0x04000FF7 RID: 4087
	private bool[] objectStates;

	// Token: 0x04000FF8 RID: 4088
	private const bool reuseExistingSlots = false;
}
