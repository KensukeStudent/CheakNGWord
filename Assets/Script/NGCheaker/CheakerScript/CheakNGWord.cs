using System.Collections.Generic;
using System.Text.RegularExpressions;

///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///                                                                                                         ///
///                                                                                                         ///
///                                              判定前審査                                                 ///
///                                      入力された文字タイプを取得                                         ///
///                                                                                                         ///
/// 【 ひらがな・全角カタカナ・半角カタカナ 】       |                     【 ローマ字 】                   ///
///                                                                                                         ///
/// 全探索で同じ文字の長さを取得・文字番号を比較         入力された文字列とNGワードの文字列の一文字目を比較 ///
///                                                 ↓↓                                                    ///
///                                 【 比較したマッチした文字列を審査開始 】                                ///
///                                                                                                         ///
///                                                                                                         ///
///////////////////////////////////////////////////////////////////////////////////////////////////////////////                                 


///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///                                                                                                         ///
///                                                                                                         ///
///                                              判定の仕方                                                 ///
///                                   入力された文字のタイプにNGワードを変換                                ///
///                                                                                                         ///
///                                                 ↓↓                                                    ///
///                                                                                                         ///
///                             入力文字文字列とNGワード文字列から配列番号を取得・比較                      ///
///                                                 ↓↓                                                    ///
///                                                                                                         ///
///                            マッチしていればNGフラグ・マッチしていなければそのまま通過                   ///
///                                                                                                         ///
///                                                                                                         ///
/////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

namespace NGCheaker
{
    /// <summary>
    /// NGワードチェッククラス
    /// </summary>
    public class CheakNGWord
    {
        /// <summary>
        /// 文字列変換クラス
        /// </summary>
        readonly ChagneCharacters charaClass = new ChagneCharacters();
        /// <summary>
        /// NGワード情報
        /// </summary>
        NGWordInfo ngWordInfo = null;

        /// <summary>
        /// 入力された文字列がNGワードにあるかをチェックします
        /// </summary>
        /// <param name="inputCharacter">入力文字列</param>
        public bool NGWrodCheaker(string inputCharacter)
        {
            if (ngWordInfo == null) ngWordInfo = new NGWordInfo();

            //文字タイプを取得
            var charaType = charaClass.AnalysisType(inputCharacter);

            //入力したタイプに同じ文字列があるか(ひらがな、カタカナ、ローマ字、それ以外)
            if (charaType == ChagneCharacters.CharaType.Japasece ||
                charaType == ChagneCharacters.CharaType.ZenKatakana ||
                charaType == ChagneCharacters.CharaType.English ||
                charaType == ChagneCharacters.CharaType.Else)
            {
                if (SameCharacterCheaker(inputCharacter, (int)charaType)) return true;
            }

            //同じ文字列の長さの文字を取得
            List<string> NGWords = new List<string>();

            switch (charaType)
            {
                //ひらがな、半角カタカナ、全角カタカナ
                case ChagneCharacters.CharaType.Japasece:
                case ChagneCharacters.CharaType.ZenKatakana:
                case ChagneCharacters.CharaType.HanKatakana:

                    //検索方法//
                    //--> 全探索で同じ文字の長さを取得・文字番号を比較
                    //--> マッチしていれば検査開始

                    GetSameLength(inputCharacter, ref NGWords);

                    break;

                //ローマ字
                case ChagneCharacters.CharaType.English:

                    //検索方法//
                    //入力文字列の最初の一文字目とNGワード文字列の一文字目を比較 --> マッチしていれば検査開始
                    GetSameChar(inputCharacter, ref NGWords);
                    break;
            }

            //無ければfalseを返します
            if (NGWords.Count == 0) return false;

            //NGワード検索開始
            foreach (var NGWord in NGWords)
            {
                if (charaClass.NGJudgement(inputCharacter, NGWord)) return true;
            }

            return false;
        }

        /// <summary>
        /// 同じ文字列があればチェック
        /// </summary>
        /// <param name="inputCharacter">入力された文字列</param>
        bool SameCharacterCheaker(string inputCharacter, int charaType)
        {
            for (int NGtype = 0; NGtype < ngWordInfo.typeCount; NGtype++)
            {
                //NG表現配列
                var ngWordType = ngWordInfo.GetNGWord(NGtype);

                //入力されたタイプと同じ文字タイプ
                var NGWords = ngWordType.GetWord(charaType);

                foreach (var NGWord in NGWords)
                {
                    //文字が同じかどうかを判定
                    if (Regex.IsMatch(inputCharacter, NGWord, RegexOptions.IgnoreCase)) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 入力された文字列の長さと同じ文字列をJsonから取得
        /// </summary>
        void GetSameLength(string inputCharacter, ref List<string> characters)
        {
            var length = inputCharacter.Length;
            for (int NGtype = 0; NGtype < ngWordInfo.typeCount; NGtype++)
            {
                //NG表現Json配列
                var ngWordType = ngWordInfo.GetNGWord(NGtype);

                for (int wordType = 0; wordType < ngWordType.characters.Length - 2; wordType++)
                {
                    //入力されたタイプを出力
                    var NGWords = ngWordType.GetWord(wordType);

                    foreach (var NGWord in NGWords)
                    {
                        //文字列の長さを比較
                        if (length == NGWord.Length &&
                            //入力文字列の最初の一文字目とNGワード文字列の一文字目を比較
                            charaClass.CheakCharacterNo(inputCharacter, NGWord))
                        {
                            characters.Add(NGWord);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 入力された文字列とNGワードの文字列の一文字目を比較
        /// </summary>
        void GetSameChar(string inputCharacter, ref List<string> characters)
        {
            for (int NGtype = 0; NGtype < ngWordInfo.typeCount; NGtype++)
            {
                //NG表現Json配列
                var ngWordType = ngWordInfo.GetNGWord(NGtype);

                for (int wordType = 0; wordType < ngWordType.characters.Length - 1; wordType++)
                {
                    //入力されたタイプを出力
                    var NGWords = ngWordType.GetWord(wordType);

                    foreach (var NGWord in NGWords)
                    {
                        if (charaClass.CheakSameChar(inputCharacter, NGWord)) characters.Add(NGWord);
                    }
                }
            }
        }
    }
}