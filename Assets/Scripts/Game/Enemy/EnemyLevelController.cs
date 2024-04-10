using System.Linq;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Singletons;
using UnityEngine;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyLevelController : Singleton<EnemyLevelController>
    {
        private EnemyController[] enemies;

        private void Awake()
        {
            enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
        }

        public void CheckEnemies()
        {
            if (enemies.All(e => e.Dead))
            {
                PlayerPrefsManager.Instance.Save();
                LevelManager.Instance.LoadNextLevel(3);
            }
        }
    }
}
