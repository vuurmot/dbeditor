using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DBItemsView
{
    List<ActorSO> items = new List<ActorSO>();
    ListView actorListView;
    public void Init(VisualElement mainContent)
    {
        //load template
        VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/ActorContent.uxml");
        VisualElement ui = uiAsset.Instantiate();
        mainContent.Add(ui);
        ui.StretchToParentSize();

        FindAssets();

        actorListView = mainContent.Q<ListView>("ActorList");
        InitList(actorListView);

        Button createButton = mainContent.Q<Button>("CreateNew");
        createButton.clickable.clicked += () => AddNewItem();
    }
    public void AddNewItem()
    {
        ActorSO asset = ScriptableObject.CreateInstance<ActorSO>();
        int id = items.Count + 1;
        AssetDatabase.CreateAsset(asset, $"Assets/_Root/Database/Data/Actors/{id}.asset");
        AssetDatabase.SaveAssets();

        FindAssets();
        actorListView.Rebuild();
    }
    private void FindAssets()
    {
        string[] actorAssets = AssetDatabase.FindAssets("", new[] { "Assets/_Root/Database/Data/Actors/" });
        List<ActorSO> actorList = new List<ActorSO>();
        foreach (string assetName in actorAssets)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetName);
            ActorSO actor = AssetDatabase.LoadAssetAtPath<ActorSO>(assetPath);
            actorList.Add(actor);
        }
        items = actorList;
    }
    private void InitList(ListView actorListView)
    {
        // The "makeItem" function is called when the
        // ListView needs more items to render.
        Func<VisualElement> makeItem = () => new Label();

        // As the user scrolls through the list, the ListView object
        // recycles elements created by the "makeItem" function,
        // and invoke the "bindItem" callback to associate
        // the element with the matching data item (specified as an index in the list).
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i].Name;

        actorListView.itemsSource = items;
        actorListView.fixedItemHeight = 16;
        actorListView.makeItem = makeItem;
        actorListView.bindItem = bindItem;
        actorListView.selectionType = SelectionType.Single;
        actorListView.selectionChanged += objects => OnItemChosen(objects.First());

        actorListView.style.flexGrow = 1.0f;
    }
    public void OnItemChosen(object item)
    {
        Debug.Log(item);
    }
    private void InitSettings()
    {

    }
}
