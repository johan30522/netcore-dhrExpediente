using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppExpedienteDHR.Core.ViewModels.User
{
    public class UserFormViewModel
    {

        public UserViewModel User { get; set; }

        public IEnumerable<SelectListItem>? RolesItems { get; set; }

        public List<string> SelectedRoles { get; set; } = new List<string>();



    }
}
