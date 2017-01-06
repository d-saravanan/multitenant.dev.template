# About
This is a boilerplate framework code base that can be used to fasten the development of the applications that are multi-tenant. The multi-tenant or tenancy aware applications are the most sought after these days with the growing demand for apps that can be hosted in single code base on the cloud with the customizations per tenant or per customer or per company.

The following terms
1. Tenant
2. Customer
3. Company

are analogous and refers to the term **tenant** in **multi-tenancy**.

# Introduction
This source code contains abstract implementatons for the development team to get started with ease in building the **multi-tenant** application.

# Source Code Overview
The folder named **MultiTenancy.Core** contains the multi-tenancy aspect, which helps the user of this source to help the layers higher in the hierarchy get a feel of the context of the tenant under which the request is being handled.

## User Context Provider
The following is the contract that can be implemented to figure out the tenant id from the presentation tier. The presentation tier can be either an MVC app or a Web Api layer.

[**Contract**](https://github.com/d-saravanan/multitenant.dev.template/MultiTenancy.Core/ProviderContracts/IUserContextProvider.cs)

## Sample User Context Provider

The following file provides a sample User Context provider implementation that looks into the logged in user claims to figure out the **tenantid** and the **userid** from the established identity.
[**Sample Code**](https://github.com/d-saravanan/multitenant.dev.template/blob/master/MultiTenancy.Core/Providers/ClaimsContextDataProvider.cs)

# Database Sharding
In order to facilitate the option of **Shared DataStore** or **Dedicated DataStore** per tenant, the Shard resolver is required. 
For a widespread implementation, please refer this [link](https://msdn.microsoft.com/en-us/library/aa479086.aspx) from Microsoft about Multi-Tenant Data Sharding.

This code base provides the options for the user to pass in a shard resolver that can be used by the Data access layer to get the tenant specific connection and then once obtained, the same can be used to get the database calls routed to the corresponding databases

The Shard Manager has its [Contract](https://github.com/d-saravanan/multitenant.dev.template/blob/master/src/GenericRepository/Shards/Contracts/ITenantShardResolver.cs) defined here which enables the implementation or the development team can provide their own implementation which can be a custom one or can be targetted to the Azure Shard Maps.

# Database Tier
The database tier implmentation is completely pluggable / testable with the use of the contracts as given [here](https://github.com/d-saravanan/multitenant.dev.template/tree/master/src/GenericRepository.EntityFramework)

The Repository implementation given [here](https://github.com/d-saravanan/multitenant.dev.template/blob/master/src/GenericRepository.EntityFramework/Repository/EntityRepository'2.cs) can be used to accomplish the basic use-case of any **CRUD** implementation. However, in-case of any custom implementation, the developer can inherit from the corresponding contract and the implementation and create their own **DAL** implementations

# Generic Service Layering
The **Service Layer** plays an important role in the development phase of any application having to fullfil the following responsibilities
* Custom Business Logic Implementations
* Access Control checks that ensures that the ones that are authorized to view / update / create data is only granted access to do so
* Customizable Rules / data transformation or manipulation
* Wrapping the database calls and exceptions

Given the above responsibilites, the development teams spend a lot of time in designing the service layer and then filling out the missed out ones when there is a requirement coming or found from the production feedback / issues.

Based on my expreience over the years, I have put those to build a generic service layer that gives the users the hooks to plug-in the custom logic and leaving the generic / fixed logic to be handled by the **GenericService**

For implementation details, please refer [this](https://github.com/d-saravanan/multitenant.dev.template/blob/master/GenericService/Services/BaseService.cs). This service is written so that the service can be used to plugin any pre-developed or third-party services without bothering about the code in this template

I have written an abstract documentation for this implementation. However, in order to better understand this template / boilerplate codebase, please review the sample application implementation as given [here](https://github.com/d-saravanan/multitenant.dev.template/tree/master/samples/GenericRepository.EntityFramework.SampleWebApi)

For any more details or implementation help or suggestions to improve this codebase or sharing your implentation difficulties, please feel free to <a href="mailto:s.dorai2009@gmail.com" title=" email the developer of this repo">mail me</a>


