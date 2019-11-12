using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewTiersConfig",menuName = "NuevaConfiguracionDeTiers")]
public class TiersInfoConfig : ScriptableObject
{
    public List<Tierinfo> TiersInfo = new List<Tierinfo>();
}
