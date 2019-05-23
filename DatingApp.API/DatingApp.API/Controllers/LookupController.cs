using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.IServices;
using DatingApp.API.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LookupController : ControllerBase
    {
        ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("{type}")]
        public async Task<IActionResult> GetLookup(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                return Ok(await _lookupService.GetLookup(type));
            }
            return BadRequest();
        }
    }
}