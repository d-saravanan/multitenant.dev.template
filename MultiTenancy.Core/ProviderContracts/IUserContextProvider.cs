namespace MultiTenancy.Core.ProviderContracts
{
    using System;

    /// <summary>
    /// The IUser Context Provider
    /// </summary>
    public interface IUserContextDataProvider
    {
        /// <summary>
        /// The User identifier
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// The user name
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// The tenant identifier
        /// </summary>
        Guid TenantId { get; }
    }
}
