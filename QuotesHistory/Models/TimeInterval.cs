namespace AutoTrading.QuotesHistory.Models
{
    /// <summary>
    /// Time interval of quotes history
    /// </summary>
    public enum TimeInterval
    {
        /// <summary>
        /// 1 minute
        /// </summary>
        Minutes1 = 2,
        
        /// <summary>
        /// 5 minutes
        /// </summary>
        Minutes5 = 3,
        
        /// <summary>
        /// 10 minutes
        /// </summary>
        Minutes10 = 4,
        
        /// <summary>
        /// 15 minutes
        /// </summary>
        Minutes15 = 5,
        
        /// <summary>
        /// 30 minutes
        /// </summary>
        Minutes30 = 6,
        
        /// <summary>
        /// 1 hour
        /// </summary>
        Hours = 7,
        
        /// <summary>
        /// 1 day
        /// </summary>
        Days = 8,
        
        /// <summary>
        /// 1 week
        /// </summary>
        Weeks = 9,
        
        /// <summary>
        /// 1 month
        /// </summary>
        Months = 10,
    }
}