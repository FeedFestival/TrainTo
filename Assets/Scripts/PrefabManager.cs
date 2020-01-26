using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    private static PrefabManager _instance;
    public static PrefabManager Instance(){
        return _instance;
    }

    public RectTransform PanelsParent;

    public GameObject AuthorAct;
    public GameObject HelloAct_1;

    public Dictionary<string, GameObject> Prefabs;

    void Awake() {
        _instance = this;
        foreach (Transform child in PanelsParent.transform) {
            GameObject.Destroy(child.gameObject);
        }
        InitActObjects();
    }

    private void InitActObjects() {
        Prefabs = new Dictionary<string, GameObject>() {
            { "AuthorAct", AuthorAct },
            { "HelloAct_1", HelloAct_1}
        };
    }

    public IAct GetActObject(string actName) {
        GameObject go = null;
        if (Prefabs.ContainsKey(actName)) {
            go = GameHiddenOptions.Instance.GetAnInstantiated(Prefabs[actName]);
        }

        go.name = actName;
        go.transform.SetParent(PanelsParent, false);
        go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        return go.GetComponent<IAct>();
    }
}
