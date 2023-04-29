using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


[CustomEditor(typeof(PlayerStateMachine))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerStateMachine psm = (PlayerStateMachine)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (psm.stateMachine == null) return;

        if (psm.stateMachine.CurrentState != null)
            EditorGUILayout.LabelField("Current State: ", psm.stateMachine.CurrentState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Avaliable States");

        if (showFoldout)
        {
            if (psm.stateMachine.dictionaryState != null)
            {
                var keys = psm.stateMachine.dictionaryState.Keys.ToArray();
                var vals = psm.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                }
            }
        }
    }
}
