using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdataAnalyzeTool.Model;
using UpdataAnalyzeTool.Utility;
using IapCLR;

namespace UpdataAnalyzeTool.Repository
{
    /// <summary>
    /// 一次升级中所有有效数据包的仓库，有效数据包=》如包含SSW:END
    /// </summary>
    public class UpdataRepository
    {
        #region SSW数据包列表
        /// <summary>
        /// 一次升级数据中所有的有效SSW数据包
        /// </summary>
        public List<SSW_Send> sswSendList { get; set; }
        #endregion

        #region SSW响应包列表
        /// <summary>
        /// 一次升级数据中所有的有效SSW响应包
        /// </summary>
        public List<SSW_Recv> sswRecvList { get; set; }
        #endregion

        #region USW数据包列表
        /// <summary>
        /// 一次升级数据中所有的有效USW数据包
        /// </summary>
        public List<USW_Send> uswSendList { get; set; }
        #endregion

        #region USW响应包列表
        /// <summary>
        /// 一次升级数据中所有的有效USW响应包
        /// </summary>
        public List<USW_Recv> uswRecvList { get; set; }
        #endregion

        #region QSW数据包列表
        /// <summary>
        /// 一次升级数据中所有的有效QSW数据包
        /// </summary>
        public List<QSW_Send> qswSendList { get; set; }
        #endregion

        #region QSW响应包列表
        public List<QSW_Recv> qswRecvList { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数->加密bin文件
        /// </summary>
        /// <param name="file"></param>
        public UpdataRepository(string file)
        {
            this.sswSendList = new List<SSW_Send>();
            this.sswRecvList = new List<SSW_Recv>();
            this.uswSendList = new List<USW_Send>();
            this.uswRecvList = new List<USW_Recv>();
            this.qswSendList = new List<QSW_Send>();
            this.qswRecvList = new List<QSW_Recv>();

            this.GetPackageList(file);
        }
        #endregion

        #region 从加密bin文件中获取升级数据包
        /// <summary>
        /// 从加密bin文件中获取升级数据包
        /// </summary>
        /// <param name="file">文件路径和文件名</param>
        private void GetPackageList(string file)
        {
            Iap iap = new Iap(file);
            this.sswSendList.Add(new SSW_Send(iap.GetSSWPackage()));

            for (int i = 0; i < iap.GetPackageNum(); i++)
            {
                this.uswSendList.Add(new USW_Send(iap.GetUSWPackage(i)));
            }

            this.qswSendList.Add(new QSW_Send(iap.GetQSWPackage()));
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数->串口数据文件
        /// </summary>
        public UpdataRepository(string file, int fileType)
        {
            this.sswSendList = new List<SSW_Send>();
            this.sswRecvList = new List<SSW_Recv>();
            this.uswSendList = new List<USW_Send>();
            this.uswRecvList = new List<USW_Recv>();
            this.qswSendList = new List<QSW_Send>();
            this.qswRecvList = new List<QSW_Recv>();

            this.GetPackageList(file, fileType);
        }
        #endregion

        #region 从串口文件中取到所有的有效升级数据包和响应包
        /// <summary>
        /// 从串口文件中取到所有的有效升级数据包和响应包
        /// </summary>
        /// <param name="file">文件路径和文件名</param>
        /// <param name="fileType">文件类型 0 为bin文件 1 为文本文件</param>
        private void GetPackageList(string file, int fileType)
        {
            byte[] fff;
            if (fileType == 0)
            {
                FileUtility.GetDataFromBinFile(file, out fff);
            }
            else if (fileType == 1)
            {
                FileUtility.GetDataFormTxtFile(file, out fff);
            }
            else
            {
                return;
            }

            for (int i = 0; i < fff.Length; i++)
            {
                if (fff[i] == 'S' && fff[i + 1] == 'S' && fff[i + 2] == 'W')
                {
                    var ssws = new SSW_Send(ByteUtility.GetSubByte(fff, i, 25));
                    if (ssws.isValid)
                    {
                        this.sswSendList.Add(ssws);
                    }
                    else
                    {
                        var sswr = new SSW_Recv(ByteUtility.GetSubByte(fff, i, 9));
                        if (sswr.isValid)
                        {
                            this.sswRecvList.Add(sswr);
                        }
                    }
                }
                else if (fff[i] == 'U' && fff[i + 1] == 'S' && fff[i + 2] == 'W')
                {
                    var usws = new USW_Send(ByteUtility.GetSubByte(fff, i, 523));
                    if (usws.isValid)
                    {
                        this.uswSendList.Add(usws);
                    }
                    else
                    {
                        var uswr = new USW_Recv(ByteUtility.GetSubByte(fff, i, 10));
                        if (uswr.isValid)
                        {
                            this.uswRecvList.Add(uswr);
                        }
                    }
                }
                else if (fff[i] == 'Q' && fff[i + 1] == 'S' && fff[i + 2] == 'W')
                {
                    var qsws = new QSW_Send(ByteUtility.GetSubByte(fff, i, 8));
                    if (qsws.isValid)
                    {
                        this.qswSendList.Add(qsws);
                    }
                    else
                    {
                        var qswr = new QSW_Recv(ByteUtility.GetSubByte(fff, i, 32));
                        if (qswr.isValid)
                        {
                            this.qswRecvList.Add(qswr);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
