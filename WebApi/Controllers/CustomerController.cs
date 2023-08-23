using System;
using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly IRepository<CustomerEntity> _repository;
        private readonly IMapper _mapper;
        public CustomerController(IRepository<CustomerEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id:long}")]   
        public async Task<ActionResult<Customer>> GetCustomerAsync([FromRoute] long id)
        {
            var res =  await _repository.GetAsync(id);
            if(res == null)
            {
                return NotFound("Клиент с таким ID не найден");
            }

            return Ok(res);
        }

        [HttpPost("")]   
        public async Task<IActionResult> CreateCustomerAsync([FromBody] Customer customer)
        {
            var exist = await _repository.GetAsync(customer.Id);
            if (exist != null)
            {
                return Conflict($"Клиент с ID = {exist.Id}");
            }

            var res = await _repository.AddAsync(_mapper.Map<CustomerEntity>(customer));
            
            return Ok($"Клиент c ID = {res.Id} успешно добавлен!");
        }
    }
}