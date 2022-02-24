using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder.Client.Common.Models
{
    public class PageData : BindableBase
    {
        public int SizePage { get; set; }

        #region int CurrentPage 当前页
        /// <summary>
        /// 当前页 字段
        /// </summary>
        private int _CurrentPage;
        /// <summary>
        /// 当前页 属性
        /// </summary>
        public int CurrentPage
        {
            get => _CurrentPage;
            set
            {
                if (_CurrentPage != value)
                {
                    _CurrentPage = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region int Total 总条数
        /// <summary>
        /// 总条数 字段
        /// </summary>
        private int _Total;
        /// <summary>
        /// 总条数 属性
        /// </summary>
        public int Total
        {
            get => _Total;
            set
            {
                if (_Total != value)
                {
                    _Total = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region int TotalPage 总页数
        /// <summary>
        /// 总页数 字段
        /// </summary>
        private int _TotalPage;
        /// <summary>
        /// 总页数 属性
        /// </summary>
        public int TotalPage
        {
            get => _TotalPage;
            set
            {
                if (_TotalPage != value)
                {
                    _TotalPage = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region int? GoPage 跳转页
        /// <summary>
        /// 跳转页 字段
        /// </summary>
        private int? _GoPage;
        /// <summary>
        /// 跳转页 属性
        /// </summary>
        public int? GoPage
        {
            get => _GoPage;
            set
            {
                if (_GoPage != value)
                {
                    _GoPage = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region bool HasPreviousPage 是否有前一页
        /// <summary>
        /// 是否有前一页 字段
        /// </summary>
        private bool _HasPreviousPage;
        /// <summary>
        /// 是否有前一页 属性
        /// </summary>
        public bool HasPreviousPage
        {
            get => _HasPreviousPage;
            set
            {
                if (_HasPreviousPage != value)
                {
                    _HasPreviousPage = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region bool HasNextPage 是否有后一页
        /// <summary>
        /// 是否有后一页 字段
        /// </summary>
        private bool _HasNextPage;
        /// <summary>
        /// 是否有后一页 属性
        /// </summary>
        public bool HasNextPage
        {
            get => _HasNextPage;
            set
            {
                if (_HasNextPage != value)
                {
                    _HasNextPage = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion
    }
}
