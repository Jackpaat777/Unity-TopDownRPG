using System.Collections;
using System.Collections.Generic;

// Object�� ���� �ʰ� ��ũ��Ʈ������ ����ϱ� ���ؼ� MonoBehaviour�� ���� / using Unity Engine�� �ʿ����
public class QuestData
{
    public string questName;
    public int[] npcId; // ����Ʈ�� �����Ǿ��ִ� NPC�� ID �迭

    // ������ (�ٸ� ��ũ��Ʈ���� new�� ���� QuestData�� ȣ���ϱ� ����)
    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
