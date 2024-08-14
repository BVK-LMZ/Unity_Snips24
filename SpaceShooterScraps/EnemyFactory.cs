using UnityEngine;
using UnityEngine.Splines;


    public class EnemyFactory
    {
        public GameObject CreateEnemy(EnemyTypeSO theEnemyType, SplineContainer theSpline)
        {
            EnemyBuilder builder = new EnemyBuilder() // the builder new instance
                .SetBasePrefab(theEnemyType._theTangoPrefab)
                .SetSpline(theSpline)
                .SetSpeed(theEnemyType._theShipSpeed);
            return builder.Build(); //return a  game object from the factory
        }


}