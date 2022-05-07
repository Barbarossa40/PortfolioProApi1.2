using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PortfolioProApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PortfolioProApi.Entities;


namespace PortfolioProApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private PortfolioProApiContext _context;
        private readonly IMapper _mapper;


        public UserController(PortfolioProApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       

        [HttpGet]
        public IActionResult GetAssetsByUserId(string id)
        {
            try
            {
                var query = _context.Transactions.Where(x => x.UserId == id)
                .Include(x => x.Asset)
                .AsEnumerable()
                 .GroupBy(d => d.AssetId)
                .Select(g => g.OrderByDescending(d => d.TransactionId))
                .OrderByDescending(e => e.First().AssetId)
                .Select(e => e.FirstOrDefault())
                .ToList();

                query.RemoveAll(x => x.TotalAmount == 0);

                return Ok(query);

            }
            catch (Exception xxx)
            {

                BadRequest(xxx);
            }

            return BadRequest();


        }

        [HttpGet]
        [Route("average")]
        public IActionResult GetAveragePrice(string id)
        {
            var query = _context.Transactions.Where(x => x.UserId == id)
                .AsEnumerable()
           .GroupBy(d => d.AssetId)
           .Select(g => g.OrderByDescending(d => d.TransactionId))
           .OrderByDescending(e => e.First().AssetId)
          .ToList();



            List<double> avgPriceList = new List<double>();

       

            foreach(var item in query)
            {
                double weightedAmount = 0;
                double totalCount =0;
                double totalCost=0;
                Transaction mostRecent = item.FirstOrDefault();

                foreach(var trans in item)
                {
                   weightedAmount = trans.TransactionAmount / mostRecent.TotalAmount;

                     totalCost += (trans.PriceSnapshot * weightedAmount);
                    
                }
                avgPriceList.Add(totalCost);
               
            }

            return Ok(avgPriceList);
        }




        [HttpGet]
        [Route("details")]
        public IActionResult GetUserAssetDetail(string userid, int assetId)
        {
            List<Transaction> transactions = _context.Transactions.Where(x => x.UserId == userid).Where(x => x.AssetId == assetId)
                  .OrderByDescending(x => x.TransactionTime).Include(x => x.Asset).ToList();

            return Ok(transactions);

        }


        [HttpPost]
        public IActionResult PostTransaction(TransactionDto transactionDto)
        {
            Transaction transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.Asset = _context.Assets.Where(c => c.AssetId == transactionDto.AssetId).FirstOrDefault();

            _context.Transactions.Add(transaction);

            _context.SaveChanges();
            return Ok(transaction);
        }

        [HttpPut]
        public IActionResult PutTransaction([FromBody] TransactionPutDto transactionPutDto)
        {
            try
            {
                Transaction transaction = _mapper.Map<Transaction>(transactionPutDto);
                transaction.Asset = _context.Assets.Where(c => c.AssetId == transactionPutDto.AssetId).FirstOrDefault();


                if (!ModelState.IsValid)
                {
                    return BadRequest("Not valid");
                }

                Transaction existingTransaction = _context.Transactions.Where(c => c.UserId == transaction.UserId).Where(x => x.AssetId == transaction.AssetId).FirstOrDefault();


                if (existingTransaction != null)
                {
                    existingTransaction.AssetId = transaction.AssetId;
                    existingTransaction.UserId = transaction.UserId;
                    existingTransaction.TransactionAmount = transaction.TransactionAmount;
                    existingTransaction.TransactionTime = transaction.TransactionTime;
                    existingTransaction.TotalAmount=transaction.TotalAmount;
                    existingTransaction.Asset=transaction.Asset;

                    _context.Transactions.Update(existingTransaction);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {

                    return NotFound();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }


            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteTransaction(int tId, string id)
        {
            try
            {
                Transaction deleteTransaction = _context.Transactions.Where(u => u.UserId == id).Where(c => c.TransactionId == tId).FirstOrDefault();

                if(deleteTransaction != null)
                {
                    _context.Transactions.Remove(deleteTransaction);
                    _context.SaveChanges();
                    
                    return Ok();
                }
                else
                return NotFound();
                        
            }
            catch (Exception)
            {

                
            }
            return BadRequest();
           

        }

        [HttpGet]
        [Route("transaction-id")]
        public IActionResult GetTransactionbyId(int tId)
        {   
            Transaction transaction = _context.Transactions.Where(u => u.TransactionId == tId).FirstOrDefault();  

            if(transaction != null)
            {
                return Ok(transaction);
            }
            else return BadRequest();
            
        }
    }
}