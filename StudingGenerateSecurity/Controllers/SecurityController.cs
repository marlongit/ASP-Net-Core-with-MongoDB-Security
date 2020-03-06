using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudingGenerateSecurity.Model;
using StudingGenerateSecurity.Service;

namespace StudingGenerateSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        /// <summary>
        /// This method create new password whith lenght informed.
        /// </summary>
        /// <param name="lenghtPassword">Enter the password length.</param>
        /// <returns>Returns password with the given length</returns>
        [HttpGet, Route("{lengthPassword}")]
        public string Get(int lengthPassword)
        {
            return SecurityService.Instance.NewPassword(lengthPassword);
        }

        /// <summary>
        /// This method create new password whith lenght informed.
        /// </summary>
        /// <param name="lenghtPassword">Enter the password length.</param>
        /// <param name="whithCaracterEspecial">Inform true or false if the password must contain special characters</param>
        /// <returns>Returns password with the given length</returns>
        [HttpGet, Route("{lengthPassword}/{whithCaracterEspecial}")]
        public string Get(int lengthPassword, bool whithCaracterEspecial)
        {
            return SecurityService.Instance.NewPassword(lengthPassword, whithCaracterEspecial);
        }

        /// <summary>
        /// Creates a new record containing the user account data.
        /// </summary>
        /// <param name="securityEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public string Post(SecurityEntity entity)
        {
            return SecurityService.Instance.Create(entity);
        }

        /// <summary>
        /// List all registered accounts
        /// </summary>
        /// <returns>Returns a list of all registered accounts</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(SecurityService.Instance.ListAll());
        }

        /// <summary>
        /// List all registered accounts using a filter
        /// </summary>
        /// <param name="entity">Entity that will be used with the base fields for filter.</param>
        /// <returns>Returns list of filtered records.</returns>
        [HttpGet, Route("getByFilter")]
        public IActionResult GetByFilter(SecurityEntity entity)
        {
            return Ok(SecurityService.Instance.ListAllByFilter(entity));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(SecurityEntity entity)
        {
            return Ok(entity);
        }
    }
}