namespace Pc.Information.Model.Config
{
    /// <summary>
    /// App setting model.
    /// </summary>
    public class AppSettingsModel
    {
        /// <summary>
        /// Redis cach config setting.
        /// </summary>
        public RedisCaching RedisCaching { get; set; }
    }

    /// <summary>  
    /// Redis  
    /// </summary>  
    public class RedisCaching
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>  
        /// 链接信息
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
