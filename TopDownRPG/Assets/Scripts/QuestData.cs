using System.Collections;
using System.Collections.Generic;

// Object에 넣지 않고 스크립트에서만 사용하기 위해서 MonoBehaviour를 지움 / using Unity Engine도 필요없음
public class QuestData
{
    public string questName;
    public int[] npcId; // 퀘스트와 연관되어있는 NPC의 ID 배열

    // 생성자 (다른 스크립트에서 new를 통해 QuestData를 호출하기 위해)
    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
