using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace AppExpedienteDHR.Infrastructure.Repositories.Workflow
{
    public class ActionRuleWfRepository : Repository<ActionRuleWf>, IActionRuleWfRespository
    {
        private readonly ApplicationDbContext _context;

        public ActionRuleWfRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(ActionRuleWf actionRule)
        {
            ActionRuleWf actionRuleToUpdate = await _context.ActionRuleWfs.FirstOrDefaultAsync(c => c.Id == actionRule.Id);
            if (actionRuleToUpdate != null)
            {
                actionRuleToUpdate.ActionId = actionRule.ActionId;
                actionRuleToUpdate.Name = actionRule.Name;
                actionRuleToUpdate.Order = actionRule.Order;
                actionRuleToUpdate.FieldEvaluated = actionRule.FieldEvaluated;
                actionRuleToUpdate.Operator = actionRule.Operator;
                actionRuleToUpdate.ComparisonValue = actionRule.ComparisonValue;
                actionRuleToUpdate.ResultStateId = actionRule.ResultStateId;

                await _context.SaveChangesAsync();
            }
        }
    }
}
