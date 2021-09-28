using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServerRoot.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServerRoot.Controllers
{
    /// <summary>
    /// The endpoint to call to check for new updates
    /// </summary>
    [Route("/Info/[action]/{id?}")]
    public class InfoController : Controller
    {
        #region Private data
        /// <summary>
        /// The path for the program details json file
        /// </summary>
        private static readonly string ProgramsDetailsFilePath = "./wwwroot/Assets/Files/ProgramsDetails.json";
        #endregion
        /// <summary>
        /// Gets the details for the sent program id
        /// </summary>
        /// <returns></returns>
        public IActionResult Details()
        {
            //Try to get the ids
            if (Guid.TryParse(Request.Query["id"], out Guid programId))
            {
                //Get the data
                var data = GetJSONData(programId);
                if (data == null)
                {
                    return NotFound($"The sent id = {programId} is not found");
                }
                else
                {
                    return Json(data);
                }
            }
            return BadRequest("The sent id is not well formated");
        }


        #region Helpers
        /// <summary>
        /// Gets the json data for the sent id
        /// </summary>
        /// <returns></returns>
        private ProgramModel GetJSONData(Guid id)
        {
            //Open the file
            using (StreamReader file = System.IO.File.OpenText(ProgramsDetailsFilePath))
            {
                //Read the data to the end
                var json = file.ReadToEnd();
                //Deserialize the objec into the program model
                var items = JsonConvert.DeserializeObject<List<ProgramModel>>(json);
                //Get the information for the sent id
                return items.SingleOrDefault(p => p.Id == id);
            }
        }
        #endregion



    }
}
