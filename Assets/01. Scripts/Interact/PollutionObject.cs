using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PollutionObject : MonoBehaviour
{
    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.onProgressComplete.AddListener(Interact);
    }

    public void Interact()
    {
        print("오염오브젝트 상호작용");

        if (PlayerStats.Instance.IsFetilizerBag)
        {
            var trashData = TrashDatabase.Instance.RandomTrash();
            //int dropAmount = Random.Range(1, 3);
            TrashInventory.Instance.GetTrash(trashData, 1);
        }

        var stage = SceneManager.GetActiveScene().name;

        gameObject.SetActive(false);
        GameManager.Instance.UpdateClearProgress(stage, 10f);


    }
}
