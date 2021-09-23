using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

///判定前審査
///入力された文字が漢字か日本語化
/// ↓
/// 英語なら日本語の方に回す
/// ↓
/// 日本語なら文字列の長さを比較
/// 英語なら最初の一文字と日本語の一文字目を比較
/// ↓
/// 異なれば判定開始


/// 判定の仕方
///禁止ワードと打ち込まれた文字を比較
///打ち込まれた文字(ひらがな、カタカナ、半角カタカナ、ローマ字)
///打ち込まれた文字のタイプにNGWordを整形
///漢字､ローマ字は省く --> 漢字の時はそのまま判定

public class CheakNGWord : MonoBehaviour
{
    /// <summary>
    /// 文字列変換クラス
    /// </summary>
    ChagneCharacters charaClass = new ChagneCharacters();

    private void Start()
    {
        //NGワード
        var NGWord = "ぎろ";
        //入力テキスト
        var inputCharacter = "Giro";

        Debug.Log(charaClass.NGJudgement(inputCharacter,NGWord));
    }
}