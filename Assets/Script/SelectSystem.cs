using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 選択オブジェクトクラス
/// </summary>
public class SelectSystem : MonoBehaviour
{
    [SerializeField] Selectable firstSelect;
    [SerializeField] EventSystem ev;
    GameObject memory;

    private void Start()
    {
        firstSelect.Select();
        memory = firstSelect.gameObject;
    }

    private void Update()
    {
        //①現在選択されていなければ
        if (!ev.currentSelectedGameObject)
        {
            ev.SetSelectedGameObject(memory);
        }
        else if (memory.name != ev.currentSelectedGameObject.name)
        {
            memory = ev.currentSelectedGameObject;
        }
    }
}
