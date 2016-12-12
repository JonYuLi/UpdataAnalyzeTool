namespace UTest
{
    /// <summary>
    /// 升级状态定义
    /// </summary>
    public enum UpdateStatus
    {
        /// <summary>
        /// 应用程序接收升级文件中
        /// </summary>
        recvFile = 1,

        /// <summary>
        /// 应用程序完成接收升级文件
        /// </summary>
        revcFileDone = 2,

        /// <summary>
        /// 更新文件中
        /// </summary>
        updateFile = 3,

        /// <summary>
        /// 更新完成
        /// </summary>
        updateFileDone = 4,

        /// <summary>
        /// 应用程序正常运行中
        /// </summary>
        softwareRun = 5,

        /// <summary>
        /// 恢复软件中
        /// </summary>
        recover = 6,

        /// <summary>
        /// 完成恢复软件
        /// </summary>
        recoverDone = 7,
    }
}