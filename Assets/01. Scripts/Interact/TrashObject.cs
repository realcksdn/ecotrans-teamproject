using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashObject : MonoBehaviour
{
    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.onProgressComplete.AddListener(Interact);
    }

    public void Interact()
    {
        var trashData = TrashDatabase.Instance.RandomTrash();
        //int dropAmount = Random.Range(1, 3);
        TrashInventory.Instance.GetTrash(trashData, 1);

        var stage = SceneManager.GetActiveScene().name;
        GameManager.Instance.UpdateClearProgress(stage, 8);
        gameObject.SetActive(false);
    }
}
