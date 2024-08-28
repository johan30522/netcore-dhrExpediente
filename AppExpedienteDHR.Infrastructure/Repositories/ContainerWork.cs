using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using AppExpedienteDHR.Infrastructure.Repositories;
using AppExpedienteDHR.Infrastructure.Repositories.Workflow;


namespace BlogCore.Infrastructure.Repositories
{
    public class ContainerWork : IContainerWork
    {
        private readonly ApplicationDbContext _context;


        public ContainerWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            User = new UserRepository(_context);
            // Workflow repositories
            ActionWf = new ActionWfRepository(_context);
            StateWf = new StateWfRepository(_context);
            ActionGroupWf = new ActionGroupWfRepository(_context);
            GroupWf = new GroupWfRepository(_context);
            ActionRuleWf = new ActionRuleWfRepository(_context);
            FlowHistoryWf = new FlowHistoryWfRepository(_context);
            FlowWf = new FlowWfRepository(_context);
            GroupUserWf = new GroupUserWfRepository(_context);
            RequestFlowHistoryWf = new RequestFlowHistoryWfRepository(_context);

        }
        public ICategoryRepository Category { get; private set; }

        public IUserRepository User { get; private set; }


        #region Workflow repositories

        public IActionWfRepository ActionWf { get; private set; }

        public IStateWfRepository StateWf { get; private set; }

        public IActionGroupWfRepository ActionGroupWf { get; private set; }

        public IGroupWfRepository GroupWf { get; private set; }

        public IActionRuleWfRespository ActionRuleWf { get; private set; }


        public IFlowHistoryWfRepository FlowHistoryWf { get; private set; }

    
        public IFlowWfRepository FlowWf { get; private set; }

        public IGroupUserWfRepository GroupUserWf { get; private set; }

        public IRequestFlowHistoryWfRepository RequestFlowHistoryWf { get; private set; }


        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
