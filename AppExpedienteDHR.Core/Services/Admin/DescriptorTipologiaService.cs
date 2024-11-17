using AppExpedienteDHR.Core.ServiceContracts.Admin;
using AppExpedienteDHR.Core.ViewModels.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;

using Serilog;
using AutoMapper;


namespace AppExpedienteDHR.Core.Services.Admin
{
    public class DescriptorTipologiaService : IDescriptorTipologiaService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public DescriptorTipologiaService(
            IContainerWork containerWork,
            IMapper mapper,
            ILogger logger
        )
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<bool> DeleteDescriptor(int id)
        {
            try
            {
                // busca por el id 
                var descriptor = await _containerWork.Descriptor.GetFirstOrDefault(x => x.Id == id );
                if (descriptor != null) {
                    descriptor.IsDeleted = true;
                    descriptor.DeletedAt = DateTime.Now;
                    await _containerWork.Save();
                    return true;
                } else { return false; }

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar el descriptor");
                return false;
            }
        }

        public async Task<DescriptorViewModel> GetDescriptorById(int id)
        {
            try
            {
                var descriptor = await _containerWork.Descriptor.GetFirstOrDefault(x => x.Id == id );
                return _mapper.Map<DescriptorViewModel>(descriptor);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el descriptor");
                return null;
            }
        }
        public async Task<DescriptorViewModel> GetDescriptorByCode(string code)
        {
            try
            {
                var descriptor = await _containerWork.Descriptor.GetFirstOrDefault(x => x.Codigo == code);
                return _mapper.Map<DescriptorViewModel>(descriptor);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el descriptor");
                return null;
            }
        }

        public async Task<IEnumerable<DescriptorViewModel>> GetDescriptors(int eventoId)
        {
            try
            {
                var descriptor = await _containerWork.Descriptor.GetAll(x =>  x.EventoId == eventoId);
                return _mapper.Map<IEnumerable<DescriptorViewModel>>(descriptor);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener los descriptores");
                return null;
            }
        }

        public async Task<DescriptorViewModel> InsertDescriptor(DescriptorViewModel descriptor)
        {
            try
            {
                var descriptorEntity = _mapper.Map<Descriptor>(descriptor);

                await _containerWork.Descriptor.Add(descriptorEntity);

                await _containerWork.Save();
                return _mapper.Map<DescriptorViewModel>(descriptorEntity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al insertar el descriptor");
                return null;
            }
        }

        public async Task<DescriptorViewModel> UpdateDescriptor(DescriptorViewModel descriptor)
        {
            try
            {
                var descriptorEntity = _mapper.Map<Descriptor>(descriptor);

                await _containerWork.Descriptor.Update(descriptorEntity);

                await _containerWork.Save();
                return _mapper.Map<DescriptorViewModel>(descriptorEntity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar el descriptor");
                return null;
            }
        }
    }
}
