using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubjectPanel : MonoBehaviour
{
    private Disciplina subject;
    private List<Atividade> activities;
    public Color panelColor;
    public Image subjectButtonImage;
    public TextMeshProUGUI subjectNameText;
    public Image iconImage;
    public bool returnToDefault;

    public void setSubject(Disciplina disc, bool _whereToReturn)
    {
        subject = disc;
        returnToDefault = _whereToReturn;
        setNameText();
        setButtonColor();
    }

    private void setNameText()
    {
        subjectNameText.text = subject.nome;
    }

    private void setButtonColor()
    {
        float? r = subject.corR / 255f;
        float? g = subject.corG / 255f;
        float? b = subject.corB / 255f;
        float? a = subject.corA;

        subjectButtonImage.color = new Color(r.GetValueOrDefault(.404f), g.GetValueOrDefault(.643f), b.GetValueOrDefault(1f), a.GetValueOrDefault(1f));
    }

    public void openActivitiesScreen()
    {
        GameManager.Instance.SetDisciplina(subject.nome);
        GameManager.Instance.SetIdDisciplina(subject.idDisciplina);
        GameManager.Instance.SetActivitiesTitleText(subject.nome);
        string whereToReturn = "disciplinasCurso";
        if(subject.isDefault)
            whereToReturn = "Atividade";

        GameManager.Instance.loadManager.loadActivitiesOfSubject(subject.atividades, whereToReturn);
    }
}