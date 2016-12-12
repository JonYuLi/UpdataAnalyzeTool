namespace UTest
{
    /// <summary>
    /// 升级结果定义
    /// </summary>
    public enum UpdateResult
    {
        /// <summary>
        /// 升级完成
        /// </summary>
        updateDone = 0,

        /// <summary>
        /// 升级超时（保留，未使用）
        /// </summary>
        TimeOut = 1,

        /// <summary>
        /// FLASH 读写失败
        /// </summary>
        FLASHError = 2,

        /// <summary>
        /// 校验错误
        /// </summary>
        CheckError = 3,

        /// <summary>
        /// 长度错误
        /// </summary>
        LengthError = 4,

        /// <summary>
        /// 等待升级包
        /// </summary>
        waitingPackage = 5,

        /// <summary>
        /// 应用软件无法正常运行
        /// </summary>
        SoftWareRunError = 6,
    }
}