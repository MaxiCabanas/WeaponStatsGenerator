using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGenerator : MonoBehaviour 
{

    //public int mlvl;
    //public int mtier; //0 = common, 1 = Elite, 2 = Boss

    [Header("Configuration Inputs")]
    public TiersInfoConfig TiersConfig;
    public MonsterDropRatesConfig MonsterDropRatesConfig;
    [Header("Info Outputs:")]
    public Text tierOutput;
    public Text dmgOutput;
    public Text spdOutput;
    public Text mlvlOutput;
    public Text mtierOutput;
    private float dmg;
    private float spd;
    private int mlvl;
    private int mtier;
    private int wtier;

    private List<MonsterDropRate> DropRates;
    private Vector2[] WeaponTiers = new Vector2[4];
    //private string[] MonsterTiers = new string[3];

    private List<Tierinfo> Tiers;

    private void InicializarWeaponRates()
    {
        int num = 25;
        for (int i=0;i<4; i++)
        {
            WeaponTiers[i] = new Vector2(num * i+1, num*(i+1));
        }
    }
    private int FindWeaponTier(int mtier)
    {
        //Numero aleatorio entre 1 y 100.
        int value = Random.Range(1, 101);
        //Este numero va de 0 a la cantidad de Weapon Tiers.
        int j = 0;
        while (value>DropRates[mtier].DropRate[j])
        {
            value -= DropRates[mtier].DropRate[j];
            j++;
        }
        if (j < 4)
            return j;
        else
        {
            Debug.Log("ERROR AL ENCONTRAR EL TIER DEL ARMA ");
            return -1;
        }
    }

    //x = mlvl
    //a = ((x^2)/(10+(x*0.30))
    //base = Random de acuerdo al wtier
    //Valorfinal = base+a
    /// <summary>
    /// Este metodo aplica la formula para la generacion de los stats del arma.
    /// </summary>
    /// <param name="rango"></param>
    /// <returns></returns>
    private int CalculoFormula(Vector2 rango)
    {
        int Base = (int)Random.Range(rango.x, rango.y);
        Debug.Log("Daño Base = "+Base);
        int a = (int)(Mathf.Pow(mlvl, 2) / (10 + (mlvl * 0.3)));
        Debug.Log("Daño Bonus = " + a);

        return Base + a;
    }

/// <summary>
/// Para implementar debe obtener mlvl y mtier por parametros.
/// </summary>
    public void Generate()
    {
        //Obtengo un nivel de monstruo aleatorio
        mlvl = Random.Range(1, 101);
        //Obtengo un Tier de monstruo aleatorio: 0= comun, 1=Elite, 2=Boss
        mtier = Random.Range(0, 3);
        //Obtengo el tier del arma a dropear en base al tier del monstruo
        wtier = FindWeaponTier(mtier);

        Vector2 BaseRango = WeaponTiers[wtier];

        dmg = CalculoFormula(BaseRango);
        spd = CalculoFormula(BaseRango);

        MostrarValores();
    }

    void MostrarValores()
    {
        mtierOutput.text = DropRates[mtier].name;
        mlvlOutput.text = "Nivel "+mlvl.ToString();

        tierOutput.text = Tiers[wtier].name;
        tierOutput.color = Tiers[wtier].color;

        dmgOutput.text = "+ " + dmg;
        spdOutput.text = "+ " + spd;
    }

    void Awake()
    {
        Tiers = TiersConfig.TiersInfo;
        DropRates = MonsterDropRatesConfig.DropRatesInfo;
    }
    void Start()
    {
        InicializarWeaponRates();

        Generate();
    }
}
