using UnityEngine;

namespace GDRTest.Config
{
    [CreateAssetMenu(fileName = "Config")]
    public class GameConfig : ScriptableObject
    {
        public int NumOfMoney;
        public int NumOfSpikes;
    }
}
