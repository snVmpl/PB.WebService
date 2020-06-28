using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PB.Data.Providers;
using System;
using System.Threading.Tasks;

namespace PB.WebService.Controllers
{
    public class ManageDbController : BaseController
    {
        [HttpGet]
        [Route("switchdb/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SwitchDb(string name)
        {
            try
            {
                ConnectionStringProvider.Instance.SetCurrentConnection(name);
            }
            catch (Exception ex)
            {
                SetExceptionResult(ex);
            }

            return Result();
        }
    }
}