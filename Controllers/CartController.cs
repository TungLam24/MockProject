using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MockProject.Data;
using MockProject.Models;
using MockProject.Models.User;
using System.Net;
using System.Security.Claims;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MockProject.Controllers
{
    [Route("/api/Cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;
        public CartController(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _response = new APIResponse();
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("AddToCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Add(int id, int qty)
        {
            try
            {
                int cardNo = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.Name));
                var cart = await _db.Cart.FirstOrDefaultAsync(i => i.Id == cardNo && i.ProductId == id);
                var product = await _db.Products.FirstOrDefaultAsync(i => i.Id == id);
                if (cart == null)
                {   
                    if (qty > product.RemainingQuantity)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("Dont have enough item");
                        return BadRequest(_response);
                    }
                    var cart1 = new Cart();
                    cart1.ProductId = id;
                    cart1.UserId = cardNo;
                    cart1.Quantity = qty;
                    await _db.Cart.AddAsync(cart1);
                    await _db.SaveChangesAsync();
                    _response.Result = await _db.Cart.Where(i => i.UserId == cardNo).Select(i => new {i.ProductId, i.Quantity}).ToListAsync();
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                if (qty + cart.Quantity > product.RemainingQuantity)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Dont have enough item");
                    return BadRequest(_response);
                }
                cart.Quantity += qty;
                _db.Cart.Update(cart);
                await _db.SaveChangesAsync();
                _response.Result = await _db.Cart.Where(i => i.UserId == cardNo).ToListAsync();
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

    }
}
