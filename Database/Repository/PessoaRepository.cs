using Infra.Connection;
using Infra.Enitty;

namespace Infra.Repository;

public class PessoaRepository : BaseRepositoryAbstract<Pessoa>
{
    
    public PessoaRepository(ContextAdoNet contextAdoNet) : base(contextAdoNet)
    {
        
    }
}