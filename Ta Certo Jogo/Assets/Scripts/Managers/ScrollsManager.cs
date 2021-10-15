using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollsManager : MonoBehaviour
{
    [Header("Subjects elements")]
    public ScrollRect subjectsScrollView;
    public GameObject subjectsScrollContent;
    public GameObject subjectPanelPrefab;
    private List<Disciplina> subjectsList { get; set; }

    [Header("Activities elements")]
    public ScrollRect activitiesScrollView;
    public GameObject avaliativeActivitiesScrollContent;
    public GameObject taskActivitiesScrollContent;
    public GameObject avaliativeActivityPanelPrefab;
    public GameObject taskActivityPanelPrefab;
    public GameObject avisoPanel;

    private List<Atividade> activitiesList { get; set; }

    [Header("Subjects Class elements")]
    public ScrollRect subjectsClassScrollView;
    public GameObject subjectsClassScrollContent;

    public void CreateSubjectList(List<Disciplina> disciplinas)
    {
        ClearChildren(avaliativeActivitiesScrollContent.transform);
        subjectsList = disciplinas;

        foreach (Disciplina d in subjectsList)
        {
            GenerateSubjectItem(d);
        }
    }

    void GenerateSubjectItem(Disciplina disciplina)
    {
        GameObject scrollItem = Instantiate(subjectPanelPrefab);
        SubjectPanel prefabScript = scrollItem.GetComponent<SubjectPanel>();
        prefabScript.setSubject(disciplina, true);
        scrollItem.transform.SetParent(subjectsScrollContent.transform, false);
    }

    public void CreateClassSubjectList(List<Disciplina> disciplinas)
    {
        ClearChildren(subjectsClassScrollContent.transform);
        subjectsList = disciplinas;

        foreach (Disciplina d in subjectsList)
        {
            GenerateClassSubjectItem(d);
        }
    }

    void GenerateClassSubjectItem(Disciplina disciplina)
    {
        GameObject scrollItem = Instantiate(subjectPanelPrefab);
        SubjectPanel prefabScript = scrollItem.GetComponent<SubjectPanel>();
        prefabScript.setSubject(disciplina, false);
        scrollItem.transform.SetParent(subjectsClassScrollContent.transform, false);
    }

    // Aqui
    public void CreateActivityList(List<Atividade> atividades)
    {
        ClearChildren(avaliativeActivitiesScrollContent.transform);
        ClearChildren(taskActivitiesScrollContent.transform);
        activitiesList = atividades;
        foreach (Atividade a in activitiesList)
        {
            GenerateActivityItem(a);
        }

        StartCoroutine(CheckAlreadyDone());

    }

    IEnumerator CheckAlreadyDone()
    {
        yield return new WaitForSeconds(.5f);

        if (avaliativeActivitiesScrollContent.transform.childCount == 0)
            ShowHasNoActivities(true);

        if (taskActivitiesScrollContent.transform.childCount == 0)
            ShowHasNoActivities(false);
    }
    private void ShowHasNoActivities(bool avaliative)
    {
        GameObject textoDeAviso = Instantiate(avisoPanel);

        if (avaliative) {
            textoDeAviso.GetComponent<AvisoPanel>().SetTextoAviso("Você não possui nenhuma atividade avaliativa");
            textoDeAviso.transform.SetParent(avaliativeActivitiesScrollContent.transform, false);
        }
        else {
            textoDeAviso.GetComponent<AvisoPanel>().SetTextoAviso("Você não possui nenhuma tarefa");
            textoDeAviso.transform.SetParent(taskActivitiesScrollContent.transform, false);
        }
    }

    public void ShowHasNoSubjecties()
    {
        GameObject textoDeAviso = Instantiate(avisoPanel);

        textoDeAviso.GetComponent<AvisoPanel>().SetTextoAviso("Você não possui nenhuma disciplina");
        textoDeAviso.transform.SetParent(subjectsClassScrollContent.transform, false);
    }

    void GenerateActivityItem(Atividade atividade)
    {
        GameObject activityScrollItem = Instantiate(avaliativeActivityPanelPrefab);

        ActivityButton prefabScript = activityScrollItem.GetComponent<ActivityButton>();
        prefabScript.setActivity(atividade);

        /*if (atividade.numeroTentativas == atividade.numeroTentativasAtuais)
            prefabScript.avoidClickToButton();
        */
        if (atividade.isProva)
            activityScrollItem.transform.SetParent(avaliativeActivitiesScrollContent.transform, false);
        else
            activityScrollItem.transform.SetParent(taskActivitiesScrollContent.transform, false);

    }


    public void ClearActivities()
    {
        ClearChildren(subjectsClassScrollContent.transform);
        ClearChildren(avaliativeActivitiesScrollContent.transform);
        ClearChildren(taskActivitiesScrollContent.transform);
    }

    public void ClearChildren(Transform transform)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}