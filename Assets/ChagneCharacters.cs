using System.Text;
using System.Collections.Generic;
using System;

//濁点

/// <summary>
/// 文字変換クラス
/// </summary>
public class ChagneCharacters
{
    readonly string[] japanese =
    {
        "あ","い","う","え","お",
        "か","き","く","け","こ",
        "が","ぎ","ぐ","げ","ご",
        "さ","し","す","せ","そ",
        "ざ","じ","ず","ぜ","ぞ",
        "た","ち","つ","て","と",
        "だ","ぢ","づ","で","ど",
        "な","に","ぬ","ね","の",
        "は","ひ","ふ","へ","ほ",
        "ば","び","ぶ","べ","ぼ",
        "ぱ","ぴ","ぷ","ぺ","ぽ",
        "ま","み","む","め","も",
        "や",     "ゆ",     "よ",
        "ら","り","る","れ","ろ",
        "わ","を","ん"
    };

    readonly string[] zenkana =
    {
        "ア","イ","ウ","エ","オ",
        "カ","キ","ク","ケ","コ",
        "ガ","ギ","グ","ゲ","ゴ",
        "サ","シ","ス","セ","ソ",
        "ザ","ジ","ズ","ゼ","ゾ",
        "タ","チ","ツ","テ","ト",
        "ダ","ヂ","ヅ","デ","ド",
        "ナ","ニ","ヌ","ネ","ノ",
        "ハ","ヒ","フ","ヘ","ホ",
        "バ","ビ","ブ","ベ","ボ",
        "パ","ピ","プ","ペ","ポ",
        "マ","ミ","ム","メ","モ",
        "ヤ",     "ユ",     "ヨ",
        "ラ","リ","ル","レ","ロ",
        "ワ","ヲ","ン",
    };

    readonly string[] hankana =
    {
        "ｱ", "ｲ", "ｳ", "ｴ", "ｵ",
        "ｶ", "ｷ", "ｸ", "ｹ", "ｺ",
        "ｶﾞ","ｷﾞ","ｸﾞ","ｹﾞ","ｺﾞ",
        "ｻ", "ｼ", "ｽ", "ｾ", "ｿ",
        "ｻﾞ","ｼﾞ","ｽﾞ","ｾﾞ","ｿﾞ",
        "ﾀ", "ﾁ", "ﾂ", "ﾃ", "ﾄ",
        "ﾀﾞ","ﾁﾞ","ﾂﾞ","ﾃﾞ","ﾄﾞ",
        "ﾅ", "ﾆ", "ﾇ", "ﾈ", "ﾉ",
        "ﾊ", "ﾋ", "ﾌ", "ﾍ", "ﾎ",
        "ﾊﾞ","ﾋﾞ","ﾌﾞ","ﾍﾞ","ﾎﾞ",
        "ﾊﾟ","ﾋ","ﾟﾌﾟ","ﾍﾟ","ﾎﾟ",
        "ﾏ", "ﾐ", "ﾑ", "ﾒ", "ﾓ",
        "ﾔ",      "ﾕ",      "ﾖ",
        "ﾗ", "ﾘ", "ﾙ", "ﾚ", "ﾛ",
        "ﾜ", "ｦ", "ﾝ",
    };

    readonly string[] english =
    {
        "a",  "i", "u", "e", "o",
        "ka","ki","ku","ke","ko",
        "ga","gi","gu","ge","go",
        "sa","si","su","se","so",
        "za","zi","zu","ze","zo",
        "ta","ti","tu","te","to",
        "da","di","du","de","do",
        "na","ni","nu","ne","no",
        "ha","hi","hu","he","ho",
        "ba","bi","bu","be","bo",
        "pa","pi","pu","pe","po",
        "ma","mi","mu","me","mo",
        "ya",     "yu",     "yo",
        "ra","ri","ru","re","ro",
        "wa","wo", "n",
    };

    /// <summary>
    /// 現在の文字タイプ
    /// </summary>
    public enum CharaType
    {
        Japanese,
        ZenKatakana,
        HanKatakana,
        English
    }

    /// <summary>
    /// 指定した文字タイプへ変換した文字列を返します
    /// </summary>
    /// <param name="character">入力文字列</param>
    /// <param name="NGWord">変換する文字タイプ</param>
    /// <returns></returns>
    public string ToChangeCharacter(string character, string NGWord)
    {
        var sb = new StringBuilder("");
        string[][] arrayType = { japanese, zenkana, hankana, english };

        //入力された文字ごとの番号格納変数
        var arrayNo = IndexOf(character);

        //文字配列
        var array = arrayType[0/*AnalysisType(NGWord)*/];

        //入力された文字 -----> NGWordの文字タイプに変換
        for (int i = 0; i < character.Length; i++)
        {
            sb.Append(array[arrayNo[i]]);
        }

        return sb.ToString();
    }

    /// <summary>
    /// 入力された文字列が文字配列のインデックス番号の何番目にあるか全て返します
    /// </summary>
    /// <param name="character">入力する文字</param>
    int[] IndexOf(string character)
    {
        //文字タイプ配列番号を取得
        //var charaType = AnalysisType(character);

        var charaType = 1;

        string[][] arrayType = { japanese, zenkana, hankana, english };

        //文字タイプ番号格納変数
        int[] arrayNo = new int[character.Length];

        for (int c = 0; c < character.Length; c++)
        {
            for (int i = 0; i < arrayType[charaType].Length; i++)
            {
                if (character[c].ToString() == arrayType[charaType][i])
                {
                    arrayNo[c] = i;
                    break;
                }
            }
        }

        return arrayNo;
    }

    /// <summary>
    /// 文字列からひらがな、カタカナ、ローマ字、漢字を判別
    /// </summary>
    int AnalysisType(string character)
    {
        return 1;
    }

    int CharacterLength()
    {
        //濁点は判別しない

        return 0;
    }
}