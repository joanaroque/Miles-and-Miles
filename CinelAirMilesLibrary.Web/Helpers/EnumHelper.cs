namespace CinelAirMilesLibrary.Common.Helpers
{
    using CinelAirMilesLibrary.Common.Enums;

    public static class EnumHelper
    {
        /// <summary>
        /// Receives a PremiumType to convert to a NotificationType
        /// </summary>
        /// <param name="type"></param>
        /// <returns>NotificationType type, or a broken type if any isn't found</returns>
        public static NotificationType GetType(PremiumType type)
        {
            switch (type)
            {
                case PremiumType.Ticket:
                    return NotificationType.Ticket;
                case PremiumType.Upgrade:
                    return NotificationType.Upgrade;
                case PremiumType.Voucher:
                    return NotificationType.Voucher;
                default:
                    return NotificationType.Broken;
            }
        }
    }
}
