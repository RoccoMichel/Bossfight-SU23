using UnityEngine;

public class Boss : Entity
{
    [Header("Boss Attributes")]
    public int currentStage = 1;
    public StagedGameObjects[] StagedReferences;

    [System.Serializable]
    public struct StagedGameObjects
    {
        public GameObject[] stagedObjects;
    }

    public void UpdateStage(int stage)
    {
        foreach (StagedGameObjects allObjects in StagedReferences)
        {
            foreach (GameObject go in allObjects.stagedObjects) go.SetActive(false);
            if (allObjects.stagedObjects.Length > stage) allObjects.stagedObjects[stage].SetActive(true);
        }
    }

    public override void Die()
    {
        print("You win!!!");
    }
    protected override void OnReset()
    {
        identity = "Boss";
        maxHealth = 3;
        base.OnReset();
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);

        currentStage++;
        UpdateStage(currentStage);
    }
}
