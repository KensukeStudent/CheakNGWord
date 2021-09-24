using System;
using UnityEngine;

namespace NGCheaker
{
    /// <summary>
    /// シナリオを読み込むためのクラス
    /// </summary>
    public class NGWordInfo
    {
        /// <summary>
        /// NGワード表現タイプ
        /// </summary>
        public readonly int typeCount = 3;

        NGDiscriminatory discriminatoryList = new NGDiscriminatory();
        NGSexual sexualList = new NGSexual();
        NGDanger dangerList = new NGDanger();

        NGWordJson[] ngWordJsons = null;

        public NGWordInfo()
        {
            Load();

            NGWordJson[] ngWordJsons =
            {
              discriminatoryList.discriminatory[0],
              sexualList.sexual[0],
              dangerList.danger[0]
            };

            this.ngWordJsons = ngWordJsons;

            for (int i = 0; i < this.ngWordJsons.Length; i++)
            {
                ngWordJsons[i].SetCharacaterArray();
            }
        }

        /// <summary>
        /// NGWordの読み込み
        /// </summary>
        void Load()
        {
            try
            {
                var json = Resources.Load<TextAsset>("Json/NGWord").text;

                if(!string.IsNullOrEmpty(json))
                {
                    discriminatoryList = JsonUtility.FromJson<NGDiscriminatory>(json);
                    sexualList = JsonUtility.FromJson<NGSexual>(json);
                    dangerList = JsonUtility.FromJson<NGDanger>(json);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("データはありません。");
            }
        }

        /// <summary>
        /// 指定のタイプのNGWord配列を返します
        /// </summary>
        /// <param name="ngWrodType">NGType</param>
        public NGWordJson GetNGWord(int ngWrodType)
        {
            return ngWordJsons[ngWrodType];
        }
    }

}