using System;
using System.Security.Cryptography;
using TogglOn.Client.Abstractions.Models;

namespace TogglOn.Client.AspNetCore.Models.Rules
{
    internal class PercentageRule : AbstractFeatureToggleRule
    {
        public int Percentage { get; }

        public PercentageRule(int percentage)
        {
            Percentage = percentage;
        }

        public override bool IsEnabled(AbstractFeatureToggle toggle)
        {
            int percentage = toggle.EnrichedState.HasValue
                ? GetPercentageBasedOnRemoteIpAddress(toggle)
                : GetPercentageBasedOnRequestStatistics(toggle);

            return percentage <= Percentage;
        }

        private int GetPercentageBasedOnRemoteIpAddress(AbstractFeatureToggle toggle)
        {
            using (var hashAlgorithm = MD5.Create())
            {
                var hash = hashAlgorithm.ComputeHash(toggle.EnrichedState.HttpContext.Connection.RemoteIpAddress.GetAddressBytes());
                var number = BitConverter.ToInt32(hash, 0);
                var absoluteValue = Math.Abs(number);

                //return absoluteValue % (100 - Percentage);
                return absoluteValue % 100;
            }
        }

        //TODO: Finish this
        private int GetPercentageBasedOnRequestStatistics(AbstractFeatureToggle toggle)
        {

            return 0;
            //return toggle.TotalRequestAmount == 0
            //    ? 0
            //    : toggle.EnabledRequestAmount / toggle.TotalRequestAmount * 100;
        }
    }
}
