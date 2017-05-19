namespace Lup.Software.Engineering
{
    using System.Collections.Generic;
    using System.Linq;
    using Lup.Software.Engineering.Models;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json;

    public static class ModelStateHelper
    {
        /// <summary>
        /// Project ModelState Errors down into a KVP that is serializable.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this ModelStateDictionary modelState)
        {
            return modelState
                .Where(x => x.Value.Errors.Any())
                .ToDictionary(
                    m => m.Key,
                    m => m.Value.Errors
                        .Select(e => e.ErrorMessage)
                        .FirstOrDefault(s => s != null));
        }
    }
}