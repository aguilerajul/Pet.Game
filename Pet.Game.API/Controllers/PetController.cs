using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Pet.Game.API.Dtos;
using Pet.Game.Domain.Interfaces;
using System.Collections.Generic;

namespace Pet.Game.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<PetController> logger;
        private readonly IPetRepository petRepository;

        public PetController(ILogger<PetController> logger,
            IPetRepository petRepository,
            IMapper mapper)
        {
            this.logger = logger;
            this.petRepository = petRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var pet = await this.petRepository.GetAsync(id);
                if (pet == null)
                    return NotFound();

                return Ok(mapper.Map<PetResponseDto>(pet));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to get User: {id}", id);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                var pets = await this.petRepository.ListAsync();
                if (!pets.Any())
                    return NoContent();

                return Ok(mapper.Map<IEnumerable<PetResponseDto>>(pets));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to get the list of Users");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
