using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingleTone<GameManager>
{
    private Dictionary<string, float> stageProgress = new Dictionary<string, float>();
    private Dictionary<string, bool> isStageClear = new Dictionary<string, bool>();
    private Dictionary<string, Dictionary<string, bool>> stagePollusion = new Dictionary<string, Dictionary<string, bool>>();
    //바뀔 일 없으므로 읽기 전용
    private readonly string[] stages = { "Map1", "Map2", "Map3" };

    public UnityEvent<string, float> onProgressUpdated;

    private void OnEnable()
    {
        foreach (var stage in stages)
        {
            stageProgress[stage] = PlayerPrefs.GetFloat($"Progress_{stage}", 0);
            stagePollusion[stage] = new Dictionary<string, bool>();
            isStageClear[stage] = PlayerPrefs.GetInt($"Clear_{stage}", 0) == 1;
        }

        //나중에 끄기
        ResetAllMapState();
    }

    public void UpdateClearProgress(string stage, float progress)
    {
        if (isStageClear[stage]) return;

        stageProgress[stage] = Mathf.Clamp(stageProgress[stage] + progress, 0, 100);

        if (onProgressUpdated != null)
        {

            onProgressUpdated.Invoke(stage, stageProgress[stage]);
            print("실행함");
        }
        else
        {
            print("으아아아ㅏㄴㅁ기ㅓ아;ㄻ");

        }

        print($"{stage} : {stageProgress[stage]}% 진행");
        if (stageProgress[stage] >= 100)
        {
            isStageClear[stage] = true;
            PlayerPrefs.SetInt($"Clear_{stage}", 1);
        }

        SaveMapStage(stage);
    }

    public float GetClearProgress(string stage)
    {
        return stageProgress[stage];
    }

    public bool IsStageCleared(string stage)
    {
        return isStageClear[stage];
    }

    public void SaveMapStage(string stage)
    {
        var trashObjs = FindObjectsOfType<PollutionObject>(true); //인수 = 비활성화오브젝트포함여부
        var trashStates = stagePollusion[stage];
        trashStates.Clear();
        foreach (var trash in trashObjs)
            trashStates[trash.gameObject.name] = trash.gameObject.activeSelf;

        var serializableStates = new SerializableTrashStates();
        foreach (var state in trashStates)
        {
            serializableStates.states.Add(new TrashState { name = state.Key, isAction = state.Value });
        }

        string json = JsonUtility.ToJson(serializableStates);
        PlayerPrefs.SetString($"TrashStates_{stage}", json);
        PlayerPrefs.SetFloat($"Progress_{stage}", stageProgress[stage]);
        PlayerPrefs.Save();

        print(PlayerPrefs.GetString($"TrashStates_{stage}"));
    }

    public void LoadMapState(string stage)
    {
        stageProgress[stage] = PlayerPrefs.GetFloat($"Progress_{stage}", 0);
        onProgressUpdated.Invoke(stage, stageProgress[stage]);
        if (PlayerPrefs.HasKey($"TrashStates_{stage}"))
        {
            string json = PlayerPrefs.GetString($"TrashStates_{stage}");
            print($"{stage}: {json}");

            var serializableStatess = JsonUtility.FromJson<SerializableTrashStates>(json);
            var trashStates = stagePollusion[stage];
            trashStates.Clear();
            foreach (var state in serializableStatess.states)
                trashStates[state.name] = state.isAction;

            var trashObjs = FindObjectsOfType<PollutionObject>(true);
            foreach (var trash in trashObjs)
            {
                if (trashStates.TryGetValue(trash.gameObject.name, out var isAction))
                    trash.gameObject.SetActive(isAction);
            }
        }
        else
            print("로드된 데이터 없음");
    }

    public void ResetAllMapState()
    {
        PlayerPrefs.DeleteAll();

        //현재 스테이지는 다음 로드 떄 초기화 되므로
        foreach (var stage in stages)
        {
            stageProgress[stage] = 0;
            isStageClear[stage] = false;
            stagePollusion[stage].Clear();

            if (stage == SceneManager.GetActiveScene().name)
            {
                var trashObjects = FindObjectsOfType<TrashObject>(true);
                var pollutionObjects = FindObjectsOfType<PollutionObject>(true);
                foreach (var trash in trashObjects)
                    trash.gameObject.SetActive(true);
                foreach (var pollution in pollutionObjects)
                    pollution.gameObject.SetActive(true);
                onProgressUpdated.Invoke(stage, 0);
            }
        }
        PlayerPrefs.Save();
        print("데이터 초기화 완료");
    }

    [Serializable]
    private class SerializableTrashStates
    {
        public List<TrashState> states = new List<TrashState>();
    }

    [Serializable]
    private class TrashState
    {
        public string name;
        public bool isAction;
    }
}
