using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200021B RID: 539
public class Parser
{
	// Token: 0x06000A0A RID: 2570 RVA: 0x00009AC5 File Offset: 0x00007CC5
	public Parser(string t)
	{
		this.text = t;
		this.index = 0;
		this.length = this.text.Length;
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00009AEC File Offset: 0x00007CEC
	private string nextCharacter()
	{
		if (this.index == this.length)
		{
			return "";
		}
		string text = this.text.Substring(this.index, 1);
		this.index++;
		return text;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0003B110 File Offset: 0x00039310
	private bool isWhitespace(string cha)
	{
		return cha.Equals(" ") || cha.Equals("\t") || cha.Equals("\n") || cha.Equals("\r") || cha.Equals("\v") || cha.Equals("");
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0003B170 File Offset: 0x00039370
	private string nextLexeme()
	{
		string text = this.nextCharacter();
		if (text.Equals(""))
		{
			return "";
		}
		while (this.isWhitespace(text) && !text.Equals(""))
		{
			text = this.nextCharacter();
		}
		if (text.Equals(""))
		{
			return "";
		}
		if (text.Equals("\""))
		{
			bool flag = false;
			string text2 = this.nextCharacter();
			while (!text2.Equals("\"") && !flag)
			{
				if (text2.Equals("\\"))
				{
					text2 = this.nextCharacter();
				}
				text += text2;
				text2 = this.nextCharacter();
			}
			text += text2;
		}
		else
		{
			string text3 = this.nextCharacter();
			while (!this.isWhitespace(text3))
			{
				text += text3;
				text3 = this.nextCharacter();
			}
		}
		return text;
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0003B240 File Offset: 0x00039440
	private DialogueChunk nextDialogueChunk()
	{
		string text = this.nextLexeme();
		if (text.Equals(""))
		{
			return null;
		}
		int num = -1;
		List<DialogueLine> list = new List<DialogueLine>();
		string text2 = this.nextLexeme();
		while (!text2.Equals("Options") && !text2.Equals("End") && !text2.Equals(""))
		{
			string text3 = this.nextLexeme();
			DialogueLine dialogueLine = default(DialogueLine);
			dialogueLine.emote = Animator.StringToHash("None");
			dialogueLine.cue = false;
			int num2 = -1;
			int num3 = -1;
			if (!int.TryParse(text2, out dialogueLine.actorIndex))
			{
				Debug.LogError("Invalid int: " + text2 + " in dialogue chunk " + text);
			}
			while (!text3.StartsWith("\""))
			{
				if (text3.StartsWith("L"))
				{
					num = int.Parse(text3.Substring(1, text3.Length - 1));
				}
				else if (text3.Equals("Q"))
				{
					dialogueLine.cue = true;
				}
				else if (text3.StartsWith("S"))
				{
					ActorState actorState;
					if (Enum.TryParse<ActorState>(text3, out actorState))
					{
						num2 = (int)actorState;
					}
				}
				else if (text3.StartsWith("P"))
				{
					ActorPosition actorPosition;
					if (Enum.TryParse<ActorPosition>(text3, out actorPosition))
					{
						num3 = (int)actorPosition;
					}
				}
				else
				{
					dialogueLine.emote = Animator.StringToHash(text3);
				}
				text3 = this.nextLexeme();
			}
			dialogueLine.english = this.clean(text3);
			dialogueLine.lookTarget = num;
			dialogueLine.state = num2;
			dialogueLine.position = num3;
			list.Add(dialogueLine);
			text2 = this.nextLexeme();
		}
		List<string> list2 = new List<string>();
		if (text2.Equals("Options"))
		{
			text2 = this.nextLexeme();
			while (!text2.Equals("End") && !text2.Equals(""))
			{
				list2.Add(this.clean(text2));
				text2 = this.nextLexeme();
			}
		}
		DialogueChunk dialogueChunk = new DialogueChunk();
		dialogueChunk.name = text;
		dialogueChunk.lines = list.ToArray();
		dialogueChunk.options = list2.ToArray();
		dialogueChunk.mlOptions = new MultilingualString[dialogueChunk.options.Length];
		for (int i = 0; i < dialogueChunk.options.Length; i++)
		{
			dialogueChunk.mlOptions[i].english = dialogueChunk.options[i];
		}
		return dialogueChunk;
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00009B22 File Offset: 0x00007D22
	private string clean(string text)
	{
		if (text.Length - 2 < 0)
		{
			Debug.Log(text);
		}
		return text.Substring(1, text.Length - 2);
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0003B49C File Offset: 0x0003969C
	public List<DialogueChunk> getChunks()
	{
		List<DialogueChunk> list = new List<DialogueChunk>();
		for (DialogueChunk dialogueChunk = this.nextDialogueChunk(); dialogueChunk != null; dialogueChunk = this.nextDialogueChunk())
		{
			list.Add(dialogueChunk);
		}
		return list;
	}

	// Token: 0x04000C96 RID: 3222
	private const string defaultEmote = "None";

	// Token: 0x04000C97 RID: 3223
	private string text;

	// Token: 0x04000C98 RID: 3224
	private int index;

	// Token: 0x04000C99 RID: 3225
	private int length;
}
