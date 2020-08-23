﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Pet.Game.API.Dtos;
using Pet.Game.Domain.Entities;
using Pet.Game.Domain.Interfaces;
using System.Collections.Generic;

namespace Pet.Game.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;
        private readonly IUserRepository userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IMapper mapper)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var user = await this.userRepository.GetAsync(id);
                if (user == null)
                    return NotFound();

                return Ok(mapper.Map<UserRequestDto>(user));
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
                var users = await this.userRepository.ListAsync();
                if (!users.Any())
                    return NoContent();

                return Ok(mapper.Map<IEnumerable<UserRequestDto>>(users));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to get the list of Users");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> Post([FromBody] UserRequestDto data)
        {
            try
            {
                var newUser = await this.userRepository.AddOrUpdateAsync(mapper.Map<User>(data));
                return Ok(mapper.Map<UserRequestDto>(newUser));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to add a new user");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
