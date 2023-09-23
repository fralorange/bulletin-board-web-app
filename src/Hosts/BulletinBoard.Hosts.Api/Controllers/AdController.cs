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
        [HttpGet("get-all-by-pages")]

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
        [HttpGet("get-by-id")]
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
        /// <remarks>
        /// ������: curl -X 'POST' \ 'https://localhost:port/ad' \
        /// </remarks>
        /// <param name="dto">������ ������������ ����������.</param>
        /// <param name="cancellationToken">������ ��������.</param>
        /// <returns>������������� ������������ ����������</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAdDto dto, CancellationToken cancellationToken)
        {
            var dtoId = await _adService.CreateAsync(dto, cancellationToken);
            return Created(nameof(CreateAsync), dtoId);
        }

        /// <summary>
        /// ����������� ����������.
        /// </summary>
        /// <param name="id">������������� ����������.</param>
        /// <param name="dto">������ ����������.</param>
        /// <param name="cancellationToken">������ ��������.</param>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateAdDto dto, CancellationToken cancellationToken)
        {
            var ad = await _adService.GetByIdAsync(id, cancellationToken);
            if (ad is null)
                return NotFound();

            await _adService.UpdateAsync(id, dto, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// ������� ���������� �� ��������������.
        /// </summary>
        /// <param name="id">������������� ����������.</param>
        /// <param name="cancellationToken">������ ��������.</param>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var state = await _adService.DeleteAsync(id, cancellationToken);
            if (!state)
                return NotFound();
            return NoContent();
        }
    }
}