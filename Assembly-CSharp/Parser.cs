using System;
using System.Collections.Generic;
using UnityEngine;

public class Parser
{
	// Token: 0x06000A52 RID: 2642 RVA: 0x00009DE4 File Offset: 0x00007FE4
	public Parser(string t)
	{
		this.text = t;
		this.index = 0;
		this.length = this.text.Length;
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00009E0B File Offset: 0x0000800B
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

	// Token: 0x06000A54 RID: 2644 RVA: 0x0003CA24 File Offset: 0x0003AC24
	private bool isWhitespace(string cha)
	{
		return cha.Equals(" ") || cha.Equals("\t") || cha.Equals("\n") || cha.Equals("\r") || cha.Equals("\v") || cha.Equals("");
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0003CA84 File Offset: 0x0003AC84
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

	// Token: 0x06000A56 RID: 2646 RVA: 0x0003CB54 File Offset: 0x0003AD54
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
			dialogueLine.english[0] = this.clean(text3);
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
			dialogueChunk.mlOptions[i].english[0] = dialogueChunk.options[i];
		}
		return dialogueChunk;
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x00009E41 File Offset: 0x00008041
	private string clean(string text)
	{
		if (text.Length - 2 < 0)
		{
			Debug.Log(text);
		}
		return text.Substring(1, text.Length - 2);
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0003CDB4 File Offset: 0x0003AFB4
	public List<DialogueChunk> getChunks()
	{
		List<DialogueChunk> list = new List<DialogueChunk>();
		for (DialogueChunk dialogueChunk = this.nextDialogueChunk(); dialogueChunk != null; dialogueChunk = this.nextDialogueChunk())
		{
			list.Add(dialogueChunk);
		}
		return list;
	}

	private const string defaultEmote = "None";

	private string text;

	private int index;

	private int length;
}
