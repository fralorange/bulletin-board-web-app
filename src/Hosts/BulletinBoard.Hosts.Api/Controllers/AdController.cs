using BulletinBoard.Contracts.Ad;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// ���������� ��� ������ � ������������.
    /// </summary>
    [ApiController]
    [Route("ad")]
    public class AdController : ControllerBase
    {
        /// <summary>
        /// ���������� ������������ ����������.
        /// </summary>
        /// <param name="cancellationToken">������ ��������.</param>
        /// <param name="pageSize">������ ��������.</param>
        /// <param name="pageIndex">����� ��������.</param>
        /// <returns>��������� ���������� <see cref="AdDto"/>.</returns>
        [HttpGet]
        [Route("get-all-by-pages")]

        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            return Ok();
        }

        /// <summary>
        /// ���������� ���������� �� ��������� ��������������.
        /// </summary>
        /// <param name="id">������������� ����������.</param>
        /// <param name="cancellationToken">������ ��������.</param>
        /// <returns>������ ���������� <see cref="AdDto"/>.</returns>
        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// ������� ����������.
        /// </summary>
        /// <param name="dto">������ ����������.</param>
        /// <param name="cancellationToken">������ ��������.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(AdDto dto, CancellationToken cancellationToken)
        {
            return Created(string.Empty, null);
        }

        /// <summary>
        /// ����������� ����������.
        /// </summary>
        /// <param name="dto">������ ����������.</param>
        /// <param name="cancellationToken">������ ��������.</param>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(AdDto dto, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// ������� ���������� �� ��������������.
        /// </summary>
        /// <param name="id">������������� ����������.</param>
        /// <param name="cancellationToken">������ ��������.</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}