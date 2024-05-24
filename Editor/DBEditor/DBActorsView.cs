using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DBActorsView
{
    List<ActorSO> items = new List<ActorSO>();
    ListView actorListView;
    VisualElement container;
    public void Init(VisualElement mainContent)
    {
        container = mainContent;
        //load template
        VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/DBEditor/ActorContent.uxml");
        VisualElement ui = uiAsset.Instantiate();
        container.Add(ui);
        ui.StretchToParentSize();

        FindAssets();

        actorListView = container.Q<ListView>("ActorList");
        InitList(actorListView);

        Button createButton = container.Q<Button>("CreateNew");
        createButton.clickable.clicked += () => AddNewItem();
    }
    public void AddNewItem()
    {
        ActorSO asset = ScriptableObject.CreateInstance<ActorSO>();
        string guid = Guid.NewGuid().ToString();
        asset.GUID = guid;
        asset.DateCreated = DateTime.Now.ToString();
        AssetDatabase.CreateAsset(asset, $"Assets/_Root/Database/Data/Actors/{guid}.asset");
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
        Debug.Log(actorList.Count);
        // actorList.OrderByDescending(actor =>
        // {
        //     return DateTime.Parse(actor.DateCreated);
        // });
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
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i].GUID;

        actorListView.itemsSource = items;
        actorListView.fixedItemHeight = 16;
        actorListView.makeItem = makeItem;
        actorListView.bindItem = bindItem;
        actorListView.selectionType = SelectionType.Single;
        actorListView.selectionChanged += objects => InitSettings(objects.First());

        actorListView.style.flexGrow = 1.0f;
    }
    public void InitSettings(object item)
    {
        ActorSO actor = item as ActorSO;
        TextField actorName = container.Q<TextField>("NameField");
        actorName.value = actor.Name;
    }

}
