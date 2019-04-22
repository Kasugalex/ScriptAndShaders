using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

public class ReflectTest : MonoBehaviour
{

    public enum WarehousePrestallOpenType
    {
        None = 0,
        Arena = 1,//竞技场攻击阵容
        Expedition = 2,//远征
        OneToOne = 3,//1V1
        FourKing = 4,//四大天王
        HuntingArena = 5,//狩猎场
        PokemonExperiment = 6,//精灵试炼
        ArenaAdjust = 7,//竞技场防守阵容
        GuildAdventure = 8,//公会冒险
        MiningDefence = 9, // 守矿
        MiningHelp = 10, //协防
        MiningAttack = 11, // 矿点掠夺
        FightAgain = 12,//重新战斗
        GuildFight = 13,//公会守阵
        KingTower = 14,//精灵爬塔
        PVPTwo = 15, //PVP双打
        GuildTrial = 16,//工会boss试炼
    }

    public WarehousePrestallOpenType openType = WarehousePrestallOpenType.None;

    public ShowTip tip;
    private void Start()
    {
        tip = new ShowTip();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            System.Diagnostics.Stopwatch wathcer1 = new System.Diagnostics.Stopwatch();
            wathcer1.Start();
            var e = new WarehousePrestallOpenType();
            var length = Enum.GetNames(e.GetType()).Length;

            int random = UnityEngine.Random.Range(0, length);

            openType = (WarehousePrestallOpenType)random;

            ReflectToDo(tip.GetType());
            wathcer1.Stop();

            print("反射一次用了:" + wathcer1.ElapsedMilliseconds + "毫秒");
        }
    }


    private void ReflectToDo(Type type)
    {
        /*var methodNames = from m in type.GetMethods() select m.Name;
        foreach (var item in methodNames)
        {
            print(item);
        }*/

        object obj = Activator.CreateInstance(type);

        string methodName = openType.ToString();

        MethodInfo info = type.GetMethod(methodName, new Type[]{ });

        var parameters = new object[] { };
        info.Invoke(obj, parameters);

        MethodInfo p = type.GetMethod("Print", new Type[] { });
        p.Invoke(obj, parameters);
    }


}

[Serializable]
public class ShowTip
{
    public string log = "";
    public void Arena()
    {
        log = "Arena";
    }

    public void Expedition()
    {
        log = "Expedition";
    }

    public void OneToOne()
    {
        log = "OneToOne";
    }

    public void FourKing()
    {
        log = "FourKing";
    }

    public void HuntingArena()
    {
        log = "HuntingArena";
    }

    public void PokemonExperiment()
    {
        log = "PokemonExperiment";
    }

    public void ArenaAdjust()
    {
        log = "ArenaAdjust";
    }

    public void GuildAdventure()
    {
        log = "GuildAdventure";
    }

    public void MiningDefence()
    {
        log = "MiningDefence";
    }

    public void MiningHelp()
    {
        log = "MiningHelp";
    }

    public void MiningAttack()
    {
        log = "MiningAttack";
    }

    public void FightAgain()
    {
        log = "FightAgain";
    }

    public void GuildFight()
    {
        log = "GuildFight";
    }

    public void KingTower()
    {
        log = "KingTower";
    }

    public void PVPTwo()
    {
        log = "PVPTwo";
    }

    public void GuildTrial()
    {
        log = "GuildTrial";
    }

    public void Print()
    {
        Debug.Log(log);
    }
}