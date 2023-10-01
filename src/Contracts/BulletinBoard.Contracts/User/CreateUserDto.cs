﻿using BulletinBoard.Contracts.Base;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.User
{
    /// <summary>
    /// Модель для создания пользователя.
    /// </summary>
    public class CreateUserDto
    {
        /// <summary>
        /// Имя создаваемого пользователя.
        /// </summary>
        [Required]
        [StringLength(50,MinimumLength = 4)]
        public string Name { get; init; }

        /// <summary>
        /// Логин создаваемого пользователя.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Login { get; init; }

        /// <summary>
        /// Пароль создаваемого пользователя.
        /// </summary>
        [Required]
        [StringLength (100, MinimumLength = 8)]
        public string Password { get; init; }
    }
}
