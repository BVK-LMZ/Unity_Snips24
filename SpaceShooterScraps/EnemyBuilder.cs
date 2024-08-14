using UnityEngine;
using UnityEngine.Splines;
using UnityEditor.Rendering.Utilities;
using UnityEngine.InputSystem.Utilities;


    public class EnemyBuilder
    {

        //Variables to build with 
        GameObject _theTangoPrefab;
        SplineContainer _theSpline;
        GameObject _theWeaponPrefab;
        float _theTangospeed;
        

        
        //Functions to build with
        public EnemyBuilder SetBasePrefab(GameObject prefab)
        {
            _theTangoPrefab = prefab;
            return this;
        }

        public EnemyBuilder SetSpline(SplineContainer spline)
        {
            this._theSpline = spline;
            return this;
        }

        public EnemyBuilder SetWeaponPrefab(GameObject prefab)
        {
            _theWeaponPrefab = prefab;
            return this;
        }

        public EnemyBuilder SetSpeed(float speed)
        {   
            this._theTangospeed = speed;

            if (speed < 0 ) { speed = 1; }
            return this;
        }

    public GameObject Build()
    {
        if (_theTangoPrefab == null)
        {
            Debug.LogError("The Tango Prefab is not assigned.");
            return null;
        }

        GameObject instance = GameObject.Instantiate(_theTangoPrefab);

        SplineAnimate splineAnimate = instance.GetComponent<SplineAnimate>();
        if (splineAnimate == null)
        {
            Debug.LogWarning("SplineAnimate component missing on prefab. Adding a new one.");
            splineAnimate = instance.AddComponent<SplineAnimate>();
        }

        splineAnimate.Container = _theSpline;
        splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
        splineAnimate.ObjectUpAxis = SplineAnimate.AlignAxis.ZAxis;
        splineAnimate.ObjectForwardAxis = SplineAnimate.AlignAxis.YAxis;
        splineAnimate.MaxSpeed = _theTangospeed;

        // Debug logs to confirm settings
        Debug.Log($"Setting MaxSpeed to: {_theTangospeed}");
        Debug.Log($"SplineAnimate MaxSpeed set to: {splineAnimate.MaxSpeed}");
        Debug.Log($"Spline Animate is: {splineAnimate}");

        instance.transform.position = _theSpline.EvaluatePosition(0f);

        return instance;
    }




}