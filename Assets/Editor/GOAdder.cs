using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Container))]
public class GOAdder : Editor {

    public List<GameObject> m = new List<GameObject>();
    public string names;
    public int id;
    public int choice;
    public override void OnInspectorGUI()
    {
        Container spot = (Container)target;
        if (spot.enemy == null)
        {


            EditorGUILayout.ObjectField("GO to add", spot.enemy, typeof(GameObject));
            if (GUILayout.Button("Add"))
            {

                spot.enemiesL.Add(spot.enemy);
                spot.enemy = null;
            }
            EditorGUILayout.IntField("List size", spot.enemiesL.Count);
        }
    }
}
