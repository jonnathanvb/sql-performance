using Infra.Connection;
using Infra.Enitty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class PessoaRepositoryEntity : BaseRepositoryEntityFramework
    {
        public PessoaRepositoryEntity(ContextEntityFramework context) : base(context)
        {
        }
        public Pessoa Search() => Context.Pessoa.FirstOrDefault();

        public void Insert(Pessoa pessoa)
        {
            Context.Pessoa.Add(pessoa);
            Context.SaveChanges();
        }

        public void InsertListSaveEach(List<Pessoa> pessoas)
        {
            foreach (var pessoa in pessoas)
            {
                Context.Pessoa.Add(pessoa);
                Context.SaveChanges();
            }
        }

        public void InsertListSaveAll(List<Pessoa> pessoas)
        {
            foreach (var pessoa in pessoas)
            {
                Context.Pessoa.Add(pessoa);
            }
            Context.SaveChanges();
        }

        public void InsertListRange(List<Pessoa> pessoa)
        {
            Context.Pessoa.AddRange(pessoa);
            Context.SaveChanges();
        }


    }
}
