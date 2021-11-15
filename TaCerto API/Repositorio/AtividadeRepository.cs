using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTaCerto.Models;
using ApiTaCerto.Models.Usuario;

namespace ApiTaCerto.Repositorio
{
    public class AtividadeRepository : IAtividadeRepository
    {

        private readonly MainDbContext _contexto;

        public AtividadeRepository(MainDbContext ctx)
        {
            _contexto = ctx;    
        }

        public Disciplina Find(long id)
        {
            return _contexto.Disciplina.FirstOrDefault(p => p.IdDisciplina == id);
        }

        public IEnumerable<Disciplina> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Disciplina> GetAllSubjectsWithId(int id)
        {
            return _contexto.Disciplina.Where(d => d.IdMatriz == id).ToList();
        }

        public int GetDefaultInstituteId(string cnpj)
        {
            Instituicao instituicao = _contexto.Instituicao.Where(i => i.CNPJ == cnpj).FirstOrDefault();
            if (instituicao != null)
                return instituicao.IdInstituicao;

            return 0;
        }

        public IEnumerable<AtividadeDisciplina> GetAllClassActivities(long idDisciplina){ 
            //return _contexto.AtividadeDisciplinas.Where(ad => ad.IdDisciplina == idDisciplina).ToList();
            return null;
        }

        public IEnumerable<Atividade> GetAllActivities(long idDisciplinaAutor){
            return _contexto.Atividade.Where(a => a.IdTurmaDisciplinaAutor == idDisciplinaAutor && a.NumeroQuestoes > 0).ToList();
        }

        public IEnumerable<Questao> GetAllQuestions(long idAtividade){
            return _contexto.Questao.Where(a => a.IdAtividade == idAtividade).ToList();
        }

        public int GetAttemptsNumber(long idAtividade){
            int maxAttempts = _contexto.Atividade.Where(a => a.IdAtividade == idAtividade).FirstOrDefault().NumeroTentativas;

            return maxAttempts;
        }

        public IEnumerable<Disciplina> GetAllClassSubjects(long idTurma){
            using(var contexto = _contexto){
                var innerJoin = from disc in contexto.Disciplina join disctur in contexto.DisciplinaTurma on disc.IdDisciplina equals 
                disctur.IdDisciplina where disctur.IdTurma == idTurma select disc;

                List<Disciplina> atividades = innerJoin.ToList();
                return atividades;
            }
        }

        public async Task<int> SaveAtividadeResposta(AtividadeRespostaAluno atividadeRespostaAluno)
        {

            await _contexto.AtividadeRespostaAluno.AddAsync(atividadeRespostaAluno);
            await _contexto.SaveChangesAsync();

            return atividadeRespostaAluno.IdAtividadeRespostaAluno;
        }

        public async Task SaveQuestaoResposta(List<QuestaoRespostaAluno> questaoRespostaAluno){
            await _contexto.QuestaoRespostaAluno.AddRangeAsync(questaoRespostaAluno);
            await _contexto.SaveChangesAsync();
        }

        public IEnumerable<AtividadeAluno> GetNumeroDeAtividadesFeitas(long idAluno)
        {
            return _contexto.AtividadeAluno.Where(at => at.IdPessoa == idAluno).ToList();
        }

        public async Task AddAtividadeAluno(AtividadeAluno atividadeAluno)
        {
            await _contexto.AtividadeAluno.AddAsync(atividadeAluno);
            await _contexto.SaveChangesAsync();
        }

        public async Task UpdateAtividadeAluno(AtividadeAluno atividadeAluno)
        {
            _contexto.AtividadeAluno.Update(atividadeAluno);
            await _contexto.SaveChangesAsync();
        }

        public int GetAttemptsNumberFromId(long idAtividadeAluno){
            AtividadeAluno atividadeAluno = _contexto.AtividadeAluno.Where(a => a.IdAtividadeAluno == idAtividadeAluno).FirstOrDefault();
            if(atividadeAluno != null){
                _contexto.Entry<AtividadeAluno>(atividadeAluno).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                _contexto.SaveChanges();
                return atividadeAluno.NumeroTentativas;
            }else{
                return 0;
            }
        }
    }
}