using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestingAttacks
{
    int totalDamage = 0;
    GameObject player = new GameObject("player");
    
    [Test]
    public void TestSpawnHitbox() 
    {
        int damage = 0;
        SpawnHitbox();
        if(damage != 0)
        {
            Debug.Log("Passed)");
        }
        
        Debug.Log("Failed - SpawnHitbox does not deal damage to the enemy");

    }

    public void TestSpawnHigh() 
    {
        int damage = 0;
        SpawnHigh();
        if(damage != 0)
        {
            Debug.Log("Passed)");
        }
        
        Debug.Log("Failed - SpawnHitbox does not deal damage to the enemy");
    }

    public void TestSpawnLow() 
    {
        int damage = 0;
        SpawnLow();
        if(damage != 0)
        {
            Debug.Log("Passed)");
        }
        
        Debug.Log("Failed - SpawnHitbox does not deal damage to the enemy");
    }

}
