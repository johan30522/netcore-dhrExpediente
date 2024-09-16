using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Dhr
{
    public class DenuncianteRepository : Repository<Denunciante>, IDenuncianteRepository
    {
        private readonly ApplicationDbContext _context;

        public DenuncianteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task <Denunciante> Update(Denunciante denunciante)
        {
            Denunciante denuncianteToUpdate = await _context.Denunciantes.FirstOrDefaultAsync(d => d.Id == denunciante.Id);
            if (denuncianteToUpdate != null)
            {
                denuncianteToUpdate.Nombre = denunciante.Nombre;
                denuncianteToUpdate.TipoIdentificacionId = denunciante.TipoIdentificacionId;
                denuncianteToUpdate.NumeroIdentificacion = denunciante.NumeroIdentificacion;
                denuncianteToUpdate.PrimerApellido = denunciante.PrimerApellido;
                denuncianteToUpdate.SegundoApellido = denunciante.SegundoApellido;
                denuncianteToUpdate.SexoId = denunciante.SexoId;
                denuncianteToUpdate.EstadoCivilId = denunciante.EstadoCivilId;
                denuncianteToUpdate.PaisOrigenCodigo = denunciante.PaisOrigenCodigo;
                denuncianteToUpdate.EscolaridadId = denunciante.EscolaridadId;
                denuncianteToUpdate.TelefonoCelular = denunciante.TelefonoCelular;
                denuncianteToUpdate.CorreoElectronico = denunciante.CorreoElectronico;
                denuncianteToUpdate.EsMenorEdad = denunciante.EsMenorEdad;
                denuncianteToUpdate.TieneRequerimientoEspecial = denunciante.TieneRequerimientoEspecial;
                denuncianteToUpdate.ProvinciaCodigo = denunciante.ProvinciaCodigo;
                denuncianteToUpdate.CantonCodigo = denunciante.CantonCodigo;
                denuncianteToUpdate.DistritoCodigo = denunciante.DistritoCodigo;
                denuncianteToUpdate.DireccionExacta = denunciante.DireccionExacta;

                await _context.SaveChangesAsync();

                return denuncianteToUpdate;
            }
            return null;
        }
    }
    
}
