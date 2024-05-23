using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameResultPopup : UI_Base
{
    #region UI ��� ����Ʈ
    // ���� ����
    // ResultStageValueText : �ش� �������� ��
    // ResultSurvivalTimeValueText : �������� Ŭ���� ���� �ɸ� �ð� ( mm:ss �� ǥ��)
    // ResultGoldValueText : �ױ��� ���� ���� ���
    // ResultKillValueText : �ױ��� ���� ų ��
    // ResultRewardScrollContentObject : : �������� ��Ե� �������� �� �θ� ��ü
    // (���, ����ġ, ������, ĳ���� ��ȭ�� ���� ��������)

    // ���ö���¡ �ؽ�Ʈ
    // GameResultPopupTitleText
    // ResultSurvivalTimeText
    // ConfirmButtonText
    #endregion
    
    enum GameObjects
    {
        ContentObject,
        ResultRewardScrollContentObject,
        ResultGoldObject,
        ResultKillObject,
    }

    enum Texts
    {
        GameResultPopupTitleText,
        ResultStageValueText,
        ResultSurvivalTimeText,
        ResultSurvivalTimeValueText,
        ResultGoldValueText,
        ResultKillValueText,
        ConfirmButtonText,
    }

    enum Buttons
    {
        StatisticsButton,
        ConfirmButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindTMPText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StatisticsButton).gameObject.BindEvent(OnClickStatisticsButton);
        GetButton((int)Buttons.ConfirmButton).gameObject.BindEvent(OnClickConfirmButton);

        RefreshUI();
        return true;
    }

    public void SetInfo()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        if (_init == false)
            return;

        // ���� ����
        GetTMPText((int)Texts.GameResultPopupTitleText).text = "Game Result";
        GetTMPText((int)Texts.ResultStageValueText).text = "4 STAGE";
        GetTMPText((int)Texts.ResultSurvivalTimeText).text = "Survival Time";
        GetTMPText((int)Texts.ResultSurvivalTimeValueText).text = "14:23";
        GetTMPText((int)Texts.ResultGoldValueText).text = "200";
        GetTMPText((int)Texts.ResultKillValueText).text = "100";
        GetTMPText((int)Texts.ConfirmButtonText).text = "OK";

    }

    void OnClickStatisticsButton()
    {
        Debug.Log("OnClickStatisticsButton");
    }

    void OnClickConfirmButton()
    {
        Debug.Log("OnClickConfirmButton");
    }
}
