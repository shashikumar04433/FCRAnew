using FCRA.Models;
using FCRA.Models.Account;
using FCRA.Models.Customers;
using FCRA.Models.Defaults;
using FCRA.Models.Masters;
using FCRA.Repository.Helpers;
using FCRA.Repository.Managers;
using FCRA.Repository.Managers.Implementations;
using FCRA.Repository.Managers.Implementations.Masters;
using FCRA.Repository.Repositories;
using FCRA.Repository.Repositories.Implementations;
using FCRA.ViewModels;
using FCRA.ViewModels.Account;
using FCRA.ViewModels.Customers;
using FCRA.ViewModels.Defaults;
using FCRA.ViewModels.Masters;
using Microsoft.Extensions.DependencyInjection;

namespace FCRA.Repository
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
        {
            services.AddTransient(provider =>
            {
                var factory = new ContextFactory();
                return factory.CreateDbContext(Array.Empty<string>());
            });

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            #region Helpers
            services.AddScoped<IDBHelper, DBHelper>();
            #endregion

            #region Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepositoryCustomer<>), typeof(RepositoryCustomer<>));
            services.AddScoped(typeof(IBaseModelRepository<>), typeof(BaseModelRepository<>));
            services.AddScoped(typeof(IBaseIdModelRepository<>), typeof(BaseIdModelRepository<>));
            services.AddScoped(typeof(IBaseModelCustomerRepository<>), typeof(BaseModelCustomerRepository<>));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IProductServiceMappingRepository, ProductServiceMappingRepository>();
            services.AddScoped<IProductRiskCriteriaMappingRepository, ProductRiskCriteriaMappingRepository>();
            services.AddScoped<IRiskAssessmentRepository, RiskAssessmentRepository>();
            services.AddScoped<IRiskScoreResponseRepository, RiskScoreResponseRepository>();
            services.AddScoped<IRiskScoreProductVolumRatingResponseRepository, RiskScoreProductVolumRatingResponseRepository>();
            services.AddScoped<IRiskFactorResponseRepository, RiskFactorResponseRepository>();
            services.AddScoped<IRiskSubFactorResponseRepository, RiskSubFactorResponseRepository>();
            services.AddScoped<IRiskSubFactorVolumeResponseRepository, RiskSubFactorVolumeResponseRepository>();
            services.AddScoped<IQuestionRiskCriteriaMappingRepository, QuestionRiskCriteriaMappingRepository>();
            services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
            services.AddScoped<IApprovalMatrixRepository, ApprovalMatrixRepository>();

            #endregion
            services.AddScoped<IReportRepository, ReportRepository>();


            #region Managers
            #region Masters
            services.AddScoped<IMasterManager<RoleMasterViewModel>, MasterManager<RoleMasterViewModel, RoleMaster>>();
            services.AddScoped<IMasterManager<UserViewModel>, MasterManager<UserViewModel, UserMaster>>();
            services.AddScoped<IMasterManager<CustomerViewModel>, MasterManager<CustomerViewModel, Customer>>();

            services.AddScoped<IIdModelManager<FormViewModel>, IdModelManager<FormViewModel, FormMaster>>();
            services.AddScoped<IIdModelManager<DefaultScaleViewModel>, IdModelManager<DefaultScaleViewModel, DefaultScale>>();


            services.AddScoped<IMasterManagerCustomer<StageViewModel>, MasterManagerCustomer<StageViewModel, Stage>>();
            services.AddScoped<IMasterManagerCustomer<RiskTypeViewModel>, MasterManagerCustomer<RiskTypeViewModel, RiskType>>();
            services.AddScoped<IMasterManagerCustomer<GeographicPresenceViewModel>, MasterManagerCustomer<GeographicPresenceViewModel, GeographicPresence>>();
            services.AddScoped<IMasterManagerCustomer<CustomerSegmentViewModel>, MasterManagerCustomer<CustomerSegmentViewModel, CustomerSegment>>();
            services.AddScoped<IMasterManagerCustomer<BusinessSegmentViewModel>, MasterManagerCustomer<BusinessSegmentViewModel, BusinessSegment>>();
            services.AddScoped<IMasterManagerCustomer<GeographyRiskViewModel>, MasterManagerCustomer<GeographyRiskViewModel, GeographyRisk>>();

            services.AddScoped<IMasterManagerCustomer<RiskFactorViewModel>, MasterManagerCustomer<RiskFactorViewModel, RiskFactor>>();
            services.AddScoped<IMasterManagerCustomer<RiskSubFactorViewModel>, MasterManagerCustomer<RiskSubFactorViewModel, RiskSubFactor>>();



            services.AddScoped<IMasterManagerCustomer<PreDefinedRiskParameterViewModel>, MasterManagerCustomer<PreDefinedRiskParameterViewModel, PreDefinedRiskParameter>>();
            services.AddScoped<IMasterManagerCustomer<QuestionsViewModel>, MasterManagerCustomer<QuestionsViewModel, Questions>>();
            services.AddScoped<IMasterManagerCustomer<ProductServiceViewModel>, MasterManagerCustomer<ProductServiceViewModel, ProductService>>();
            services.AddScoped<IMasterManagerCustomer<RiskCriteriaViewModel>, MasterManagerCustomer<RiskCriteriaViewModel, RiskCriteria>>();
            services.AddScoped<IMasterManager<CountryViewModel>, MasterManager<CountryViewModel, Country>>();
            services.AddScoped<IModelManagerCustomer<ExitRemarksViewModel>, ModelManagerCustomer<ExitRemarksViewModel, ExitRemarks>>();
            #endregion


            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IProductServiceMappingManager, ProductServiceMappingManager>();
            services.AddScoped<IProductRiskCriteriaMappingManager, ProductRiskCriteriaMappingManager>();
            services.AddScoped<IRiskAssessmentManager, RiskAssessmentManager>();
            services.AddScoped<IReportManager, ReportManager>();
            services.AddScoped<IAuditTrailManager, AuditTrailManager>();
            services.AddScoped<IApprovalMatrixManager, ApprovalMatrixManager>();
            #endregion

            return services;
        }
    }
}
