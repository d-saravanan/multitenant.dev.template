namespace MultiTenantRepository.Enums
{
    /// <summary>
    /// The Types of operations that can be done on an Entity
    /// </summary>
    public enum EntityOperations : int
    {
        /// <summary>
        /// The Read Operation
        /// </summary>
        Read,
        /// <summary>
        /// The create operation
        /// </summary>
        Create,
        /// <summary>
        /// The update operation
        /// </summary>
        Update,
        /// <summary>
        /// The delete operation
        /// </summary>
        Delete,
        /// <summary>
        /// The search operation
        /// </summary>
        Search
    }
}
