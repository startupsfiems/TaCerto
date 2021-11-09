using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

using TaCertoForms.Contexts;
using TaCertoForms.Models;

namespace TaCertoForms.Factory {
    public class PessoaMatrizCreator : BaseCreator, IFactoryPessoa {
        public PessoaMatrizCreator(HttpSessionStateBase session) : base(session) { }

        public Pessoa FindPessoa(int? id) {
            if(id == null) return null;
            Context db = new Context();
            Pessoa pessoa = db.Pessoa.Find(id);
            if(pessoa == null) return null;
            Instituicao instituicao = db.Instituicao.Find(pessoa.IdInstituicao);
            if(instituicao == null) return null;
            if(instituicao.IdInstituicao == IdMatriz || (instituicao.IdMatriz != null && instituicao.IdMatriz == IdMatriz))
                return pessoa;
            db.Dispose();
            return null;
        }

        public List<Pessoa> PessoaList() {
            Context db = new Context();
            List<int> idAuxList;

            List<Instituicao> instituicaoList = db.Instituicao.Where(i => i.IdInstituicao == IdMatriz || (i.IdMatriz != null && i.IdMatriz == IdMatriz)).ToList();
            if(instituicaoList == null || instituicaoList.Count == 0) return null;
            idAuxList = new List<int>();
            foreach(var i in instituicaoList) idAuxList.Add(i.IdInstituicao);

            List<Pessoa> pessoaList = db.Pessoa.Where(p => idAuxList.Contains(p.IdInstituicao)).ToList();
            if(pessoaList == null || pessoaList.Count == 0) return null;

            db.Dispose();
            return pessoaList;
        }

        public Pessoa CreatePessoa(Pessoa pessoa) {
            Context db = new Context();

            Instituicao instituicao = db.Instituicao.Find(pessoa.IdInstituicao);
            if(instituicao == null) return null;
            if(instituicao.IdInstituicao != IdMatriz && (instituicao.IdMatriz == null || instituicao.IdMatriz != IdMatriz))
                return null;

            if(pessoa.Perfil.Equals(Perfil.Aluno)) {
                pessoa.Senha = generateHash(pessoa.Senha);
            }

            db.Pessoa.Add(pessoa);
            db.SaveChanges();
            return pessoa;
        }

        public Pessoa EditPessoa(Pessoa pessoa) {
            Context db = new Context();

            Pessoa pessoa_aux = db.Pessoa.Find(pessoa.IdPessoa);

            if (pessoa_aux == null) return null;

            Instituicao instituicao = db.Instituicao.Find(pessoa_aux.IdInstituicao);
            if(instituicao == null) return null;
            if(instituicao.IdInstituicao != IdMatriz && (instituicao.IdMatriz == null || instituicao.IdMatriz != IdMatriz))
                return null;

            instituicao = db.Instituicao.Find(pessoa.IdInstituicao);
            if(instituicao == null) return null;
            if(instituicao.IdInstituicao != IdMatriz && (instituicao.IdMatriz == null || instituicao.IdMatriz != IdMatriz))
                return null;

            db.Dispose();
            db = new Context();
            db.Entry(pessoa).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
            return pessoa;
        }

        public bool DeletePessoa(int? id) {
            if(id == null) return false;
            Context db = new Context();
            Pessoa pessoa = db.Pessoa.Find(id);
            if(pessoa == null) return false;

            Instituicao instituicao = db.Instituicao.Find(pessoa.IdInstituicao);
            if(instituicao == null) return false;
            if(instituicao.IdInstituicao != IdMatriz && (instituicao.IdMatriz == null || instituicao.IdMatriz != IdMatriz))
                return false;

            db.Pessoa.Remove(pessoa);
            db.SaveChanges();
            db.Dispose();
            return true;
        }

        private string generateHash(string textToEncrypt) {            
            string ToReturn = "";
            string publickey = "santhosh";
            string secretkey = "engineer";
            byte[] secretkeyByte = { };
            secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
            using(DESCryptoServiceProvider des = new DESCryptoServiceProvider()) {
                ms = new MemoryStream();
                cs = new CryptoStream(ms,des.CreateEncryptor(publickeybyte,secretkeyByte),CryptoStreamMode.Write);
                cs.Write(inputbyteArray,0,inputbyteArray.Length);
                cs.FlushFinalBlock();
                ToReturn = Convert.ToBase64String(ms.ToArray());
            }
            return ToReturn;
        }
    }
}