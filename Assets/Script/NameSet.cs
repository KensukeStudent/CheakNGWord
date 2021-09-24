using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NGCheaker;

public class NameSet : MonoBehaviour
{
    [SerializeField] InputField inputField = null;
    [SerializeField] Text text = null;

    CheakNGWord cheakNG = new CheakNGWord();

    private void Start()
    {
        inputField.onEndEdit.AddListener(call => SendName());
    }

    /// <summary>
    /// ���O�𑗐M
    /// </summary>
    void SendName()
    {
        if(!string.IsNullOrEmpty(inputField.text))
        {
            var inputCharacter = inputField.text;

            var ret = cheakNG.NGWrodCheaker(inputCharacter) ?
            "���̌��t��NG���[�h�ł��F<color=red>" + inputCharacter + "</color>" :
            "���̌��t��NG���[�h�ł͂���܂���F" + inputCharacter;

            text.text = ret;
        }
        else
        {
            text.text = "���͂��ĉ������I�I";
        }
    }
}
