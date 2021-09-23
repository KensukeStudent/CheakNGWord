using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

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
        //入力テキスト
        var inputCharacter = "もるこ";

        var ret = NGWrodCheaker(inputCharacter) ?
        "この言葉はNGワードです："           + inputCharacter:
        "この言葉はNGワードではありません：" + inputCharacter;

        Debug.Log(ret);
    }

    /// <summary>
    /// 入力された文字列がNGワードにあるかをチェックします
    /// </summary>
    /// <param name="inputCharacter">入力文字列</param>
    bool NGWrodCheaker(string inputCharacter)
    {
        var ret = false;
        //文字タイプを取得
        var charaType = charaClass.AnalysisType(inputCharacter);

        //入力したタイプに同じ文字列があるか(ひらがな、カタカナ、ローマ字、それ以外)
        if (charaType == ChagneCharacters.CharaType.Japasece    ||
            charaType == ChagneCharacters.CharaType.ZenKatakana ||
            charaType == ChagneCharacters.CharaType.English     ||
            charaType == ChagneCharacters.CharaType.Else)
        {
            ret = SameCharacterCheaker(inputCharacter, (int)charaType);

            if (ret) return true;
        }

        switch (charaType)
        {
            //ひらがな、半角カタカナ、全角カタカナ
            case ChagneCharacters.CharaType.Japasece:
            case ChagneCharacters.CharaType.ZenKatakana:
            case ChagneCharacters.CharaType.HanKatakana:    
                //全探索で文字の長さを比較 --> 文字番号を比較 --> マッチしていれば検査開始



                break;

            //ローマ字
            case ChagneCharacters.CharaType.English:
                //最初の一文字目を比較 --> マッチしていれば検査開始

                break;
        }

        return false;
    }

    /// <summary>
    /// 同じ文字列があればチェック
    /// </summary>
    /// <param name="inputCharacter">入力された文字列</param>
    bool SameCharacterCheaker(string inputCharacter,int charaType)
    {
        var ngWordInfo = new NGWordInfo();
        
        for (int NGtype = 0; NGtype < ngWordInfo.typeCount; NGtype++)
        {
            //NG表現配列
            var ngWordType = ngWordInfo.GetNGWord(NGtype);

            //入力されたタイプと同じ文字タイプ
            var ngWord = ngWordType.GetWord(charaType);

            if (ngWord.Contains(inputCharacter)) return true;
        }

        return false; 
    }
}