using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ConversionUI : MonoBehaviour
{
    public Image curTrashIcon;
    public TextMeshProUGUI curTrashName;
    public TextMeshProUGUI curTrashExplanation;

    public GameObject warningText;

    [Header("Checks")]
    public Toggle removeToggle;
    public Toggle cleanToggle;
    public Toggle pressureToggle;
    public List<Toggle> categoryToggles;
    public Button checkBtn;

    private TrashInventory inventory;
    private TrashData selectedTrash;
    private string selectedCategory;

    [Header("인벤토리 표시")]
    public List<Image> slots;
    public Sprite nullImage;

    private void OnEnable()
    {
        inventory = TrashInventory.Instance;
        inventory.onInventoryChanges.AddListener(OnInventoryChanged);
        checkBtn.onClick.AddListener(Check);
        SetupCategoryButtons();
        UpdateUI();
    }

    private void OnDisable()
    {
        inventory.onInventoryChanges.RemoveListener(OnInventoryChanged);
    }

    private void SetupCategoryButtons()
    {
        for (int i = 0; i < categoryToggles.Count; i++)
        {
            //순서는 버튼 할당할 떄 정해야됨 (기획서 기준)
            var index = i;
            categoryToggles[i].onValueChanged.AddListener(isOn => { if (isOn) { SelectCategory(index); } });

            if (i == 0) categoryToggles[i].isOn = true; //처음거 기본 선택
        }
    }

    private void OnInventoryChanged(TrashData data)
    {
        //인수 패스
        UpdateUI();
    }

    private void SelectCategory(int index)
    {
        selectedCategory = index switch
        {
            0 => "Fiber",
            1 => "Plastic",
            2 => "Iron",
            3 => "Paper",
            4 => "Vinyl",
            5 => "Fertilizer",
            6 => "Glass",
            _ => ""
        };

        //단일선택
        for (int i = 0;i < categoryToggles.Count;i++)
            categoryToggles[i].isOn = (i == index);
    }

    public void Check()
    {
        bool toggleMatch = selectedTrash.isRemove == removeToggle.isOn && 
            selectedTrash.isClean == cleanToggle.isOn && 
            selectedTrash.isPressure == pressureToggle.isOn;

        //첫 번째 체크
        if (!toggleMatch){
            MissSelect(); return; }

        //두 번째 체크
        if (selectedTrash.category.name != selectedCategory)
        {
            MissSelect();
            return;
        }

        AcquireResource(selectedTrash);
    }

    private void AcquireResource(TrashData trash)
    {
        inventory.GetTrash(trash, -1);
        Inventory.Instance.GetResource(trash.category, trash.value);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (inventory.trashs.Count > 0)
        {
            selectedTrash = inventory.trashs[0].data;
            curTrashIcon.sprite = selectedTrash.icon;
            curTrashName.text = selectedTrash.trashName;
            curTrashExplanation.text = selectedTrash.explanation;
            checkBtn.gameObject.SetActive(true);
        }
        else
        {
            selectedTrash = null;
            curTrashIcon.sprite = nullImage;
            curTrashName.text = "쓰레기 없음";
            curTrashExplanation.text = "";
            checkBtn.gameObject.SetActive(false);
        }

        for (int i = 0;i< slots.Count;i++)
        {
            if (i >= inventory.trashs.Count || inventory.trashs[i] == null)
            {
                slots[i].sprite = nullImage;
                continue;
            }

            slots[i].sprite = inventory.trashs[i].data.icon;
        }
    }

    public void MissSelect()
    {
        StopAllCoroutines();
        StartCoroutine(Warning());
        //뭔가 이벤트
    }

    IEnumerator Warning()
    {
        warningText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningText.SetActive(false);
    }

    
}
