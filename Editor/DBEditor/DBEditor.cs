using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DBEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    DBActorsView actorsView;
    DBItemsView itemsView;
    VisualElement root;
    public enum Tabs
    {
        ACTOR, ITEM, QUEST, SKILL, EVENT
    }


    [MenuItem("Window/UI Toolkit/DBEditor")]
    public static void ShowExample()
    {
        DBEditor wnd = GetWindow<DBEditor>();
        wnd.titleContent = new GUIContent("DBEditor");
    }

    public void CreateGUI()
    {
        actorsView = new DBActorsView();
        // Each editor window contains a root VisualElement object
        root = rootVisualElement;
        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        Button actorsButton = root.Q<Button>("ActorsTab");
        actorsButton.clickable.clicked += () => SwitchTab(Tabs.ACTOR);

        Button itemsButton = root.Q<Button>("ItemsTab");
        itemsButton.clickable.clicked += () => SwitchTab(Tabs.ITEM);

        Button questsButton = root.Q<Button>("QuestsTab");
        questsButton.clickable.clicked += () => SwitchTab(Tabs.QUEST);

        Button skillsButton = root.Q<Button>("SkillsTab");
        skillsButton.clickable.clicked += () => SwitchTab(Tabs.SKILL);

        Button eventsButton = root.Q<Button>("QuestsTab");
        eventsButton.clickable.clicked += () => SwitchTab(Tabs.EVENT);

        SwitchTab(Tabs.ACTOR);
    }
    public void SwitchTab(Tabs tab)
    {
        VisualElement mainContent = root.Q<VisualElement>("MainContent");
        switch (tab)
        {
            case Tabs.ACTOR:
                mainContent.Clear();
                actorsView.Init(mainContent);
                break;
            case Tabs.EVENT:
                break;
            case Tabs.ITEM:
                mainContent.Clear();
                itemsView.Init(mainContent);
                break;
            case Tabs.QUEST:
                break;
            case Tabs.SKILL:
                break;

        }
    }
}
