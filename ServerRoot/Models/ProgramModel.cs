using System;

namespace ServerRoot.Models
{
    /// <summary>
    /// The program information model
    /// </summary>
    public class ProgramModel
    {

        #region Properties
        /// <summary>
        /// The unique id of the program
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The production name of the program
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The owner of the program
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// The verstion of the     
        /// </summary>
        public float Version { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public ProgramModel()
        {

        }
        #endregion
    }
}
