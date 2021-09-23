using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//判定の仕方
//禁止ワードと打ち込まれた文字を比較
//打ち込まれた文字(ひらがな、カタカナ、半角カタカナ、ローマ字)

//打ち込まれた文字のタイプを検索

//漢字､ローマ字は省く --> 漢字の時はそのまま判定

public class CheakNGWord : MonoBehaviour
{
    /// <summary>
    /// 文字列変換クラス
    /// </summary>
    ChagneCharacters charaClass = new ChagneCharacters();

    private void Start()
    {
        //NGワード
        var NGWord = "ごりら";
        //入力テキスト
        var inputCharacter = "ゴリラ";


        Debug.Log(charaClass.ToChangeCharacter(inputCharacter, NGWord));
    }
}