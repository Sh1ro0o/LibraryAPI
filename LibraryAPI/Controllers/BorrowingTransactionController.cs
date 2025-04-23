using LibraryAPI.Common.Response;
using LibraryAPI.Dto.Book;
using LibraryAPI.Dto.BorrowingTransaction;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("BorrowingTransaction")]
    [ApiController]
    public class BorrowingTransactionController : ControllerBase
    {
        private readonly IBorrowingTransactionService _borrowingTransactionService;
        public BorrowingTransactionController(IBorrowingTransactionService borrowingTransactionService)
        {
            _borrowingTransactionService = borrowingTransactionService;
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<IEnumerable<BorrowingTransactionDto>>))]
        [ProducesResponseType(500)]
        [HttpGet("allBorrowingTransactions")]
        public async Task<IActionResult> GetBorrowingTransactions([FromQuery] BorrowingTransactionFilter filter)
        {
            var result = await _borrowingTransactionService.GetAll(filter);

            return result.ToActionResult();
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<BorrowingTransactionDto>))] //OK
        [ProducesResponseType(409)] //Conflict
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost]
        public async Task<IActionResult> CreateBorrowingTransaction([FromBody] CreateBorrowingTransactionDto model)
        {
            var result = await _borrowingTransactionService.CreateBorrowingTransaction(model);

            return result.ToActionResult();
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<BorrowingTransactionDto>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPut]
        public async Task<IActionResult> UpdateBorrowingTransaction([FromBody] SaveBorrowingTransactionDto model)
        {
            var result = await _borrowingTransactionService.UpdateBorrowingTransaction(model);

            return result.ToActionResult();
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<bool>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowingTransaction([FromRoute] int id)
        {
            var result = await _borrowingTransactionService.DeleteBorrowingTransaction(id);

            return result.ToActionResult();
        }
    }
}
