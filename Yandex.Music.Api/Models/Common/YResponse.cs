﻿namespace Yandex.Music.Api.Models.Common
{
    /// <summary>
    /// Модель ответа от API
    /// </summary>
    public class YResponse<T>
    {
        #region Свойства

        public YInvocationInfo InvocationInfo { get; set; }

        public T Result { get; set; }

        #endregion
    }
}