using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Random_Numbers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public ValidationResult ValidationResult { get; set; }
        public BaseController()
        {
            ValidationResult = new ValidationResult();
        }
        public ActionResult CustomResponse(object data = null)
        {
            if (ValidationResult.Errors.Any())
            {
                var errors = new List<string>();
                foreach (var error in ValidationResult.Errors)
                    errors.Add(error.ErrorMessage);

                return BadRequest(new { success = false, errors = errors });
            }

            return Ok(new { success = true, data = data });
        }

        public void AddValidationResult(ValidationResult validation)
        {
            this.ValidationResult = validation;
        }

        public void AddErrorToList(string error)
        {
            this.ValidationResult.Errors.Add(new ValidationFailure("", error));
        }
    }
}
