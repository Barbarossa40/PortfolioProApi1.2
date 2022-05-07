using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PortfolioProApi.Entities;



namespace PortfolioProApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private PortfolioProApiContext _context;
        public AssetController(PortfolioProApiContext context)
        {
            _context = context;
        }



        [HttpGet]
        [Route("new")]
        public IActionResult GetNewAsset(string symbol, string? uuid = "")
        {
            Asset result = new Asset();
            int comId = 0;

            if (uuid == "")
            {

                result = _context.Assets.Where(y => y.StockSymbol == symbol).FirstOrDefault();


                return Ok(result);
            }
            else if (uuid != "")
            {
                result = _context.Assets.Where(x => x.StockSymbol == symbol).Where(y => y.Uuid == uuid).FirstOrDefault();
                return Ok(result);
            }
            else return BadRequest();



        }

        [HttpGet("{id}")]
        public IActionResult GetAssetById(int id)
        {
            Asset result = _context.Assets.Where(x => x.AssetId == id).FirstOrDefault();
            return Ok(result);
        }


        [HttpGet]
        [Route("ticker")]
        public IActionResult GetAssetByTicker(string ticker)
        {
            List<Asset> result = _context.Assets.Where(x => x.StockSymbol.Contains(ticker)).ToList();
            return Ok(result);
        }




        [HttpPost]
        public IActionResult AddNewAsset([FromBody] Asset newAsset)
        {
            if (ModelState.IsValid)
            {


                _context.Assets.Add(newAsset);

                _context.SaveChanges();

                return Ok();
            }
            else
                return BadRequest();
        }
    }
}

        //    if(_context.Assets.fin(x => x.StockSymbol == newAsset.StockSymbol))         //    {
                //        return BadRequest(newAsset);
                //    }
                //    else
                //    {
                //        _context.Assets.Add(newAsset);

                //        _context.SaveChanges();

                //        return Ok();
                //    }
                //}
                //if (newAsset.Uuid !="")
                //{
                //    if (_context.Assets.Any(x => x.Uuid == newAsset.Uuid))
                //    {
                //        return BadRequest(newAsset);
                //    }
                //}
                //else
                //{
                //    _context.Assets.Add(newAsset);

                //    _context.SaveChanges();

                //    return Ok();
                //}
                //return BadRequest();
            

            //public int TransactionId { get; set; }

            //[DataType(DataType.DateTime)]
            //[Display(Name = "Time Of Transaction")]
            //public DateTime TransactionTime { get; set; }

            //public double TransactionAmount { get; set; }

            //public double TotalAmount { get; set; }

            //[ForeignKey("UserId")]
            //public User User { get; set; }

            //[ForeignKey("AssetId")]
            //public virtual Asset Asset { get; set; }
            //public int AssetId { get; set; }
       



    


