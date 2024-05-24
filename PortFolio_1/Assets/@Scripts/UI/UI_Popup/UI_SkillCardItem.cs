using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillCardItem : UI_Base
{
    // 어떤 스킬?
    // 몇 레벨?
    // 데이터시트?
    int _templateID;
    Data.SkillData _skillData;
    string _skillName;
    int _level;

    enum Images
    {
        SkillImage,
        StarOn_0,
        StarOn_1,
        StarOn_2,
        StarOn_3,
        StarOn_4,
    }

    enum Texts
    {
        SkillDescriptionText,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindTMPText(typeof(Texts));
        GetImage((int)Images.StarOn_0).gameObject.SetActive(false);
        GetImage((int)Images.StarOn_1).gameObject.SetActive(false);
        GetImage((int)Images.StarOn_2).gameObject.SetActive(false);
        GetImage((int)Images.StarOn_3).gameObject.SetActive(false);
        GetImage((int)Images.StarOn_4).gameObject.SetActive(false);

        return true;
    }

    public void ChangeSprite(int type, string sprite)
    {
        Sprite sp = FindSprite(sprite);
        GetImage(type).gameObject.GetOrAddComponent<Image>().sprite = sp;
    }

    private Sprite FindSprite(string sprite)
    {
        Sprite go = Managers.Resource.Load<Sprite>(sprite);
        return go;
    }

    public void SetInfo(int templateID)
    {
        _templateID = templateID;

        Managers.Data.SkillDic.TryGetValue(templateID, out _skillData);
        _skillName = _skillData.name;
        _templateID = _skillData.templateID;
    }

    public void OnClickItem()
    {
        //스킬 업그레이드
        _level++;
        switch (_level)
        {
            case 1:
                GetImage((int)Images.StarOn_0).gameObject.SetActive(true);
                break;
            case 2:
                GetImage((int)Images.StarOn_1).gameObject.SetActive(true);
                break;
            case 3:
                GetImage((int)Images.StarOn_2).gameObject.SetActive(true);
                break;
            case 4:
                GetImage((int)Images.StarOn_3).gameObject.SetActive(true);
                break;
            case 5:
                GetImage((int)Images.StarOn_4).gameObject.SetActive(true);
                break;
        }

        Managers.Game.Player.SkillLevelUp(_templateID, _level, _skillName);
        
        Managers.UI.ClosePopup();
    }
}
