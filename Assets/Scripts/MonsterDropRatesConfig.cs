using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewMonsterDropRateConfig",menuName = "NuevaConfiguracionDeDropRate")]
public class MonsterDropRatesConfig : ScriptableObject
{
    public List<MonsterDropRate> DropRatesInfo = new List<MonsterDropRate>();
}
