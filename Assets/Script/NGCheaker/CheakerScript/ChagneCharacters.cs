using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NGCheaker
{
    /// <summary>
    /// 文字変換クラス
    /// </summary>
    public class ChagneCharacters
    {
        readonly int arrayCount;

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
        CharacterCheak[] characterCheak = null;

        public ChagneCharacters()
        {
            //配列内の合計文字数を取得
            arrayCount = japanese.Length;

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
        /// <param name="inputCharacter">入力文字</param>
        /// <param name="NGWord">NGワード</param>
        public bool NGJudgement(string inputCharacter, string NGWord)
        {
            //文字列が同じ出なければNGワードを入力された文字タイプに変換
            var judgeCharacter = ToChangeCharacter(inputCharacter, NGWord);

            return Regex.IsMatch(inputCharacter, judgeCharacter, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 指定した文字タイプへ変換した後、文字列を返します
        /// </summary>
        /// <param name="inputCharacter">入力文字列</param>
        /// <param name="NGWord">変換する文字タイプ</param>
        string ToChangeCharacter(string inputCharacter, string NGWord)
        {
            var sb = new StringBuilder("");

            //NGワードの文字タイプ配列から格納番号を取得
            var arrayNo = IndexOf(NGWord);
            //入力された文字タイプを識別
            var inputType = AnalysisType(inputCharacter);
            if (arrayNo == null || inputType == CharaType.Else) return NGWord;

            var arrayType = GetCharacterArray(inputType);

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

            var arrayType = GetCharacterArray(charaType);

            //文字の長さを取得
            var characterLength = NGWord.Length;
            //文字タイプ番号格納変数
            int[] arrayNo = new int[characterLength];

            for (int c = 0; c < characterLength; c++)
            {
                var index = arrayType.IndexOf(NGWord[c].ToString());

                if (index != -1) arrayNo[c] = index;
                else return null;
            }

            return arrayNo;
        }

        /// <summary>
        /// 文字列からひらがな、全角カタカナ、半角カタカナ、ローマ字のどれかを判定して番号を返します
        /// それ以外は-1を返します
        /// </summary>
        public CharaType AnalysisType(string character)
        {
            for (int i = 0; i < characterCheak.Length; i++)
            {
                if (characterCheak[i](character))
                {
                    return (CharaType)Enum.ToObject(typeof(CharaType), i);
                }
            }

            return CharaType.Else;
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
        public bool CheakCharacterNo(string inputCharacter, string NGWord)
        {
            string[] judgeCharacters = { inputCharacter, NGWord };
            int[] judgeNo = { -1, -1 };

            for (int i = 0; i < judgeCharacters.Length; i++)
            {
                var charaType = AnalysisType(judgeCharacters[i]);
                //タイプがElseなら処理しません
                if (charaType == CharaType.Else) return false;

                //一文字目を取得 ---->
                //半角カタカナ場合少し変更
                var character = charaType == CharaType.HanKatakana ?
                    GetHanKakuChar(judgeCharacters[i])
                    :
                    judgeCharacters[i][0].ToString();

                var arrayType = GetCharacterArray(charaType);

                //一文字目の配列番号を取得
                judgeNo[i] = arrayType.IndexOf(character);
            }

            return judgeNo[0] == judgeNo[1];
        }

        /// <summary>
        /// 入力された一文字目とNGワードの一文字目が同じか判定
        /// </summary>
        public bool CheakSameChar(string inputCharacter, string NGWord)
        {
            //NGワードから文字タイプ識別
            var charaType = AnalysisType(NGWord);
            if (charaType == CharaType.Else) return false;
            var arrayType = GetCharacterArray(charaType);

            //一文字目を取得 ---->
            //半角カタカナ場合少し変更
            var NGChar = charaType == CharaType.HanKatakana ?
                    GetHanKakuChar(NGWord)
                    :
                    NGWord[0].ToString();

            var index = arrayType.IndexOf(NGChar);

            if (index < 0 || index > arrayCount) return false;

            return Regex.IsMatch(inputCharacter[0].ToString(), english[index][0].ToString(), RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 半角カタカナ文字を取得
        /// </summary>
        string GetHanKakuChar(string hChar)
        {
            var ret = Regex.IsMatch(hChar, @"^.(ﾞ|ﾟ)");

            return ret ?
                hChar.Substring(0, 2)//半角カタカナなので2文字分
                :
                hChar[0].ToString();
        }

        /// <summary>
        /// 文字タイプ配列を取得
        /// </summary>
        List<string> GetCharacterArray(CharaType charaType)
        {
            string[][] characterType = { japanese, zenkana, hankana, english };
            return characterType[(int)charaType].ToList();
        }
    }
}