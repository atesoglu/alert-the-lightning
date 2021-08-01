using System.Collections.Generic;
using Application.Flows.Assets.Commands;
using Application.Flows.Assets.Queries;
using Application.Flows.LightningStrikes.Commands;
using Application.Models;
using Application.Request;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMemoryCache()
                // .AddTransient<IFacilityHandler, FacilityHandler>()
                // .AddTransient<IPatientHandler, PatientHandler>()
                // .AddTransient<IAppointmentHandler, AppointmentHandler>()
                // .AddTransient<IChargeHandler, ChargeHandler>()
                // .AddTransient<IPaymentHandler, PaymentHandler>()
                //
                .AddTransient<IRequestHandler<ListAssetsQuery, ICollection<AssetObjectModel>>, ListAssetsHandler>()
                .AddTransient<IRequestHandler<CreateAssetCommand, AssetObjectModel>, CreateAssetHandler>().AddTransient<IValidator<CreateAssetCommand>, CreateAssetValidator>()
                //
                .AddTransient<IRequestHandler<ProcessLightningStrikeCommand, LightningStrikeObjectModel>, ProcessLightningStrikeHandler>().AddTransient<IValidator<ProcessLightningStrikeCommand>, ProcessLightningStrikeValidator>()
                // .AddTransient<IRequestHandler<ConsumePatientByIdCommand, PatientModel>, ConsumePatientByIdHandler>().AddTransient<IValidator<ConsumePatientByIdCommand>, ConsumePatientByIdValidator>()
                // .AddTransient<IRequestHandler<ConsumeAppointmentsUpdatedCommand>, ConsumeAppointmentsUpdatedHandler>().AddTransient<IValidator<ConsumeAppointmentsUpdatedCommand>, ConsumeAppointmentsUpdatedValidator>()
                // .AddTransient<IRequestHandler<ConsumeClaimsUpdatedCommand>, ConsumeClaimsUpdatedHandler>().AddTransient<IValidator<ConsumeClaimsUpdatedCommand>, ConsumeClaimsUpdatedValidator>()
                // .AddTransient<IRequestHandler<ConsumeAppointmentsByDateRangeCommand>, ConsumeAppointmentsByDateRangeHandler>().AddTransient<IValidator<ConsumeAppointmentsByDateRangeCommand>, ConsumeAppointmentsByDateRangeValidator>()
                // .AddTransient<IRequestHandler<ConsumeClaimsByPatientIdCommand>, ConsumeClaimsByPatientIdHandler>().AddTransient<IValidator<ConsumeClaimsByPatientIdCommand>, ConsumeClaimsByPatientIdValidator>()
                // .AddTransient<IRequestHandler<ConsumePaymentsByClaimIdCommand>, ConsumePaymentsByClaimIdHandler>().AddTransient<IValidator<ConsumePaymentsByClaimIdCommand>, ConsumePaymentsByClaimIdValidator>()
                ;

            return services;
        }
    }
}