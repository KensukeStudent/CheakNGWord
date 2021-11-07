using NGCheaker;
using UnityEngine;
using UnityEngine.UI;

public class NameSet : MonoBehaviour
{
    [SerializeField] InputField inputField = null;
    [SerializeField] Text text = null;

    CheakNGWord cheakNG = new CheakNGWord();

    private void Start()
    {
        inputField.onEndEdit.AddListener(call => SendName());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// 名前を送信
    /// </summary>
    void SendName()
    {
        if(!string.IsNullOrEmpty(inputField.text))
        {
            var inputCharacter = inputField.text;

            var ret = cheakNG.NGWrodCheaker(inputCharacter) ?
            "この言葉はNGワードです：<color=red>" + inputCharacter + "</color>" :
            "この言葉はNGワードではありません：" + inputCharacter;

            text.text = ret;
        }
        else
        {
            text.text = "入力して下さい！！";
        }
    }
}
