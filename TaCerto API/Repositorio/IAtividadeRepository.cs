using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTaCerto.Models;

namespace ApiTaCerto.Repositorio
{
    public interface IAtividadeRepository
    {
        IEnumerable<Disciplina> GetAll();

        IEnumerable<Disciplina> GetAllSubjectsWithId(int id);

        int GetDefaultInstituteId(string cnpj);

        Disciplina Find(long id);

        IEnumerable<AtividadeDisciplina> GetAllClassActivities(long idDisciplina);

        IEnumerable<Atividade> GetAllActivities(long idDisciplinaAutor);
    
        IEnumerable<Questao> GetAllQuestions(long idAtividade);
        
        int GetAttemptsNumber(long idAtividade);

        IEnumerable<Disciplina> GetAllClassSubjects(long idTurma);

        Task<int> SaveAtividadeResposta(AtividadeRespostaAluno atividadeRespostaAluno);

        Task SaveQuestaoResposta(List<QuestaoRespostaAluno> questaoRespostaAluno);

        IEnumerable<AtividadeAluno> GetNumeroDeAtividadesFeitas(long idaluno);

        Task AddAtividadeAluno(AtividadeAluno atividadeAluno);

        Task UpdateAtividadeAluno(AtividadeAluno atividadeAluno);

        int GetAttemptsNumberFromId(long idAtividadeAluno);
    }
}