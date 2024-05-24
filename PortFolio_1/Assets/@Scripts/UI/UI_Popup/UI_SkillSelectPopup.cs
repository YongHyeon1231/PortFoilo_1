using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillSelectPopup : UI_Base
{
    [SerializeField]
    Transform _grid;

    List<UI_SkillCardItem> _items = new List<UI_SkillCardItem>();

    private void Start()
    {
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        foreach (Transform t in _grid.transform)
        {
            Managers.Resource.Destroy(t.gameObject);
        }

        #region EgoSword
        var go = Managers.Resource.Instantiate("UI_SkillCardItem.prefab", pooling: false);
        UI_SkillCardItem item = go.GetOrAddComponent<UI_SkillCardItem>();
        item.Init();
        item.SetInfo(100);
        item.ChangeSprite(0, "EgoSwordIcon_01.sprite");
        item.transform.SetParent(_grid.transform);
        _items.Add(item);
        #endregion

        #region FireBall
        var go1 = Managers.Resource.Instantiate("UI_SkillCardItem.prefab", pooling: false);
        UI_SkillCardItem item1 = go1.GetOrAddComponent<UI_SkillCardItem>();
        item1.Init();
        item1.SetInfo(200);
        item1.ChangeSprite(0, "FireballIcon_01.sprite");
        item1.transform.SetParent(_grid.transform);
        _items.Add(item1);
        #endregion

        /*#region Temp
        var go2 = Managers.Resource.Instantiate("UI_SkillCardItem.prefab", pooling: false);
        UI_SkillCardItem item2 = go2.GetOrAddComponent<UI_SkillCardItem>();
        item2.Init();
        item2.SetInfo(200);
        item2.ChangeSprite(0, "FireballIcon_01.sprite");
        item2.transform.SetParent(_grid.transform);

        _items.Add(item2);
        #endregion*/
    }
}
