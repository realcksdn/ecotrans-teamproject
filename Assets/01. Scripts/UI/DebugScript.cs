using UnityEngine;

public class DebugScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Persistent 리스너 수 확인
            int persistentCount = GameManager.Instance.onProgressUpdated.GetPersistentEventCount();
            print($"Persistent 리스너 수: {persistentCount}");

            // 리스너가 등록되어 있는지 간접 확인
            if (GameManager.Instance.onProgressUpdated != null)
            {
                print("onProgressUpdated가 null이 아님. 리스너가 있을 가능성 있음!");
            }
            else
            {
                print("onProgressUpdated가 null임. 리스너 등록 안 됨!");
            }
        }
    }
}