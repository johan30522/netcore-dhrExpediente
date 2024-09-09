using AppExpedienteDHR.Core.Domain.Entities.General;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.General
{
    public interface ITipoIdentificacionRepository: IRepository<TipoIdentificacion>
    {
        Task Update(TipoIdentificacion tipoIdentificacion);
    }
}
