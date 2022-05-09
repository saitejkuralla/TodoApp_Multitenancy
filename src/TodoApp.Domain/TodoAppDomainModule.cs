using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using TodoApp.MultiTenancy;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.MultiTenancy.ConfigurationStore;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace TodoApp
{
    [DependsOn(
        typeof(TodoAppDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpEmailingModule)
    )]
    public class TodoAppDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        //   Guid _testTenantId = Guid.NewGuid();
        Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
                options.DatabaseStyle = MultiTenancyDatabaseStyle.PerTenant;//added by sai
            });

            //Configure<AbpDefaultTenantStoreOptions>(options =>
            //{
            //    options.Tenants = new[]
            //    {
            //        new TenantConfiguration(
            //            Guid.Parse("446a5211-3d72-4339-9adc-845151f8ada0"), //Id
            //            "tenant1sai" //Name
            //        ),
            //        new TenantConfiguration(
            //            Guid.Parse("25388015-ef1c-4355-9c18-f6b6ddbaf89d"), //Id
            //            "tenant2sai" //Name
            //        )
            //        {
            //            //tenant2 has a seperated database
            //            //ConnectionStrings =
            //            //{
            //            //    {ConnectionStrings.DefaultConnectionStringName, "..."}
            //            //}
            //        }
            //    };
            //});

#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
        }
    }
}
