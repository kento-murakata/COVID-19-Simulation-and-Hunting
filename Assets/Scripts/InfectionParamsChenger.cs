using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class InfectionParamsChenger : MonoBehaviour
{
    [SerializeField]
    private float toInfectionNegativeTime;
    [SerializeField]
    private float toInfectionPositiveTime;
    [SerializeField]
    private float toOnsetAndQuarantineTime;
    [SerializeField]
    private float toNegativeTime;

    private Infection infection;


    public void PublicMethod()
    {
        GameObject[] array  = GameObject.FindGameObjectsWithTag("Human");

        var count = array.Length;

        foreach(GameObject humanObject in array)
        {
            infection = humanObject.GetComponent<Infection>();
            infection.toInfectionNegativeTime = this.toInfectionNegativeTime;
            infection.toInfectionPositiveTime = this.toInfectionPositiveTime;
            infection.toOnsetAndQuarantineTime = this.toOnsetAndQuarantineTime;
            infection.toNegativeTime = this.toNegativeTime;
        }
    }


}

#if UNITY_EDITOR
[CustomEditor(typeof(InfectionParamsChenger))]
public class ExampleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        InfectionParamsChenger t = target as InfectionParamsChenger;

        if (GUILayout.Button("Infection Cpmponent Params àÍäáèëçûÇ›"))
        {
            t.PublicMethod();
        }

    }
}
#endif