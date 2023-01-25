using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // 앞의 int는 id, 뒤의 string[]은 대화내용
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
        talkData.Add(1000, new string[] { "루도는 왜 항상 물건을 잃어버리는거람.:3", "저번에는 집 열쇠를 잃어버려서 고생했다니깐.:2", "알고보니 자기 집 문밑에 떨어져 있더라구.:1" });
        talkData.Add(2000, new string[] { "동전을 찾아줘서 정말 고마워.:2", "다음에 만나면 꼭 보답할게.:2" });
        talkData.Add(3000, new string[] { "평범한 나무상자다.", "안에는 비어있는 듯 하다." });
        talkData.Add(4000, new string[] { "누군가 사용한 흔적이 있는 책상이다.", "...", "왜 책상이 바깥에 있는거지?" });
        talkData.Add(5000, new string[] { "커다란 바위다.", "들어서 옮기기는 힘들겠는걸." });

        // Quest Talk
        // 무시되는 NPC가 없도록 questIndex를 증가시켜서 대화순서 지정
        talkData.Add(10 + 1000, new string[] { "안녕!:0", "아까 루도가 곤란한 표정을 짓고 있던데..:1", "혹시 루도를 도와줄 수 있을까?:0" });
        talkData.Add(11 + 1000, new string[] { "루도는 오른쪽 호수에 있어.:0" });
        talkData.Add(11 + 2000, new string[] { "이상하네..:0", "동전 3개를 잃어버렸는데 어딨는지 모르겠어..:1", "혹시 내 동전 좀 찾아줄 수 있니?:0", "이 근처는 내가 찾아볼테니 마을 근처를 수색해줘.:1" });

        // 동전을 줍기 전 NPC들과의 대화
        talkData.Add(20 + 1000, new string[] { "루도의 동전?:1", "돈을 흘리고 다니면 못쓰지!:3", "나중에 한마디 해야겠어.:3" });
        talkData.Add(20 + 2000, new string[] { "마을은 왔던 길로 다시 가면 나올거야.:1", "찾으면 꼭 좀 가져다 줘.:0" });
        talkData.Add(20 + 10000, new string[] { "동색 동전을 찾았다." });
        talkData.Add(21 + 20000, new string[] { "은색 동전을 찾았다." });
        talkData.Add(22 + 30000, new string[] { "금색 동전을 찾았다.", "이제 루도에게 돌려주자." });
        // 동전을 주운 후
        talkData.Add(23 + 2000, new string[] { "왔구나!:1", "찾아줘서 정말 고마워.:2" });

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

    // 대화 내용 반환 -> GameManager에서 사용하기 위해 public
    public string GetTalk(int id, int talkIndex)
    {
        // Exception
        if (!talkData.ContainsKey(id))                          // talkData 배열에 현재 id를 포함하지 않는 경우 (ex. id가 21+1000, 10+3000의 경우는 대사가 없음)
        {
            if (!talkData.ContainsKey(id - id % 10))            // 현재 퀘스트의 가장 첫 대사가 있는가?
            {
                return GetTalk(id - id % 100, talkIndex);       // 퀘스트 처음 대사마저 없는 경우(일반 Object)
            }
            else
            {
                // 퀘스트 진행 순서 대사가 없는 경우(퀘스트 중간에 대사가 없음)
                // id - id % 10 : 현재 id의 1의자리(현재퀘스트의 대화순서)를 지워줌 -> 퀘스트의 가장 처음 대사를 가져오게 됨.
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
