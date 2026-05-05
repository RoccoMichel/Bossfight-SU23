using UnityEngine;
using System.Collections;

public class Boss : Entity
{
    [Header("Boss Attributes")]
    [SerializeField] private GameObject minion;
    [SerializeField] private Transform[] minionSpawnPoints;
    public int currentStage = 1;
    public StagedGameObjects[] StagedReferences;
    public GameObject[] enableOnDeath;

    [System.Serializable]
    public struct StagedGameObjects
    {
        public GameObject[] stagedObjects;
    }

    protected override void OnStart()
    {
        base.OnStart();
        UpdateStage(currentStage);
        StartCoroutine(Actions());
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
        print("Boss Defeated!");
        foreach (GameObject go in enableOnDeath)
        {
            go.SetActive(true);
        }
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

    private IEnumerator Actions()
    {
        do
        {
            int action = Random.Range(0, 2);
            switch (action)
            {
                case 0: // Spawn Minion
                    Transform spawnPoint = minionSpawnPoints[Random.Range(0, minionSpawnPoints.Length)];
                    Instantiate(minion, spawnPoint.position, Quaternion.identity);
                    break;

                case 1: // Spawn Area Attack
                    Instantiate(Resources.Load<GameObject>("Area Attack"));
                    break;
            }

            yield return new WaitForSeconds(10 / Mathf.Clamp(currentStage, 1, int.MaxValue) + 1);
        } while (Health > 0);

        yield break;
    }
}
