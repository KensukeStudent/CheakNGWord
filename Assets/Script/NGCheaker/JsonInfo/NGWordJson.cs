using System.Collections;
using System.Collections.Generic;

namespace NGCheaker
{
    [System.Serializable]
    /// <summary>
    /// NGワードJson
    /// </summary>
    public class NGWordJson
    {
        /// <summary>
        /// 文字タイプ格納変数
        /// </summary>
        public string[][] characters;

        //ひらがな
        public string[] japanese;
        //全角カタカナ
        public string[] zenKatakana;
        //半角カタカナ
        public string[] hanKatakana;
        //ローマ字
        public string[] english;
        //それ以外の文字
        public string[] other;

        public void SetCharacaterArray()
        {
            string[][] characters =
            {
              japanese,
              zenKatakana,
              hanKatakana,
              english,
              other
            };

            this.characters = characters;
        }

        /// <summary>
        /// 指定のタイプのcharaters配列を返します
        /// </summary>
        /// <param name="wrodType">NGType</param>
        public string[] GetWord(int wrodType)
        {
            return characters[wrodType];
        }
    }

    /// <summary>
    /// 暴力的表現クラス
    /// </summary>
    [System.Serializable]
    public class NGDiscriminatory
    {
        public List<NGWordJson> discriminatory = new List<NGWordJson>();
    }

    /// <summary>
    /// 性的表現クラス
    /// </summary>
    [System.Serializable]
    public class NGSexual
    {
        public List<NGWordJson> sexual = new List<NGWordJson>();
    }

    /// <summary>
    /// 危険な表現
    /// </summary>
    [System.Serializable]
    public class NGDanger
    {
        public List<NGWordJson> danger = new List<NGWordJson>();
    }
}