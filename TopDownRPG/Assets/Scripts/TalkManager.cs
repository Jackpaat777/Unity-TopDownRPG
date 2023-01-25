using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // ���� int�� id, ���� string[]�� ��ȭ����
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // Talk Data
        // 1000:Luna, 2000:Ludo
        // 100:Box, 200:Desk
        talkData.Add(1000, new string[] { "�絵�� �� �׻� ������ �Ҿ�����°Ŷ�.:3", "�������� �� ���踦 �Ҿ������ ����ߴٴϱ�.:2", "�˰��� �ڱ� �� ���ؿ� ������ �ִ���.:1" });
        talkData.Add(2000, new string[] { "������ ã���༭ ���� ����.:2", "������ ������ �� �����Ұ�.:2" });
        talkData.Add(3000, new string[] { "����� �������ڴ�.", "�ȿ��� ����ִ� �� �ϴ�." });
        talkData.Add(4000, new string[] { "������ ����� ������ �ִ� å���̴�.", "...", "�� å���� �ٱ��� �ִ°���?" });
        talkData.Add(5000, new string[] { "Ŀ�ٶ� ������.", "�� �ű��� ����ڴ°�." });

        // Quest Talk
        // ���õǴ� NPC�� ������ questIndex�� �������Ѽ� ��ȭ���� ����
        talkData.Add(10 + 1000, new string[] { "�ȳ�!:0", "�Ʊ� �絵�� ����� ǥ���� ���� �ִ���..:1", "Ȥ�� �絵�� ������ �� ������?:0" });
        talkData.Add(11 + 1000, new string[] { "�絵�� ������ ȣ���� �־�.:0" });
        talkData.Add(11 + 2000, new string[] { "�̻��ϳ�..:0", "���� 3���� �Ҿ���ȴµ� ������� �𸣰ھ�..:1", "Ȥ�� �� ���� �� ã���� �� �ִ�?:0", "�� ��ó�� ���� ã�ƺ��״� ���� ��ó�� ��������.:1" });

        // ������ �ݱ� �� NPC����� ��ȭ
        talkData.Add(20 + 1000, new string[] { "�絵�� ����?:1", "���� �긮�� �ٴϸ� ������!:3", "���߿� �Ѹ��� �ؾ߰ھ�.:3" });
        talkData.Add(20 + 2000, new string[] { "������ �Դ� ��� �ٽ� ���� ���ðž�.:1", "ã���� �� �� ������ ��.:0" });
        talkData.Add(20 + 10000, new string[] { "���� ������ ã�Ҵ�." });
        talkData.Add(21 + 20000, new string[] { "���� ������ ã�Ҵ�." });
        talkData.Add(22 + 30000, new string[] { "�ݻ� ������ ã�Ҵ�.", "���� �絵���� ��������." });
        // ������ �ֿ� ��
        talkData.Add(23 + 2000, new string[] { "�Ա���!:1", "ã���༭ ���� ����.:2" });

        // Portrait Data
        // 0:Normal, 1:Speak, 2:Happy, 3:Angry
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    // ��ȭ ���� ��ȯ -> GameManager���� ����ϱ� ���� public
    public string GetTalk(int id, int talkIndex)
    {
        // Exception
        if (!talkData.ContainsKey(id))                          // talkData �迭�� ���� id�� �������� �ʴ� ��� (ex. id�� 21+1000, 10+3000�� ���� ��簡 ����)
        {
            if (!talkData.ContainsKey(id - id % 10))            // ���� ����Ʈ�� ���� ù ��簡 �ִ°�?
            {
                return GetTalk(id - id % 100, talkIndex);       // ����Ʈ ó�� ��縶�� ���� ���(�Ϲ� Object)
            }
            else
            {
                // ����Ʈ ���� ���� ��簡 ���� ���(����Ʈ �߰��� ��簡 ����)
                // id - id % 10 : ���� id�� 1���ڸ�(��������Ʈ�� ��ȭ����)�� ������ -> ����Ʈ�� ���� ó�� ��縦 �������� ��.
                return GetTalk(id - id % 10, talkIndex);
            }
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
