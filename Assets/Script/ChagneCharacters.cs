using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

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

    public enum CharaType
    {
        Japasece,
        ZenKatakana,
        HanKatakana,
        English,
        Else
    }

    delegate bool CharacterCheak(string character);
    CharacterCheak[] characterCheak;

    public ChagneCharacters()
    {
        characterCheak = new CharacterCheak[4]
        {
            IsHiragana,
            IsZenKatakana,
            IsHanKatakana,
            IsEnglish,
        };
    }

    /// <summary>
    /// NGワードと入力された文字列を判定します
    /// </summary>
    /// <param name="character">入力文字</param>
    /// <param name="NGWord">NGワード</param>
    public bool NGJudgement(string character, string NGWord)
    {
        //文字が同じかどうかを判定
        if (Regex.IsMatch(character, NGWord, RegexOptions.IgnoreCase)) return true;

        //文字列が同じ出なければNGワードを入力された文字タイプに変換
        var judgeCharacter = ToChangeCharacter(character, NGWord);

        return Regex.IsMatch(character, judgeCharacter, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 指定した文字タイプへ変換した文字列を返します
    /// </summary>
    /// <param name="character">入力文字列</param>
    /// <param name="NGWord">変換する文字タイプ</param>
    string ToChangeCharacter(string character, string NGWord)
    {
        var sb = new StringBuilder("");

        //NGワードの文字タイプ配列から格納番号を取得
        var arrayNo = IndexOf(NGWord);
        //入力された文字タイプを識別
        var charaType = AnalysisType(character);
        if (arrayNo == null || charaType == CharaType.Else) return NGWord;

        var arrayType = GetCharacterArray((int)charaType);

        //NGWord文字 -----> 入力された文字タイプに変換
        for (int i = 0; i < NGWord.Length; i++)
        {
            sb.Append(arrayType[arrayNo[i]]);
        }

        return sb.ToString();
    }

    /// <summary>
    /// NGWordが文字配列のインデックス番号の何番目にあるか全て返します
    /// </summary>
    /// <param name="NGWord">NGワード</param>
    int[] IndexOf(string NGWord)
    {
        //NGワードから文字タイプ識別
        var charaType = AnalysisType(NGWord);

        if (charaType != CharaType.Japasece && charaType != CharaType.ZenKatakana) return null;

        var arrayType = GetCharacterArray((int)charaType);

        //文字の長さを取得
        var characterLength = NGWord.Length;
        //文字タイプ番号格納変数
        int[] arrayNo = new int[characterLength];

        for (int c = 0; c < characterLength; c++)
        {
            var index = arrayType.IndexOf(NGWord[c].ToString());

            if (index != -1) arrayNo[c] = index;
        }

        return arrayNo;
    }

    /// <summary>
    /// 文字列からひらがな、全角カタカナ、半角カタカナ、ローマ字を判定して番号を返します
    /// それ以外は-1を返します
    /// </summary>
    public CharaType AnalysisType(string character)
    {
        var cheakCount = characterCheak.Length;
        CharaType charaType;
        for (int i = 0; i < cheakCount; i++)
        {
            if (characterCheak[i](character))
            {
                return charaType = (CharaType)Enum.ToObject(typeof(CharaType),i);
            }
        }

        return charaType = CharaType.Else;
    }

    /// <summary>
    /// ひらがなを検索
    /// </summary>
    bool IsHiragana(string character)
    {
        //ぁ～ゞまで
        var regex = new Regex(@"^[\u3041-\u309E]+$");
        return regex.IsMatch(character);
    }

    /// <summary>
    /// 全角カタカナを検索
    /// </summary>
    bool IsZenKatakana(string character)
    {
        //ァ～ヺまで
        var regex = new Regex(@"^[\u30A1-\u30FA]+$");
        return regex.IsMatch(character);
    }

    /// <summary>
    /// 半角カタカナを検索
    /// </summary>
    bool IsHanKatakana(string character)
    {
        //ｧ～ﾝまで
        var regex = new Regex(@"^[\uFF67-\uFF9D]+$");
        return regex.IsMatch(character);
    }

    /// <summary>
    /// ローマ字を検索
    /// </summary>
    bool IsEnglish(string character)
    {
        //A～Zまで(小文字、大文字区別なし)
        var regex = new Regex(@"^[\u0041-\u005A]+$");
        return Regex.IsMatch(character, regex.ToString(), RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 入力された文字列とNGワードの文字列の最初の一文字目が同じか判定
    /// </summary>
    public bool GetCharacterNo(string character, string NGWord)
    {
        string[] judgeCharacters = { character, NGWord };
        int[] judgeNo = { -1, -1 };
        
        //character --> 半角カタカナの場合、濁点も入れる

        for (int i = 0; i < judgeCharacters.Length; i++)
        {
            var type = AnalysisType(judgeCharacters[i]);
            var arrayType = GetCharacterArray((int)type);

            for (int no = 0; no < arrayType.Count; no++)
            {
                if (character[i].ToString() == arrayType[no])
                    judgeNo[i] = no;
            }
        }

        return judgeNo[0] == judgeNo[1];
    }

    /// <summary>
    /// 文字タイプ配列
    /// </summary>
    List<string> GetCharacterArray(int charaType)
    {
        string[][] characterType = { japanese, zenkana, hankana, english };
        return characterType[charaType].ToList();
    }
}