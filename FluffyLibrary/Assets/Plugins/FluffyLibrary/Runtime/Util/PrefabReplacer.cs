using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FluffyLibrary.Util
{
#if UNITY_EDITOR
    public class PrefabReplacer : EditorWindow
    {
        public GameObject NewPrefab;

        private void OnGUI()
        {
            GUILayout.Label("Replace Prefab Settings", EditorStyles.boldLabel);
            NewPrefab = (GameObject)EditorGUILayout.ObjectField(NewPrefab, typeof(GameObject), true);

            if (GUILayout.Button("Replace Prefabs")) ReplacePrefabs();
        }

        [MenuItem("Tools/Prefab Replacer")]
        private static void Init()
        {
            var window = (PrefabReplacer)GetWindow(typeof(PrefabReplacer));
            window.Show();
        }

        public void ReplacePrefabs()
        {
            if (NewPrefab != null)
            {
                var newObjs = new List<GameObject>();

                foreach (var transform in Selection.transforms)
                {
                    var newObject = (GameObject)PrefabUtility.InstantiatePrefab(NewPrefab);
                    Undo.RegisterCreatedObjectUndo(newObject, "created prefab");

                    newObject.transform.position = transform.position;
                    newObject.transform.rotation = transform.rotation;
                    newObject.transform.localScale = transform.localScale;
                    newObject.transform.parent = transform.parent;
                    newObjs.Add(newObject);

                    Undo.DestroyObjectImmediate(transform.gameObject);
                }

                Selection.objects = newObjs.ToArray();
            }
        }
    }
#endif
}