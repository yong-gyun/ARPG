using System.Collections.Generic;
using UnityEngine;

public interface IDungeon
{
    public HunterGroup GetHunterGroup();
    public void DungeonEnter();
    public void DungeonExit();    
}

public class Dungeon : MonoBehaviour
{
    
}
