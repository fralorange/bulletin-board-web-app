using BulletinBoard.Application.AppServices.Contexts.Ad.Services;
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
        private readonly IAdService _adService;

        /// <summary>
        /// �������������� ��������� <see cref="AdController"/>
        /// </summary>
        /// <param name="adService">������ ��� ������ � ������������.</param>
        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        /// <summary>
        /// ���������� ������������ ����������.
        /// </summary>
        /// <remarks>
        /// ������: curl -X 'GET' \ 'https://localhost:port/ad/get-all-by-pages?pageSize=10&amp;pageIndex=0'
        /// </remarks>
        /// <param name="cancellationToken">������ ��������.</param>
        /// <param name="pageSize">������ ��������.</param>
        /// <param name="pageIndex">����� ��������.</param>
        /// <returns>��������� ���������� <see cref="AdDto"/>.</returns>
        [HttpGet]
        [Route("get-all-by-pages")]

        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            var result = await _adService.GetAllAsync(cancellationToken, pageSize, pageIndex);
            return Ok(result);
        }

        /// <summary>
        /// ���������� ���������� �� ��������� ��������������.
        /// </summary>
        /// <remarks>
        /// ������: curl -X 'GET' \'https://localhost:port/ad/get-by-id'
        /// </remarks>
        /// <param name="id">������������� ����������.</param>
        /// <param name="cancellationToken">������ ��������.</param>
        /// <returns>������ ���������� <see cref="AdDto"/>.</returns>
        [HttpGet]
        [Route("get-by-id")]
        [ProducesResponseType(typeof(AdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _adService.GetByIdAsync(id, cancellationToken);
            if (result == null)
                return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// ������� ����������.
        /// </summary>
        /// <param name="dto">������ ����������.</param>
        /// <param name="cancellationToken">������ ��������.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
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